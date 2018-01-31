using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Objects.InstagramData
{
    public class FriendshipStatus
    {
        public bool following { get; set; }
        public bool blocking { get; set; }
        public bool is_private { get; set; }
        public bool incoming_request { get; set; }
        public bool outgoing_request { get; set; }
        public bool is_bestie { get; set; }
    }

    public class InboxUser
    {
        public object pk { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public bool is_private { get; set; }
        public string profile_pic_url { get; set; }
        public FriendshipStatus friendship_status { get; set; }
        public bool is_verified { get; set; }
        public bool has_anonymous_profile_picture { get; set; }
        public string profile_pic_id { get; set; }
    }

    public class Bold
    {
        public int start { get; set; }
        public int end { get; set; }
    }

    public class ActionLog
    {
        public string description { get; set; }
        public List<Bold> bold { get; set; }
    }

    public class Item
    {
        public string item_id { get; set; }
        public object user_id { get; set; }
        public object timestamp { get; set; }
        public string item_type { get; set; }
        public string text { get; set; }
        public string client_context { get; set; }
        public int? hide_in_thread { get; set; }
        public ActionLog action_log { get; set; }
        public object reactions { get; set; }
        public object media_share { get; set; }
    }

    public class Inviter
    {
        public object pk { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public bool is_private { get; set; }
        public string profile_pic_url { get; set; }
        public bool is_verified { get; set; }
        public bool has_anonymous_profile_picture { get; set; }
        public string reel_auto_archive { get; set; }
        public string profile_pic_id { get; set; }
    }

    public class __invalid_type__31997429
    {
        public string timestamp { get; set; }
        public string item_id { get; set; }
    }

    public class LastSeenAt
    {
        public object __invalid_name__31997429 { get; set; }
    }

    public class Thread
    {
        public string thread_id { get; set; }
        public List<InboxUser> users { get; set; }
        public List<object> left_users { get; set; }
        public List<Item> items { get; set; }
        public object last_activity_at { get; set; }
        public bool muted { get; set; }
        public bool is_pin { get; set; }
        public bool named { get; set; }
        public bool canonical { get; set; }
        public bool pending { get; set; }
        public string thread_type { get; set; }
        public int viewer_id { get; set; }
        public string thread_title { get; set; }
        public object pending_score { get; set; }
        public int reshare_send_count { get; set; }
        public int reshare_receive_count { get; set; }
        public int expiring_media_send_count { get; set; }
        public int expiring_media_receive_count { get; set; }
        public Inviter inviter { get; set; }
        public bool has_older { get; set; }
        public bool has_newer { get; set; }
        public LastSeenAt last_seen_at { get; set; }
        public string newest_cursor { get; set; }
        public string oldest_cursor { get; set; }
        public bool is_spam { get; set; }
    }

    public class Inbox
    {
        public List<Thread> threads { get; set; }
        public bool has_older { get; set; }
        public int unseen_count { get; set; }
        public long unseen_count_ts { get; set; }
        public string oldest_cursor { get; set; }
    }

    public class InboxData
    {
        public Inbox inbox { get; set; }
        public int seq_id { get; set; }
        public int pending_requests_total { get; set; }
        public List<object> pending_requests_users { get; set; }
        public string status { get; set; }
    }

    public class ThreadData
    {
        public Thread thread { get; set; }
        public string status { get; set; }
    }
}
