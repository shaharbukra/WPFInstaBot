using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Callbacks
{
    public static class CallBackTime
    {
        public delegate void CallbackEvent(Dictionary<string, int> next);

        public static CallbackEvent callbackEventHandler;
    }
}
