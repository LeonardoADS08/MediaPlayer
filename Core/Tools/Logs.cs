using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Core.Tools
{

    /// <summary>
    /// Log system
    /// </summary>
    public static class Logs
    {
        private static bool _started = false;
        public const string DEFAULT_LOG = "[Default]";

        /// <summary>
        /// Save a log
        /// </summary>
        /// <param name="message">Message to be saved</param>
        /// <param name="direction">Direction of the message</param>
        /// <param name="chronology">Show time</param>
        /// <param name="logName">Log file name</param>
        public static void SaveLog(string message, string direction, bool chronology = true, string logName = DEFAULT_LOG)
        {
            try
            {
                string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(string.Format("{0} - {1}", logName, "Log {0}.log"), DateTime.Today.ToString("dd-MM-yyyy")));
                if (_started)
                {
                    string text = string.Format("{0}{0}______________________________ Forge - Execution Log: {1} ______________________________{0}{0}", Environment.NewLine, DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                    File.AppendAllText(logFile, text);
                    _started = true;
                }

                string final = "";
                if (chronology) final = string.Format("{0}: {1} - {2}{3}{3}", DateTime.Now.ToString("hh:mm:ss"), direction, message, Environment.NewLine);
                else final = string.Format("{0} - {1}{2}{2}", direction, message, Environment.NewLine);

                File.AppendAllText(logFile, final);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Get the name of the funciont with Reflection
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetDirection()
        {
            try
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                return string.Format("{0}.{1}", sf.GetMethod().DeclaringType.Name, sf.GetMethod().Name);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "Direction not found.";
            }
        }
    }
}