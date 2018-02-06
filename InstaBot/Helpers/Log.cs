using InstaBot.Callbacks;
using InstaBot.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Helpers
{
    public class Log
    {
        public static void WriteLog(string log, string url=null)
        {
            string writeLog = DateTime.Now + ". " + log;
            new FileInfo(Environment.CurrentDirectory + "\\data\\").Directory.Create();
            File.AppendAllText(Environment.CurrentDirectory + "\\data\\" + InstaInfo.Login + "-log.txt", writeLog + Environment.NewLine);
            CallBackLog.CallbackEventHandler(writeLog, url);
        }
    }
}
