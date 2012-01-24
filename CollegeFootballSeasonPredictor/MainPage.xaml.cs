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
using Microsoft.Phone.Controls;
using CollegeFootballSeasonPredictor.Model;

namespace CollegeFootballSeasonPredictor
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the UI
            DataContext = App.TeamViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        /*
        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }
         */

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.TeamViewModel.IsDataLoaded)
            {
                App.TeamViewModel.LoadData();
            }
        }

        private void teamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teamsListBox.SelectedItem == null)
                return; // no selection

            App.ScheduleViewModel.SelectedTeam = (Team)teamsListBox.SelectedItem;
            NavigationService.Navigate(new Uri("/View/ScheduleView.xaml", UriKind.Relative));

            teamsListBox.SelectedIndex = -1;
        }
    }
}