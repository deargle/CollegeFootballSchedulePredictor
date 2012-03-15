using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace CollegeFootballSeasonPredictor
{
    public class AppSettings
    {

        // The isolated storage key names of our settings
        const string ShakeToPredictKeyName = "ShakeToPredictSetting";

        // The default value of our settings
        const bool ShakeToPredictSettingDefault = true;

        public AppSettings()
        {
     
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (IsolatedStorageSettings.ApplicationSettings.Contains(Key))
            {
                // If the value has changed
                if (IsolatedStorageSettings.ApplicationSettings[Key] != value)
                {
                    // Store the new value
                    IsolatedStorageSettings.ApplicationSettings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (IsolatedStorageSettings.ApplicationSettings.Contains(Key))
            {
                value = (T)IsolatedStorageSettings.ApplicationSettings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }


        /// <summary>
        /// Property to get and set a CheckBox Setting Key.
        /// </summary>
        public bool ShakeToPredictSetting
        {
            get
            {
                return GetValueOrDefault<bool>(ShakeToPredictKeyName, ShakeToPredictSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(ShakeToPredictKeyName, value))
                {
                    Save();
                }
            }
        }

    }
}
