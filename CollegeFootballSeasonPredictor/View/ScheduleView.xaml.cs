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

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // DateTime date = DateTime.Parse((string)value);
            DateTime date = (DateTime)value;
            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
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
