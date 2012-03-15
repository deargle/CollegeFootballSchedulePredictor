using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using CollegeFootballSeasonPredictor;
using CollegeFootballSeasonPredictor.Model;
using System;
using System.Runtime.Serialization;


namespace CollegeFootballSeasonPredictor.ViewModel
{
    [DataContractAttribute]
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        
        private CollegeFootballSchedulePredictorDataContext _footballDB;
        private CollegeFootballSchedulePredictorDataContext footballDB
        {
            get
            {
                if (_footballDB == null)
                {
                    if (_footballDBConnectionString == null) _footballDBConnectionString = CollegeFootballSchedulePredictorDataContext.DBConnectionString;
                    _footballDB = new CollegeFootballSchedulePredictorDataContext(_footballDBConnectionString);
                    
                }
                return _footballDB;
            }
            set
            {
                _footballDB = value;
            }

        }

        [DataMember]
        internal static string _footballDBConnectionString;

        // Class constructor, create the data context object.
        public ScheduleViewModel(string footballDBConnectionString)
        {
            _footballDBConnectionString = footballDBConnectionString;
        }

        // TeamSchedule.
        private ObservableCollection<Game> _teamSchedule;
        [DataMember]
        public ObservableCollection<Game> TeamSchedule
        {
            get 
            { 
                return _teamSchedule; 
            }
            set
            {
                if (_teamSchedule != value)
                {
                    _teamSchedule = value;
                    NotifyPropertyChanged("TeamSchedule");
                }
            }
        }

        private Team _selectedTeam;
        [DataMember]
        public Team SelectedTeam
        {
            get 
            { 
                return _selectedTeam; 
            }
            set
            {
                Team selectedTeam = (Team)value;
                if (!selectedTeam.Equals(_selectedTeam))
                {
                    _selectedTeam = value;
                    LoadSchedule(_selectedTeam);
                    NotifyPropertyChanged("SelectedTeam");
                }
            }
        }

        public void SimulateSchedule()
        {
            // Reset wins and losses
            _numWins = 0;
            _numLosses = 0;

            Predictor predictor = new Predictor(this._selectedTeam);
            foreach (Game game in this._teamSchedule)
            {
                predictor.predict(game);
                if (game.Winner.Equals(this._selectedTeam))
                {
                    _numWins++;
                }
                else
                {
                    _numLosses++;
                }

                IsScheduleSimulated = true;
            }
        }

        [DataMember]
        public bool IsScheduleSimulated
        {
            get;
            internal set;
        }

        private int _numWins;
        [DataMember]
        public int NumWins
        {
            get
            {
                if (!IsScheduleSimulated)
                {
                    SimulateSchedule();
                }
                return _numWins;
            }
            internal set
            {
                _numWins = value;
            }
        }

        private int _numLosses;
        [DataMember]
        public int NumLosses
        {
            get
            {
                if (!IsScheduleSimulated)
                {
                    SimulateSchedule();
                }
                return _numLosses;
            }
            internal set
            {
                _numLosses = value;
            }
        }

        // Query database and load the data used by the UI.
        private void LoadSchedule(Team team)
        {
            // Specify the query for all to-do items in the database.
            var schedule = from Game game in footballDB.Games
                           where (game.AwayTeamName == team.TeamName
                           || game.HomeTeamName == team.TeamName)
                           orderby game.GameDate
                           select game;

            // Query the database and load all to-do items.
            TeamSchedule = new ObservableCollection<Game>(schedule);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}