using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using CollegeFootballSeasonPredictor.Model;


namespace CollegeFootballSeasonPredictor.ViewModel
{
    public class TeamViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private CollegeFootballSchedulePredictorDataContext footballDB;

        // Class constructor, create the data context object.
        public TeamViewModel(string footballDBConnectionString)
        {
            footballDB = new CollegeFootballSchedulePredictorDataContext(footballDBConnectionString);
        }

        //
        // TODO: Add collections, list, and methods here.
        //

        // All teams.
        private ObservableCollection<Team> _allTeams;
        public ObservableCollection<Team> AllTeams
        {
            get { return _allTeams; }
            set
            {
                _allTeams = value;
                NotifyPropertyChanged("AllTeams");
            }
        }

        // Query database and load the data used by the UI.
        public void LoadData()
        {

            // Specify the query for all to-do items in the database.
            var teamsInDB = from Team team in footballDB.Teams
                                select team;

            // Query the database and load all to-do items.
            AllTeams = new ObservableCollection<Team>(teamsInDB);

            this.IsDataLoaded = true;
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            footballDB.SubmitChanges();
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
