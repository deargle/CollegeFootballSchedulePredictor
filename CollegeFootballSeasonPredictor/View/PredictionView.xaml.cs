using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using CollegeFootballSeasonPredictor.Model;
using System.Windows.Data;
using Microsoft.Advertising.Mobile.UI;

namespace CollegeFootballSeasonPredictor.View
{
    // prediction should already have been run, before this page is reached. This page just shows the results.
    public partial class PredictionView : PhoneApplicationPage
    {
        public PredictionView()
        {
            InitializeComponent();

            this.DataContext = App.ScheduleViewModel;
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            App.ScheduleViewModel.SimulateSchedule();
            DataContext = App.ScheduleViewModel;

            NavigationService.Navigate(new Uri(string.Format("/View/PredictionView.xaml?Refresh=true&random={0}", Guid.NewGuid()), UriKind.Relative));
            //NavigationService.Navigate(new Uri("/View/PredictionView.xaml", UriKind.Relative));
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/About.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ScheduleView.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string refresh;
            if (NavigationContext.QueryString.TryGetValue("Refresh", out refresh))
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
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