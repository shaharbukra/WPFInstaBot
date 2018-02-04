using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Objects.InstagramData
{
    public class HdProfilePicVersion
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class HdProfilePicUrlInfo
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class User
    {
        public long pk { get; set; }
        public string username { get; set; } 
        public string full_name { get; set; }
        public bool is_private { get; set; }
        public string profile_pic_url { get; set; }
        public string profile_pic_id { get; set; }
        public bool is_verified { get; set; }
        public bool has_anonymous_profile_picture { get; set; }
        public int media_count { get; set; }
        public int geo_media_count { get; set; }
        public int follower_count { get; set; }
        public int following_count { get; set; }
        public string biography { get; set; }
        public string external_url { get; set; }
        public bool can_boost_post { get; set; }
        public bool can_see_organic_insights { get; set; }
        public bool show_insights_terms { get; set; }
        public bool can_convert_to_business { get; set; }
        public bool can_create_sponsor_tags { get; set; }
        public bool can_be_tagged_as_sponsor { get; set; }
        public string reel_auto_archive { get; set; }
        public bool is_profile_action_needed { get; set; }
        public int usertags_count { get; set; }
        public bool usertag_review_enabled { get; set; }
        public bool is_needy { get; set; }
        public bool has_chaining { get; set; }
        public List<HdProfilePicVersion> hd_profile_pic_versions { get; set; }
        public HdProfilePicUrlInfo hd_profile_pic_url_info { get; set; }
        public bool is_business { get; set; }
        public bool show_business_conversion_icon { get; set; }
        public bool show_conversion_edit_entry { get; set; }
        public bool aggregate_promote_engagement { get; set; }
        public string allowed_commenter_type { get; set; }
        public bool is_video_creator { get; set; }
        public bool has_profile_video_feed { get; set; }
        public bool has_highlight_reels { get; set; }
        public bool include_direct_blacklist_status { get; set; }
        public bool can_follow_hashtag { get; set; }
        public int besties_count { get; set; }
        public bool show_besties_badge { get; set; }
        public bool auto_expand_chaining { get; set; }
    }

    public class UserDetail
    {
        public User user { get; set; }
        public string status { get; set; }
    }
}
