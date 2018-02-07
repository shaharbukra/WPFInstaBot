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
            var urlString = url ?? string.Empty;
            string writeLog = DateTime.Now + ". " + log + urlString;
            new FileInfo(Environment.CurrentDirectory + "\\data\\").Directory.Create();
            File.AppendAllText($@"{Environment.CurrentDirectory}\data\{InstaInfo.Login}-{DateTime.Now:dd.MM.yyyy}-log.txt", writeLog + Environment.NewLine);
            CallBackLog.CallbackEventHandler(DateTime.Now + ". " + log, url);
        }
    }
}
