using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Callbacks
{
    public static class CallBackMedia
    {
        public delegate void callbackEvent(object media, string type);

        public static callbackEvent CallbackEventHandler;
    }
}
