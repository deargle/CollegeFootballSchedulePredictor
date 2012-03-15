using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using Microsoft.Phone.Controls;
using CollegeFootballSeasonPredictor.Model;
using System.Windows.Data;
using Microsoft.Advertising.Mobile.UI;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Shell;

namespace CollegeFootballSeasonPredictor.View
{
    // prediction should already have been run, before this page is reached. This page just shows the results.
    public partial class PredictionView : PhoneApplicationPage
    {
        Accelerometer accelerometer;
        Popup popup;
        BackgroundWorker backgroundWorker;

        public PredictionView()
        {
            InitializeComponent();

            this.DataContext = App.ScheduleViewModel;

            this.accelerometer = new Accelerometer();
            this.accelerometer.CurrentValueChanged += Accelerometer_CurrentValueChanged;
        }

        void Accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            if (ShakeDetection.JustShook(e.SensorReading.Acceleration))
            {
                // We're on a different thread, so transition to the UI thread
                this.Dispatcher.BeginInvoke(delegate()
                {
                    PredictButton_Click(null, null);
                });
            }
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            App.ScheduleViewModel.SimulateSchedule();
            DataContext = App.ScheduleViewModel;
            // ShowPopup();
            // StartLoadingData();

            NavigationService.Navigate(new Uri(string.Format("/View/PredictionView.xaml?Refresh=true&random={0}", Guid.NewGuid()), UriKind.Relative));
            //NavigationService.Navigate(new Uri("/View/PredictionView.xaml", UriKind.Relative));
        }

        // http://msdn.microsoft.com/en-us/library/hh202979(v=vs.92).aspx
        private void SaveFavoriteTeamButton_Clicked(object sender, EventArgs e)
        {
            Team selectedTeam = App.ScheduleViewModel.SelectedTeam;

            // Look to see whether the Tile already exists and if so, don't try to create it again.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(string.Format("Team={0}", selectedTeam.TeamName)));

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

        private void ShowPopup()
        {
            this.popup = new Popup();
            this.popup.Child = new About();
            this.popup.IsOpen = true;
        }

        private void StartLoadingData()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backroungWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backroungWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        void backroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.popup.IsOpen = false;
            }
            );
        }

        void backroungWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do some data loading on a background
            // We'll just sleep for the demo
            Thread.Sleep(5000);
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/About.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ScheduleView.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Settings.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            // Stop the accelerometer
            try
            {
                this.accelerometer.Stop();
            }
            catch
            {  /* Nothing to do */ }

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string refresh;
            if (NavigationContext.QueryString.TryGetValue("Refresh", out refresh))
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);

            
            AppSettings settings = new AppSettings();
            if (settings.ShakeToPredictSetting)
            {
                // Start the accelerometer
                try
                {
                    this.accelerometer.Start();
                }
                catch
                {
                    MessageBox.Show("Unable to start your accelerometer. Shake-to-predict will be disabled. To enable, please try running this app again.", "Accelerometer Error", MessageBoxButton.OK);
                }
            }
            
        }
    }

    public class GameWinnerConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Team winner = (Team)value;
            string result;
            if (winner == App.ScheduleViewModel.SelectedTeam)
            {
                result = "Win!";
            }
            else
            {
                result = "Lost.";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    
}