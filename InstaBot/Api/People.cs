using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaBot.Helpers;
using InstaBot.Objects;
using Newtonsoft.Json;

namespace InstaBot.Api
{
    class People
    {
        public static async Task<bool> GetRecentActivityInbox(string cursorId = null)
        {
            if (await Request.SendRequestAsync("news/inbox/", null, false))
            {
                var a = InstaInfo.LastResponse;

                return true;

            }
            return false;
        }
    }
}
