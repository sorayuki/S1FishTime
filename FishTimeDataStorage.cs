using System;
using System.IO;
using System.Collections.Generic;

namespace FishTime
{
    class FishTimeDataStorage
    {
        static FishTimeDataStorage instance = new FishTimeDataStorage();

        public static FishTimeDataStorage Instance
        {
            get
            {
                return instance;
            }
        }

        private string GetFile(DateTime timestamp)
        {
            string filename = timestamp.ToString("yyyyMMdd") + ".csv";
            filename = Path.Combine(GlobalConfig.Instance.StorageDir, filename);

            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, @"""Window"", ""Process"", ""TimeStamp""" + Environment.NewLine);
            }

            return filename;
        }

        private string CSVEncode(string input)
        {
            return @"""" + input.Replace(@"""", @"""""") + @"""";
        }

        public bool AppendRecord(FishData fishData)
        {
            string file = GetFile(fishData.TimeStamp);
            string toAppend = CSVEncode(fishData.WindowTitle) + "," 
                + CSVEncode(fishData.ProcessName) + ","
                + CSVEncode((fishData.TimeStamp - new DateTime(1970, 1, 1)).TotalSeconds.ToString()) 
                + Environment.NewLine;
            File.AppendAllText(file, toAppend);
            return true;
        }
    }
}
