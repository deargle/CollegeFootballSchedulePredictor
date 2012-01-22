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
                    NotifyPropertyChanging("GameId");
                    _gameId = value;
                    NotifyPropertyChanged("GameId");
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
                    NotifyPropertyChanging("HomeTeam");
                    _homeTeam = value;
                    NotifyPropertyChanged("HomeTeam");
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
                    NotifyPropertyChanging("AwayTeam");
                    _awayTeam = value;
                    NotifyPropertyChanged("AwayTeam");
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
                    NotifyPropertyChanging("GameDate");
                    _gameDate = value;
                    NotifyPropertyChanged("GameDate");
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
                    NotifyPropertyChanging("Confidence");
                    _confidence = value;
                    NotifyPropertyChanged("Confidence");
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
    public class Team : INotifyPropertyChanged, INotifyPropertyChanging, IComparable
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
                    NotifyPropertyChanging("TeamId");
                    _teamId = value;
                    NotifyPropertyChanged("TeamId");
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
                    NotifyPropertyChanging("TeamName");
                    _teamName = value;
                    NotifyPropertyChanged("TeamName");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _division;

        [Column]
        public string Division
        {
            get
            {
                return _division;
            }
            set
            {
                if (_division != value)
                {
                    NotifyPropertyChanging("Division");
                    _division = value;
                    NotifyPropertyChanged("Division");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _logoPath;

        [Column]
        public string LogoPath
        {
            get
            {
                return _logoPath;
            }
            set
            {
                if (_logoPath != value)
                {
                    NotifyPropertyChanging("LogoPath");
                    _logoPath = value;
                    NotifyPropertyChanged("LogoPath");
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

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Team otherTeam = obj as Team;
            if (otherTeam != null)
            {
                int result = this._teamName.CompareTo(otherTeam.TeamName);
                return result;
            }
            else
            {
                throw new ArgumentException("Object is not a Team");
            }
        }

        public override bool Equals(object obj)
        {
            Team other = obj as Team;
            return obj != null && this.TeamName.Equals(other.TeamName);
        }

        public override int GetHashCode()
        {
            return this.TeamName.GetHashCode();
        }

        public override string ToString()
        {
            return this.TeamName;
        }
    }
}