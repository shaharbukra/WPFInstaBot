using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Callbacks
{
    public static class CallBackLog
    {
        public delegate void callbackEvent(string log, string url);

        public static callbackEvent CallbackEventHandler;
    }
}
