using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDataApp
{
    public static class Logger
    {
        private static string logFilePath = @"C:\LoadDataLog.txt";

        public static void WriteToLog(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, string.Format("{0}{1} - {2}", System.Environment.NewLine, DateTime.Now, message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
