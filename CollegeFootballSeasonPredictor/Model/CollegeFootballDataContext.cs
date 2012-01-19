using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using CollegeFootballSeasonPredictor;

namespace CollegeFootballSeasonPredictor.Model
{
    public class CollegeFootballSchedulePredictorDataContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/" + DataHelper.DB_NAME;

        // Pass the connection string to the base class.
        public CollegeFootballSchedulePredictorDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<Game> Games;

        // Specify a single table for the to-do items.
        public Table<Team> Teams;
    }

    [Table]
    public class Game : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _gameId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int GameId
        {
            get
            {
                return _gameId;
            }
            set
            {
                if (_gameId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _gameId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _homeTeam;

        [Column]
        public string HomeTeam
        {
            get
            {
                return _homeTeam;
            }
            set
            {
                if (_homeTeam != value)
                {
                    NotifyPropertyChanging("homeTeam");
                    _homeTeam = value;
                    NotifyPropertyChanged("homeTeam");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _awayTeam;

        [Column]
        public string AwayTeam
        {
            get
            {
                return _awayTeam;
            }
            set
            {
                if (_awayTeam != value)
                {
                    NotifyPropertyChanging("awayTeam");
                    _awayTeam = value;
                    NotifyPropertyChanged("awayTeam");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _gameDate;

        [Column]
        public string GameDate
        {
            get
            {
                return _gameDate;
            }
            set
            {
                if (_gameDate != value)
                {
                    NotifyPropertyChanging("gameDate");
                    _gameDate = value;
                    NotifyPropertyChanged("gameDate");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _confidence;

        public string Confidence
        {
            get
            {
                return _confidence;
            }
            set
            {
                if (_confidence != value)
                {
                    NotifyPropertyChanging("confidence");
                    _confidence = value;
                    NotifyPropertyChanged("confidence");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Team : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _teamId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int TeamId
        {
            get
            {
                return _teamId;
            }
            set
            {
                if (_teamId != value)
                {
                    NotifyPropertyChanging("teamId");
                    _teamId = value;
                    NotifyPropertyChanged("teamId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _teamName;

        [Column]
        public string TeamName
        {
            get
            {
                return _teamName;
            }
            set
            {
                if (_teamName != value)
                {
                    NotifyPropertyChanging("teamName");
                    _teamName = value;
                    NotifyPropertyChanged("teamName");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}