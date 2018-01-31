using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaBot.Objects.InstagramData;

namespace InstaBot.Callbacks
{
    public static class CallBackInbox
    {
        public delegate void CallbackEvent(object data, string type);

        public static CallbackEvent CallbackEventHandler;
    }
}
