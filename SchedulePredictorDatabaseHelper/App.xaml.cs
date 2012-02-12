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

namespace SchedulePredictorDatabaseHelper
{
    public partial class App : Application
    {
        private static string csv_schedule_file = "2012-Schedule.csv";
        private static string csv_team_file = "2012-Teams.csv";

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
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // Create the database if it does not exist.
            using (SchedulePredictorDatabaseHelperDataContext db = new SchedulePredictorDatabaseHelperDataContext(SchedulePredictorDatabaseHelperDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == true)
                {
                    db.DeleteDatabase();
                }
                
                //Create the database
                db.DeleteDatabase();
                db.CreateDatabase();

                CSVParser parser = new CSVParser();
                List<string[]> team_csv_data = parser.parseCSV(csv_team_file);
                List<string[]> schedule_csv_data = parser.parseCSV(csv_schedule_file);

                // Populate Teams
                db.Teams.InsertAllOnSubmit(parse_teams(team_csv_data));
                db.Games.InsertAllOnSubmit(parse_games(schedule_csv_data));

                // Save to the categories
                db.SubmitChanges();
            }

        }

        private List<Team> parse_teams(List<string[]> csv_data) {
            List<Team> teams = new List<Team>();
            
            int i = 0;
            foreach (string[] row in csv_data) {
                if (i++ == 0) continue; // for header row
                // if betterLogo, take that one
                String logoPath = (row[5] != "") ? "BetterLogos/" + row[5] : row[4];
                teams.Add( new Team {TeamName = row[0], DisplayName = row[1], Division = row[3], LogoPath = logoPath, Color = row[6]});
            }
            
            return teams;
        }

        private List<Game> parse_games(List<string[]> csv_data){
            List<Game> games = new List<Game>();

            int i = 0;
            foreach (string[] row in csv_data) {
                if (i++ == 0) continue; // for header row
                games.Add(new Game { GameDate = DateTime.Parse(row[0]), HomeTeamName = row[1], AwayTeamName = row[2] } );
            }

            return games;
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
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