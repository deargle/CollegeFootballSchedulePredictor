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
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace CollegeFootballSeasonPredictor
{
    public class ShakeDetection
    {
        static Vector3 previousData;
        static int numShakes;
        static bool PreviouslyShaking = false;

        // Two properties for controlling the algorith
        public static int RequiredConsecutiveShakes { get; set; }
        public static double Threshold { get; set; }

        static ShakeDetection()
        {
            RequiredConsecutiveShakes = 1;
            Threshold = .7;
        }

        // Call this with teh accelerometer data
        public static bool JustShook(Vector3 e)
        {
            if (previousData != null)
            {
                if (IsShaking(previousData, e, Threshold) && !PreviouslyShaking)
                {
                    // just started shaking
                    numShakes = 0;
                    PreviouslyShaking = true;
                }
                else if (IsShaking(previousData, e, Threshold) && PreviouslyShaking)
                {
                    // Still just shaking away like a madman, wait patiently
                    numShakes++;
                }
                else if (!IsShaking(previousData, e, .2) & PreviouslyShaking)
                {
                    // HOT DOG THEY'RE DONE
                    PreviouslyShaking = false;
                    numShakes = 0;
                    return true;
                }
                else if (!IsShaking(previousData, e, .2) && !PreviouslyShaking)
                {
                    // Nothing happened and nothing has been happening?
                    PreviouslyShaking = false;
                    numShakes = 0;
                }
            }

            previousData = e;
            return false;
        }

        // It's a shake if the values in at least two dimensions 
        // are different enough from the previous values
        static bool IsShaking(Vector3 previous, Vector3 current, double threshold)
        {
            double deltaX = Math.Abs(previous.X - current.X);
            double deltaY = Math.Abs(previous.Y - current.Y);
            double deltaZ = Math.Abs(previous.Z - current.Z);

            return (deltaX > threshold && deltaY > threshold) ||
                (deltaY > threshold && deltaZ > threshold) ||
                (deltaX > threshold && deltaZ > threshold);
        }
    }
}
