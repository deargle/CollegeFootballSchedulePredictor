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