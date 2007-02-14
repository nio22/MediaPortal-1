//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the Gentle.NET Business Entity template, $Rev: 965 $
//========================================================================
using System;
using System.Collections;
using Gentle.Common;
using Gentle.Framework;

namespace TvDatabase
{
  /// <summary>
  /// Instances of this class represent the properties and methods of a row in the table <b>Card</b>.
  /// </summary>
  [TableName("Card")]
  public class Card : Persistent
  {
    #region Members
    private bool isChanged;
    [TableColumn("idCard", NotNull = true), PrimaryKey(AutoGenerated = true)]
    private int idCard;
    [TableColumn("devicePath", NotNull = true)]
    private string devicePath;
    [TableColumn("name", NotNull = true)]
    private string name;
    [TableColumn("priority", NotNull = true)]
    private int priority;
    [TableColumn("grabEPG", NotNull = true)]
    private bool grabEPG;
    [TableColumn("lastEpgGrab", NotNull = true)]
    private DateTime lastEpgGrab;
    [TableColumn("recordingFolder", NotNull = true)]
    private string recordingFolder;
    [TableColumn("idServer", NotNull = true), ForeignKey("Server", "idServer")]
    private int idServer;
    [TableColumn("enabled", NotNull = true)]
    private bool enabled;
    [TableColumn("camType", NotNull = true)]
    private int camType;
    [TableColumn("timeshiftingFolder", NotNull = true)]
    private string timeshiftingFolder;
    [TableColumn("recordingFormat", NotNull = true)]
    private int recordingFormat;
    [TableColumn("decryptLimit", NotNull = true)]
    private int decryptLimit;
    #endregion

    #region Constructors
    /// <summary> 
    /// Create a new object by specifying all fields (except the auto-generated primary key field). 
    /// </summary> 
    public Card(string devicePath, string name, int priority, bool grabEPG, DateTime lastEpgGrab, string recordingFolder, int idServer, bool enabled, int camType, string timeshiftingFolder, int recordingFormat, int decryptLimit)
    {
      isChanged = true;
      this.devicePath = devicePath;
      this.name = name;
      this.priority = priority;
      this.grabEPG = grabEPG;
      this.lastEpgGrab = lastEpgGrab;
      this.recordingFolder = recordingFolder;
      this.idServer = idServer;
      this.enabled = enabled;
      this.camType = camType;
      this.timeshiftingFolder = timeshiftingFolder;
      this.recordingFormat = recordingFormat;
      this.decryptLimit = decryptLimit;
    }

    /// <summary> 
    /// Create an object from an existing row of data. This will be used by Gentle to 
    /// construct objects from retrieved rows. 
    /// </summary> 
    public Card(int idCard, string devicePath, string name, int priority, bool grabEPG, DateTime lastEpgGrab, string recordingFolder, int idServer, bool enabled, int camType, string timeshiftingFolder, int recordingFormat, int decryptLimit)
    {
      this.idCard = idCard;
      this.devicePath = devicePath;
      this.name = name;
      this.priority = priority;
      this.grabEPG = grabEPG;
      this.lastEpgGrab = lastEpgGrab;
      this.recordingFolder = recordingFolder;
      this.idServer = idServer;
      this.enabled = enabled;
      this.camType = camType;
      this.timeshiftingFolder = timeshiftingFolder;
      this.recordingFormat = recordingFormat;
      this.decryptLimit = decryptLimit;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Indicates whether the entity is changed and requires saving or not.
    /// </summary>
    public bool IsChanged
    {
      get { return isChanged; }
    }

    /// <summary>
    /// Property relating to database column idCard
    /// </summary>
    public int IdCard
    {
      get { return idCard; }
    }


    /// <summary>
    /// Property relating to database column idCard
    /// </summary>
    public int DecryptLimit
    {
      get { return decryptLimit; }
      set { isChanged |= decryptLimit != value; decryptLimit = value; }
    }

    /// <summary>
    /// Property relating to database column recordingFormat
    /// </summary>
    public int RecordingFormat
    {
      get { return recordingFormat; }
      set { isChanged |= recordingFormat != value; recordingFormat = value; }
    }
    /// <summary>
    /// Property relating to database column devicePath
    /// </summary>
    public string DevicePath
    {
      get { return devicePath; }
      set { isChanged |= devicePath != value; devicePath = value; }
    }

    /// <summary>
    /// Property relating to database column name
    /// </summary>
    public string Name
    {
      get { return name; }
      set { isChanged |= name != value; name = value; }
    }

    /// <summary>
    /// Property relating to database column priority
    /// </summary>
    public int Priority
    {
      get { return priority; }
      set { isChanged |= priority != value; priority = value; }
    }

    /// <summary>
    /// Property relating to database column grabEPG
    /// </summary>
    public bool GrabEPG
    {
      get { return grabEPG; }
      set { isChanged |= grabEPG != value; grabEPG = value; }
    }

    /// <summary>
    /// Property relating to database column lastEpgGrab
    /// </summary>
    public DateTime LastEpgGrab
    {
      get { return lastEpgGrab; }
      set { isChanged |= lastEpgGrab != value; lastEpgGrab = value; }
    }

    /// <summary>
    /// Property relating to database column recordingFolder
    /// </summary>
    public string RecordingFolder
    {
      get { return recordingFolder; }
      set { isChanged |= recordingFolder != value; recordingFolder = value; }
    }
    /// <summary>
    /// Property relating to database column timeshiftingFolder
    /// </summary>
    public string TimeShiftFolder
    {
      get { return timeshiftingFolder; }
      set { isChanged |= timeshiftingFolder != value; timeshiftingFolder = value; }
    }

    /// <summary>
    /// Property relating to database column idServer
    /// </summary>
    public int IdServer
    {
      get { return idServer; }
      set { isChanged |= idServer != value; idServer = value; }
    }

    /// <summary>
    /// Property relating to database column enabled
    /// </summary>
    public bool Enabled
    {
      get { return enabled; }
      set { isChanged |= enabled != value; enabled = value; }
    }

    /// <summary>
    /// Property relating to database column idServer
    /// </summary>
    public int CamType
    {
      get { return camType; }
      set { isChanged |= camType != value; camType = value; }
    }
    #endregion

    #region Storage and Retrieval

    /// <summary>
    /// Static method to retrieve all instances that are stored in the database in one call
    /// </summary>
    public static IList ListAll()
    {
      return Broker.RetrieveList(typeof(Card));
    }

    /// <summary>
    /// Retrieves an entity given it's id.
    /// </summary>
    public static Card Retrieve(int id)
    {
      // Return null if id is smaller than seed and/or increment for autokey
      if (id < 1)
      {
        return null;
      }
      Key key = new Key(typeof(Card), true, "idCard", id);
      return Broker.RetrieveInstance(typeof(Card), key) as Card;
    }

    /// <summary>
    /// Retrieves an entity given it's id, using Gentle.Framework.Key class.
    /// This allows retrieval based on multi-column keys.
    /// </summary>
    public static Card Retrieve(Key key)
    {
      return Broker.RetrieveInstance(typeof(Card), key) as Card;
    }

    /// <summary>
    /// Persists the entity if it was never persisted or was changed.
    /// </summary>
    public override void Persist()
    {
      if (IsChanged || !IsPersisted)
      {
        base.Persist();
        isChanged = false;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Checks if a card can view a specific channel
    /// </summary>
    /// <param name="_card">Card object</param>
    /// <param name="_channelId">Channel id</param>
    /// <returns>true/false</returns>
    public bool canViewTvChannel(int _channelId)
    {
      IList _cardChannels = this.ReferringChannelMap();
      foreach (ChannelMap _cmap in _cardChannels)
      {
        if (_channelId == _cmap.IdChannel) return true;
      }
      return false;
    }

    #endregion

    #region Relations

    /// <summary>
    /// Get a list of ChannelMap referring to the current entity.
    /// </summary>
    public IList ReferringChannelMap()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(ChannelMap));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idCard", idCard);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(ChannelMap), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(ChannelMap), this );
    }
    public IList ReferringCardGroupMap()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(CardGroupMap));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idCard", idCard);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(CardGroupMap), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(ChannelMap), this );
    }

    /// <summary>
    /// Get a list of ChannelMap referring to the current entity.
    /// </summary>
    public IList ReferringDiSEqCMotor()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(DiSEqCMotor));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idCard", idCard);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(DiSEqCMotor), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(ChannelMap), this );
    }

    /// <summary>
    ///
    /// </summary>
    public Server ReferencedServer()
    {
      return Server.Retrieve(IdServer);
    }
    #endregion

    public void Delete()
    {
      IList list = ReferringChannelMap();
      foreach (ChannelMap map in list)
        map.Delete();

      list = ReferringCardGroupMap();
      foreach (CardGroupMap cardGroupMap in list)
        cardGroupMap.Remove();

      list = ReferringDiSEqCMotor();
      foreach (DiSEqCMotor disEqc in list)
        disEqc.Remove();

      Remove();
    }
  }
}
