using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using CollegeFootballSeasonPredictor.Model;
using CollegeFootballSeasonPredictor.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using Microsoft.Phone.Shell;

namespace CollegeFootballSeasonPredictor.View
{
    public partial class ScheduleView : PhoneApplicationPage
    {
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void teamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            App.ScheduleViewModel.SimulateSchedule();

            NavigationService.Navigate(new Uri("/View/PredictionView.xaml", UriKind.Relative));
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/About.xaml", UriKind.Relative));
        }

        // http://msdn.microsoft.com/en-us/library/hh202979(v=vs.92).aspx
        private void SaveFavoriteTeamButton_Clicked(object sender, EventArgs e)
        {
            Team selectedTeam = App.ScheduleViewModel.SelectedTeam;

            // Look to see whether the Tile already exists and if so, don't try to create it again.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(string.Format("Team={0}",selectedTeam.TeamName)));

            if (TileToFind == null)
            {
                // Create the Tile object and set some initial properties for the Tile.
                // The Count value of 12 shows the number 12 on the front of the Tile. Valid values are 1-99.
                // A Count value of 0 indicates that the Count should not be displayed.
                StandardTileData NewTileData = new StandardTileData
                {
                    BackgroundImage = new Uri(string.Format("/Images/Logos/{0}", selectedTeam.LogoPath), UriKind.Relative),
                    Title = "NCAA FB Predict"
                };
                ShellTile.Create(new Uri(string.Format("/View/ScheduleView.xaml?Team={0}", selectedTeam.TeamName), UriKind.Relative), NewTileData);
            }
            else
            {
                MessageBox.Show("This team is already pinned to your home screen.", "Already Pinned", MessageBoxButton.OK);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            String selectedTeamName = "";
            if (this.NavigationContext.QueryString.TryGetValue("Team", out selectedTeamName))
            {
                Team selectedTeam = App.TeamViewModel.findByName(selectedTeamName);
                App.ScheduleViewModel.SelectedTeam = selectedTeam;
            }
            // In this case, the selectedTeam should already have been set for the ScheduleViewModel.
            this.DataContext = App.ScheduleViewModel;

            base.OnNavigatedTo(e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }

    public class ConfidenceConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double confidence = (double)value;
            string homeOrAway = (string)parameter;
            switch (homeOrAway)
            {
                case "Home":
                    confidence = 1 - confidence;
                    break;
                case "Away":
                default:
                    break;
            }
            return confidence;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
