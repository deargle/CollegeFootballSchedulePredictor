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

namespace CollegeFootballSeasonPredictor.View
{
    public partial class ScheduleView : PhoneApplicationPage
    {
        public ScheduleView()
        {
            InitializeComponent();

            this.DataContext = App.ScheduleViewModel;     
        }

        private void teamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void predict(object sender, EventArgs e)
        {
            App.ScheduleViewModel.SimulateSchedule();

            ScheduleViewModel s = App.ScheduleViewModel;
            ObservableCollection<Game> m = App.ScheduleViewModel.TeamSchedule;
            NavigationService.Navigate(new Uri("/View/PredictionView.xaml", UriKind.Relative));
        }

    }
}
