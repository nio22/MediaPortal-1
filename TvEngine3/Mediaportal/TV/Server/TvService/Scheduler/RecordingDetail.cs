#region Copyright (C) 2005-2011 Team MediaPortal

// Copyright (C) 2005-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Text.RegularExpressions;
using Mediaportal.TV.Server.TVControl;
using Mediaportal.TV.Server.TVDatabase.Entities;
using Mediaportal.TV.Server.TVDatabase.Entities;
using Mediaportal.TV.Server.TVDatabase.Entities.Factories;
using Mediaportal.TV.Server.TVDatabase.TVBusinessLayer;
using Mediaportal.TV.Server.TVDatabase.TVBusinessLayer.Entities;
using Mediaportal.TV.Server.TVDatabase.Entities.Enums;
using Mediaportal.TV.Server.TVLibrary.Interfaces.Logging;
using Mediaportal.TV.Server.TVService.Interfaces.Services;

namespace Mediaportal.TV.Server.TVService.Scheduler
{
  /// <summary>
  /// class which holds all details about a schedule which is current being recorded
  /// </summary>
  public class RecordingDetail
  {
    #region variables
    
    
    private readonly ScheduleBLL _schedule;
    private readonly Channel _channel;
    private string _fileName;
    private readonly DateTime _endTime;
    private readonly ProgramBLL _program;
    private CardDetail _cardInfo;
    private DateTime _dateTimeRecordingStarted;
    private Recording _recording;
    private readonly bool _isSerie;
    private readonly IUser _user;

    #endregion

    #region ctor

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="schedule">Schedule of this recording</param>
    /// <param name="channel">Channel on which the recording is done</param>
    /// <param name="endTime">Date/Time the recording should start without pre-record interval</param>
    /// <param name="endTime">Date/Time the recording should stop with post record interval</param>
    /// <param name="isSerie">Is serie recording</param>
    /// 
    /// 
    public RecordingDetail(Schedule schedule, Channel channel, DateTime endTime, bool isSerie)
    {
      _user = UserFactory.CreateSchedulerUser(schedule.id_Schedule);
      _schedule = new ScheduleBLL(schedule);
      _channel = channel;
      _endTime = endTime;
      _program = null;
      _isSerie = isSerie;

      DateTime startTime = DateTime.MinValue;

      if (isSerie)
      {
        DateTime now = DateTime.Now.AddMinutes(schedule.preRecordInterval);
        startTime = new DateTime(now.Year, now.Month, now.Day, schedule.startTime.Hour, schedule.startTime.Minute, 0);
      }
      else
      {
        startTime = schedule.startTime;
      }

      Program program = ProgramManagement.GetProgramAt(startTime, schedule.Channel.idChannel);
      
      //no program? then treat this as a manual recording
      if (program == null)
      {
        program = ProgramFactory.CreateProgram(0, DateTime.Now, endTime, "manual", "", null,
                                                   ProgramState.None,
                                                   System.Data.SqlTypes.SqlDateTime.MinValue.Value, string.Empty, string.Empty,
                                                   string.Empty, string.Empty, -1, string.Empty, 0);        
      }
      _program = new ProgramBLL(program);
    }

    #endregion

    #region properties

    public Recording Recording
    {
      get { return _recording; }
      set { _recording = value; }
    }

    /// <summary>
    /// get/sets the CardInfo for this recording
    /// </summary>
    public CardDetail CardInfo
    {
      get { return _cardInfo; }
      set { _cardInfo = value; }
    }

    /// <summary>
    /// Gets or sets the recording start date time.
    /// </summary>
    /// <value>The recording start date time.</value>
    public DateTime RecordingStartDateTime
    {
      get { return _dateTimeRecordingStarted; }
      set { _dateTimeRecordingStarted = value; }
    }

    /// <summary>
    /// gets the Schedule belonging to this recording
    /// </summary>
    public ScheduleBLL Schedule
    {
      get { return _schedule; }
    }

    /// <summary>
    /// gets the Channel which is being recorded
    /// </summary>
    public Channel Channel
    {
      get { return _channel; }
    }

    /// <summary>
    /// Gets the filename of the recording
    /// </summary>
    public string FileName
    {
      get { return _fileName; }
      set { _fileName = value; }
    }

    /// <summary>
    /// Gets the date/time on which the recording should stop
    /// </summary>
    public DateTime EndTime
    {
      get { return _endTime; }
    }

    /// <summary>
    /// Gets the Program which is being recorded
    /// </summary>
    public ProgramBLL Program
    {
      get { return _program; }
    }

    /// <summary>
    /// Property which returns true when recording is busy
    /// and false when recording should be stopped
    /// </summary>
    public bool IsRecording
    {
      get
      {
        bool isRecording = false;

        try
        {
          Schedule _sched = ScheduleManagement.GetSchedule(_schedule.Entity.id_Schedule); // Refresh();
          if (_sched != null)
          {
            isRecording = (DateTime.Now < EndTime.AddMinutes(_sched.postRecordInterval));
          }
        }
        catch (Exception e)
        {
          Log.Error("RecordingDetail: exception occured {0}", e);
        }

        return isRecording;
      }
    }

    /// <summary>
    /// Property wich returns true if the recording detail is a serie
    /// </summary>
    public bool IsSerie
    {
      get { return _isSerie; }
    }

    public IUser User
    {
      get { return _user; }
    }

    #endregion

    #region private members

    /// <summary>
    /// Create the filename for the recording 
    /// </summary>
    /// <param name="recordingPath"></param>
    public void MakeFileName(string recordingPath)
    {

      Setting setting;
      if (!IsSerie)
      {
        Log.Debug("Scheduler: MakeFileName() using \"moviesformat\" (_isSerie={0})", _isSerie);
        setting = SettingsManagement.GetSetting("moviesformat", "%title%");
      }
      else
      {
        Log.Debug("Scheduler: MakeFileName() using \"seriesformat\" (_isSerie={0})", _isSerie);
        setting = SettingsManagement.GetSetting("seriesformat", "%title%");
      }

      string strInput = "title%";
      if (setting != null)
      {
        if (setting.value != null)
        {
          strInput = setting.value;
        }
      }
      string subDirectory = string.Empty;
      string fullPath = recordingPath;
      string fileName;
      const string recEngineExt = ".ts";

      string[] TagNames = {
                            "%channel%",
                            "%title%",
                            "%name%",
                            "%series%",
                            "%episode%",
                            "%part%",
                            "%date%",
                            "%start%",
                            "%end%",
                            "%genre%",
                            "%startday%",
                            "%startmonth%",
                            "%startyear%",
                            "%starthh%",
                            "%startmm%",
                            "%endday%",
                            "%endmonth%",
                            "%endyear%",
                            "%endhh%",
                            "%endmm%"
                          };
      var programCategory = "";
      if (Program.Entity.ProgramCategory != null)
      {
        programCategory = Program.Entity.ProgramCategory.category.Trim();  
      }
      
      string[] TagValues = {
                             _schedule.Entity.Channel.displayName.Trim(),
                             Program.Entity.title.Trim(),
                             Program.Entity.episodeName.Trim(),
                             Program.Entity.seriesNum.Trim(),
                             Program.Entity.episodeNum.Trim(),
                             Program.Entity.episodePart.Trim(),
                             Program.Entity.startTime.ToString("yyyy-MM-dd"),
                             Program.Entity.startTime.ToShortTimeString(),
                             Program.Entity.endTime.ToShortTimeString(),
                             programCategory,
                             Program.Entity.startTime.ToString("dd"),
                             Program.Entity.startTime.ToString("MM"),
                             Program.Entity.startTime.ToString("yyyy"),
                             Program.Entity.startTime.ToString("HH"),
                             Program.Entity.startTime.ToString("mm"),
                             Program.Entity.endTime.ToString("dd"),
                             Program.Entity.endTime.ToString("MM"),
                             Program.Entity.endTime.ToString("yyyy"),
                             Program.Entity.endTime.ToString("HH"),
                             Program.Entity.endTime.ToString("mm")
                           };

      for (int i = 0; i < TagNames.Length; i++)
      {
        strInput = Utils.ReplaceTag(strInput, TagNames[i], Utils.MakeFileName(TagValues[i]), "unknown");
        if (!strInput.Contains("%"))
        {
          break;
        }
      }

      int index = strInput.LastIndexOf('\\');
      if (index != -1)
      {
        subDirectory = strInput.Substring(0, index).Trim();
        fileName = strInput.Substring(index + 1).Trim();
      }
      else
        fileName = strInput.Trim();


      if (subDirectory != string.Empty)
      {
        subDirectory = Utils.RemoveTrailingSlash(subDirectory);
        subDirectory = Utils.MakeDirectoryPath(subDirectory);

        /* Replace any trailing dots in path name; Bugfix for Mantis 1881 */
        subDirectory = new Regex(@"\.*$").Replace(subDirectory, "");
        /* Replace any trailing spaces in path name; Bugfix for Mantis 2933*/
        subDirectory = new Regex(@"\s+\\\s*|\\\s+").Replace(subDirectory, "\\");

        fullPath = recordingPath + "\\" + subDirectory.Trim();
        if (!System.IO.Directory.Exists(fullPath))
          System.IO.Directory.CreateDirectory(fullPath);
      }
      if (fileName == string.Empty)
      {
        DateTime dt = Program.Entity.startTime;
        fileName = String.Format("{0}_{1}_{2}{3:00}{4:00}{5:00}{6:00}p{7}{8}",
                                 _schedule.Entity.Channel.displayName, Program.Entity.title,
                                 dt.Year, dt.Month, dt.Day,
                                 dt.Hour,
                                 dt.Minute,
                                 DateTime.Now.Minute, DateTime.Now.Second);
      }
      fileName = Utils.MakeFileName(fileName);
      if (DoesFileExist(fullPath + "\\" + fileName))
      {
        int i = 1;
        while (DoesFileExist(fullPath + "\\" + fileName + "_" + i))
          ++i;
        fileName += "_" + i;
      }
      _fileName = fullPath + "\\" + fileName + recEngineExt;
    }

    /// <summary>
    /// checks if a recording with the specified filename exists
    /// either as .mpg or as .ts
    /// </summary>
    /// <param name="fileName">full path and filename expect the extension.</param>
    /// <returns>true if file exists, otherwise false</returns>
    private static bool DoesFileExist(string fileName)
    {
      if (System.IO.File.Exists(fileName + ".mpg"))
        return true;
      if (System.IO.File.Exists(fileName + ".ts"))
        return true;
      return false;
    }

    #endregion
  }
}