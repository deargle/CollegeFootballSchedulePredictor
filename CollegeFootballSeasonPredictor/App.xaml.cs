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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CollegeFootballSeasonPredictor.Model;
using CollegeFootballSeasonPredictor.ViewModel;
using System.IO.IsolatedStorage;

namespace CollegeFootballSeasonPredictor
{
    public partial class App : Application
    {
        private static TeamViewModel _teamViewModel = null;
        private static string FLURRY_API_KEY = "3N7F44LR1UMS9EMCPL3C";

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static TeamViewModel TeamViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_teamViewModel == null)
                    _teamViewModel = new TeamViewModel(CollegeFootballSchedulePredictorDataContext.DBConnectionString);
                return _teamViewModel;
            }
        }

        private static ScheduleViewModel _scheduleViewModel = null;

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static ScheduleViewModel ScheduleViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_scheduleViewModel == null)
                {
                    _scheduleViewModel = new ScheduleViewModel(CollegeFootballSchedulePredictorDataContext.DBConnectionString);
                }

                return _scheduleViewModel;
            }
            private set
            {
                _scheduleViewModel = value;
            }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            DataHelper.MoveReferenceDatabase();
            
            // Create the database if it does not yet exist.
            using (CollegeFootballSchedulePredictorDataContext db = new CollegeFootballSchedulePredictorDataContext(CollegeFootballSchedulePredictorDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                {
                    throw new Exception("Reference Database not loaded correctly");
                }
            }
            

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //FlurryWP7SDK.Api.StartSession(FLURRY_API_KEY);
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //FlurryWP7SDK.Api.StartSession(FLURRY_API_KEY);
            // Ensure that application state is restored appropriately
            if (!App.TeamViewModel.IsDataLoaded)
            {
                App.TeamViewModel.LoadData();
            }

            if (PhoneApplicationService.Current.State.ContainsKey("ScheduleViewModel"))
            {
                App.ScheduleViewModel = PhoneApplicationService.Current.State["ScheduleViewModel"] as ScheduleViewModel;
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
            if (_scheduleViewModel != null && _scheduleViewModel.SelectedTeam != null) {
                PhoneApplicationService.Current.State["ScheduleViewModel"] = App.ScheduleViewModel;
                // IsolatedStorageSettings.ApplicationSettings["SelectedTeamName"] = App.ScheduleViewModel.SelectedTeam.TeamName;
            }
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}