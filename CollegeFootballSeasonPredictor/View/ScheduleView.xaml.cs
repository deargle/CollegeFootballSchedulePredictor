﻿using System;
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

namespace CollegeFootballSeasonPredictor.View
{
    public partial class ScheduleView : PhoneApplicationPage
    {
        public ScheduleView()
        {
            InitializeComponent();

            this.DataContext = App.ScheduleViewModel;
            //      
        }
    }
}