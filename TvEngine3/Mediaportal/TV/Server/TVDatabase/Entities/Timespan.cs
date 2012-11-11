//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Mediaportal.TV.Server.TVDatabase.Entities
{
    [DataContract(IsReference = true)]
    public partial class Timespan: IObjectWithChangeTracker, INotifyPropertyChanged
    {
      #region Primitive Properties

      private int _dayOfWeek;
      private DateTime _endTime;
      private int _idKeyword;
      private int _idTimespan;

      private DateTime _startTime;

      [DataMember]
      public int IdTimespan
      {
        get { return _idTimespan; }
        set
        {
          if (_idTimespan != value)
          {
            if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
            {
              throw new InvalidOperationException("The property 'IdTimespan' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
            }
            _idTimespan = value;
            OnPropertyChanged("IdTimespan");
          }
        }
      }

      [DataMember]
      public DateTime StartTime
      {
        get { return _startTime; }
        set
        {
          if (_startTime != value)
          {
            _startTime = value;
            OnPropertyChanged("StartTime");
          }
        }
      }

      [DataMember]
      public DateTime EndTime
      {
        get { return _endTime; }
        set
        {
          if (_endTime != value)
          {
            _endTime = value;
            OnPropertyChanged("EndTime");
          }
        }
      }

      [DataMember]
      public int DayOfWeek
      {
        get { return _dayOfWeek; }
        set
        {
          if (_dayOfWeek != value)
          {
            _dayOfWeek = value;
            OnPropertyChanged("DayOfWeek");
          }
        }
      }

      [DataMember]
      public int IdKeyword
      {
        get { return _idKeyword; }
        set
        {
          if (_idKeyword != value)
          {
            _idKeyword = value;
            OnPropertyChanged("IdKeyword");
          }
        }
      }

      #endregion

      #region ChangeTracking

      private ObjectChangeTracker _changeTracker;
      protected bool IsDeserializing { get; private set; }

      #region INotifyPropertyChanged Members

      event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }

      #endregion

      #region IObjectWithChangeTracker Members

      [DataMember]
      public ObjectChangeTracker ChangeTracker
      {
        get
        {
          if (_changeTracker == null)
          {
            _changeTracker = new ObjectChangeTracker();
            _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
          }
          return _changeTracker;
        }
        set
        {
          if(_changeTracker != null)
          {
            _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
          }
          _changeTracker = value;
          if(_changeTracker != null)
          {
            _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
          }
        }
      }

      #endregion

      protected virtual void OnPropertyChanged(String propertyName)
      {
        if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
        {
          ChangeTracker.State = ObjectState.Modified;
        }
        if (_propertyChanged != null)
        {
          _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
      }
    
      protected virtual void OnNavigationPropertyChanged(String propertyName)
      {
        if (_propertyChanged != null)
        {
          _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
      }

      private event PropertyChangedEventHandler _propertyChanged;

      private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
      {
        if (e.NewState == ObjectState.Deleted)
        {
          ClearNavigationProperties();
        }
      }

      [OnDeserializing]
      public void OnDeserializingMethod(StreamingContext context)
      {
        IsDeserializing = true;
      }
    
      [OnDeserialized]
      public void OnDeserializedMethod(StreamingContext context)
      {
        IsDeserializing = false;
        ChangeTracker.ChangeTrackingEnabled = true;
      }
    
      protected virtual void ClearNavigationProperties()
      {
      }

      #endregion
    }
}
