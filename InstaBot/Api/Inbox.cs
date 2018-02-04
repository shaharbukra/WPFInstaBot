using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaBot.Callbacks;
using InstaBot.Helpers;
using InstaBot.Objects;
using Newtonsoft.Json;

namespace InstaBot.Api
{
    class Inbox
    {
      

        public static async Task<bool> GetInboxThread(string threadId)
        {
            var inboxData = new Dictionary<string, string>
            {
                {"use_unified_inbox","true"}
            };

            var data = JsonConvert.SerializeObject(inboxData).ToString();

            if (await Request.SendRequestAsync($"direct_v2/threads/{threadId}/", GenerateData.Signature(data), false))
            {
                var arg = JsonConvert.DeserializeObject<Objects.InstagramData.ThreadData>(InstaInfo.LastResponse);
                if (arg != null)
                {
                    CallBackInbox.CallbackEventHandler(arg, "thread");
                }
                return true;

            }
            return false;
        }
    
    }
}
