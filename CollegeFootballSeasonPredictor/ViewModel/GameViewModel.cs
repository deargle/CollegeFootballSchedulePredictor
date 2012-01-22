using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using CollegeFootballSeasonPredictor.Model;


namespace LocalDatabaseSample.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private CollegeFootballSchedulePredictorDataContext footballDB;

        // Class constructor, create the data context object.
        public GameViewModel(string footballDBConnectionString)
        {
            footballDB = new CollegeFootballSchedulePredictorDataContext(footballDBConnectionString);
        }

        //
        // TODO: Add collections, list, and methods here.
        //

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