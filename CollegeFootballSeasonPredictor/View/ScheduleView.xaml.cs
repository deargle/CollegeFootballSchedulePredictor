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

namespace CollegeFootballSeasonPredictor.View
{
    public partial class ScheduleView : PhoneApplicationPage
    {
        public ScheduleView(Team userSelectedTeam)
        {
            InitializeComponent();

            //  TODO: construct a new GameViewModel, passing it the user-selected team
            //  then, set that view as the data context, like this:
            //      this.DataContext = new GameViewModel(CollegeFootballSchedulePredictorDataContext.DBConnectionString, userSelectedTeam);
            //      
        }
    }
}