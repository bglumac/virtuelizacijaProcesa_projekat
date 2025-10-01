using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    internal class Logger
    {
        public enum LogLevel
        {
            INFO,
            WARN,
            EVNT,
            FAIL
        }

        string fileName { get; set; }

        private static StreamWriter sw;
        static Logger()
        {
            FileStream fs = new FileStream("logs.csv", FileMode.Create, FileAccess.ReadWrite);
            sw = new StreamWriter(fs);
        }

        public static void Log(string message, LogLevel level)
        {
            sw.WriteLine($"[{level.ToString()}][{DateTime.Now.ToString("dd/MM/yyyy => ")}] {message}");
            sw.Flush();
        }

        public static void Dispose()
        {
            if (sw != null)
            {
                sw.Dispose();
                sw = null;
            }
        }

    }
}
