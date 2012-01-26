using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;

using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone;
using System.Windows.Resources;
using System.Windows.Media.Imaging;

namespace SchedulePredictorDatabaseHelper
{
    public class CSVParser  
    {
        public List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();
            try
            {
                
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
                bool exists = isoStore.FileExists(path);
                
                using (StreamReader readFile = new StreamReader(App.GetResourceStream(new Uri(path, UriKind.Relative)).Stream))
                {
                    
                    string line;
                    string[] row;
                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return parsedData;
        }
    }
}