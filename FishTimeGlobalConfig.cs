using System;
using System.IO;
using System.Windows.Forms;

namespace FishTime
{
    class GlobalConfig
    {
        GlobalConfig()
        {
            storageDir = Application.StartupPath;
        }

        static GlobalConfig instance = new GlobalConfig();

        static public GlobalConfig Instance
        {
            get
            {
                return instance;
            }
        }

        string storageDir;
        public string StorageDir
        {
            get
            {
                return storageDir;
            }
        }
    }
}
