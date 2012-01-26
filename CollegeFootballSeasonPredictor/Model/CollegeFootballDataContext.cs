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

        public Game() : base()
        {
            _confidence = .5; // DEFAULT VALUE
        }

        // Define item name: private field, public property and database column.
        [Column(IsPrimaryKey=true)]
        public string HomeTeamName;
        
        private EntityRef<Team> _homeTeam;
        [Association(ThisKey = "HomeTeamName", OtherKey = "TeamName", Storage = "_homeTeam")]
        public Team HomeTeam
        {
            get
            {
                return this._homeTeam.Entity;
            }
            set
            {
                if (_homeTeam.Entity != value)
                {
                    NotifyPropertyChanging("HomeTeam");
                    _homeTeam.Entity = value;
                    NotifyPropertyChanged("HomeTeam");
                }
            }
        }

        // Define item name: private field, public property and database column.
        [Column(IsPrimaryKey = true)]
        public string AwayTeamName;

        private EntityRef<Team> _awayTeam;
        [Association(ThisKey = "AwayTeamName", OtherKey = "TeamName", Storage = "_awayTeam")]
        public Team AwayTeam
        {
            get
            {
                return this._awayTeam.Entity;
            }
            set
            {
                if (_awayTeam.Entity != value)
                {
                    NotifyPropertyChanging("AwayTeam");
                    _awayTeam.Entity = value;
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
        private double _confidence;

        public double Confidence
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

        private Team _winner;
        public Team Winner
        {
            get
            {
                return _winner;
            }
            set
            {
                if (_winner != value)
                {
                    NotifyPropertyChanging("Winner");
                    _winner = value;
                    NotifyPropertyChanged("Winner");
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
        // Define item name: private field, public property and database column.
        private string _teamName;

        [Column(IsPrimaryKey = true)]
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
        private string _displayName;

        [Column]
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                if (_displayName != value)
                {
                    NotifyPropertyChanging("DisplayName");
                    _displayName = value;
                    NotifyPropertyChanged("DisplayName");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _color;

        [Column]
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    NotifyPropertyChanging("Color");
                    _color = value;
                    NotifyPropertyChanged("Color");
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