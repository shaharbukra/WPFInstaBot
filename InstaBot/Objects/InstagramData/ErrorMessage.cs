using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Objects.InstagramData
{
    public class ErrorMessage
    {
        public string message { get; set; }
        public bool spam { get; set; }
        public string feedback_title { get; set; }
        public string feedback_message { get; set; }
        public string feedback_url { get; set; }
        public string feedback_appeal_label { get; set; }
        public string feedback_ignore_label { get; set; }
        public string feedback_action { get; set; }
        public string status { get; set; }
    }
    
}
