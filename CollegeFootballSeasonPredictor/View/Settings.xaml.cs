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
using System.IO.IsolatedStorage;
using System.Windows.Data;

namespace CollegeFootballSeasonPredictor.View
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ShakeToPredict_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ctrl = (ToggleSwitch)sender;
            if((bool)ctrl.IsChecked)
            {
                ctrl.Content = "On";
            }
            else
            {
                ctrl.Content = "Off";
            }
        }

    }

    public class ToggleSwitchContentConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool state = (bool)value;
            if (state)
            {
                return "On";
            }
            else
            {
                return "Off";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}