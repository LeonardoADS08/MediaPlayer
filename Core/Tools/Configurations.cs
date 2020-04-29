using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MediaPlayer.Core.Tools
{
    public class Configurations
    {
        #region Singleton
        private static Configurations _instance;
        public static Configurations Instance
        {
            get
            {
                if (_instance == null) _instance = Instantiate();
                return _instance;
            }
        }


        private static Configurations Instantiate()
        {
            if (File.Exists(FILE_DIRECTION))
            {
                using (var reader = new StreamReader(FILE_DIRECTION))
                {
                    var json = reader.ReadToEnd();
                    Configurations configurations = JsonConvert.DeserializeObject<Configurations>(json);
                    return configurations;
                }
            }
            else
            {
                Tools.Logs.SaveLog("Configuration file not found", Tools.Logs.GetDirection());
                throw new Exception("Configuration file not found.");
            }
        }

        #endregion

        public static string FILE_DIRECTION => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configurations.json");

        public bool Debug;
        public bool Logs;
        public List<string> Folders;
        public List<string> AllowedExtensions;
        public string DBName;

        public string DBDirectory => Path.Combine(AppContext.BaseDirectory, DBName);

    }
}
