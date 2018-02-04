using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaBot.Callbacks;
using InstaBot.Helpers;
using InstaBot.Objects;
using Newtonsoft.Json;

namespace InstaBot.Api
{
    class Direct
    {
        private static int inboxcount = 0;


        public static async Task<bool> GetRankedRecipients(string mode, bool showThreads, string query = null)
        {
            var dicUserData = new Dictionary<string, string>
            {
                { "mode", InstaInfo.Uuid },
                { "show_threads", InstaInfo.UserNameId },
                { "use_unified_inbox", showThreads ? "true" : "false" },
            };
            if (!string.IsNullOrEmpty(query))
            {
                dicUserData.Add("query", query);
            }

            string jsonData = JsonConvert.SerializeObject(dicUserData).ToString();
            if (await Request.SendRequestAsync("direct_v2/ranked_recipients/", GenerateData.Signature(jsonData), false))
            {
                var a = InstaInfo.LastResponse;

                return true;

            }
            return false;
        }

        public static async Task<bool> GetInbox(string cursorId = null)
        {
            var type = "inbox";
            var inboxData = new Dictionary<string, string>
            {
                {"persistentBadging","true"},
                {"use_unified_inbox","true"}
            };

            if (cursorId != null)
            {
                type = "inbox2";
                inboxData.Add("cursor", cursorId);
            }

            var data = JsonConvert.SerializeObject(inboxData).ToString();

            if (await Request.SendRequestAsync("direct_v2/inbox/", GenerateData.Signature(data), false))
            {
                var arg = JsonConvert.DeserializeObject<Objects.InstagramData.InboxData>(InstaInfo.LastResponse);
                inboxcount += arg.inbox.threads.Count;
                if (arg != null)
                {
                    CallBackInbox.CallbackEventHandler(arg, type);

                    //if (inboxcount < 100 && arg.inbox.oldest_cursor != null)
                    //    await GetInbox(arg.inbox.oldest_cursor);
                }


                return true;

            }
            return false;
        }
    }
}
