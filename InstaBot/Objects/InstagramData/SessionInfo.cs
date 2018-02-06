using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Objects.InstagramData
{
    public class SessionInfo
    {
        public string login { get; set; }
        public string password { get; set; }
        public string username_id { get; set; }
        public string csrftoken { get; set; }
        public string uuid { get; set; }
        public string device_id { get; set; }
        public string rank_token { get; set; }
        public string mid { get; set; }
        public string sessionid { get; set; }
    }
}
