using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using CollegeFootballSeasonPredictor.Model;


namespace CollegeFootballSeasonPredictor.ViewModel
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private CollegeFootballSchedulePredictorDataContext footballDB;

        // Class constructor, create the data context object.
        public ScheduleViewModel(string footballDBConnectionString)
        {
            footballDB = new CollegeFootballSchedulePredictorDataContext(footballDBConnectionString);
        }

        // TeamSchedule.
        private ObservableCollection<Game> _teamSchedule;
        public ObservableCollection<Game> TeamSchedule
        {
            get 
            { 
                return _teamSchedule; 
            }
            set
            {
                _teamSchedule = value;
                NotifyPropertyChanged("TeamSchedule");
            }
        }

        private Team _selectedTeam;
        public Team SelectedTeam
        {
            get 
            { 
                return _selectedTeam; 
            }
            set
            {
                _selectedTeam = value;
                LoadSchedule(_selectedTeam);
                NotifyPropertyChanged("SelectedTeam");
            }
        }

        // Query database and load the data used by the UI.
        private void LoadSchedule(Team team)
        {
            // Specify the query for all to-do items in the database.
            var schedule = from Game game in footballDB.Games
                           where ( game.AwayTeamName == team.TeamName
                           || game.HomeTeamName == team.TeamName )
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