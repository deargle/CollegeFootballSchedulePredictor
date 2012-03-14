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
using Microsoft.Phone.Tasks;

namespace CollegeFootballSeasonPredictor.View
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();

                marketplaceDetailTask.Show();
            }
            catch (InvalidOperationException ex)
            {
                // user probably double-clicked the button before navigation occurred
            }
        }

        private void ReviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

                marketplaceReviewTask.Show();
            }
            catch (InvalidOperationException ex)
            {
                // user probably double-clicked the button before navigation occurred
            }
        }

        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                EmailComposeTask emailComposer = new EmailComposeTask();
                emailComposer.To = "dave.eargle+footballpredictor@gmail.com";
                emailComposer.Subject = "NCAA Footbal Predictor 2012 Feedback";
                emailComposer.Show();
            }
            catch (InvalidOperationException ex)
            {
                // user probably double-clicked the button before navigation occurred
            }
        }
    }
}