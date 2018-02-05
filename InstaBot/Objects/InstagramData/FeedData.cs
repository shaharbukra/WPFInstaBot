using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Objects.InstagramData
{
    public class Candidate
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class ImageVersions2
    {
        public List<Candidate> candidates { get; set; }

        public Candidate candidate => candidates.Count > 0 ? candidates[0] : null;
    }

    //public class FriendshipStatus
    //{
    //    public bool following { get; set; }
    //    public bool outgoing_request { get; set; }
    //    public bool is_bestie { get; set; }
    //}

    //public class User
    //{
    //    public object pk { get; set; }
    //    public string username { get; set; }
    //    public string full_name { get; set; }
    //    public bool is_private { get; set; }
    //    public string profile_pic_url { get; set; }
    //    public string profile_pic_id { get; set; }
    //    public FriendshipStatus friendship_status { get; set; }
    //    public bool is_verified { get; set; }
    //    public bool has_anonymous_profile_picture { get; set; }
    //    public bool is_unpublished { get; set; }
    //    public bool is_favorite { get; set; }
    //}

    public class FriendshipStatus2
    {
        public bool following { get; set; }
        public bool outgoing_request { get; set; }
        public bool is_bestie { get; set; }
    }

    public class User2
    {
        public object pk { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public bool is_private { get; set; }
        public string profile_pic_url { get; set; }
        public string profile_pic_id { get; set; }
        public FriendshipStatus2 friendship_status { get; set; }
        public bool is_verified { get; set; }
        public bool has_anonymous_profile_picture { get; set; }
        public bool is_unpublished { get; set; }
        public bool is_favorite { get; set; }
    }

    public class Caption
    {
        public object pk { get; set; }
        public object user_id { get; set; }
        public string text { get; set; }
        public int type { get; set; }
        public int created_at { get; set; }
        public int created_at_utc { get; set; }
        public string content_type { get; set; }
        public string status { get; set; }
        public int bit_flags { get; set; }
        public User2 user { get; set; }
        public bool did_report_as_spam { get; set; }
        public object media_id { get; set; }
        public bool? has_translation { get; set; }
    }

    public class Liker
    {
        public long pk { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public bool is_private { get; set; }
        public string profile_pic_url { get; set; }
        public string profile_pic_id { get; set; }
        public bool is_verified { get; set; }
    }

    public class Usertags
    {
        public List<object> @in { get; set; }
    }

    public class HideReasonsV2
    {
        public string text { get; set; }
        public string reason { get; set; }
    }

    public class Injected
    {
        public string label { get; set; }
        public bool show_icon { get; set; }
        public string hide_label { get; set; }
        public object invalidation { get; set; }
        public bool is_demo { get; set; }
        public List<object> view_tags { get; set; }
        public bool is_holdout { get; set; }
        public string tracking_token { get; set; }
        public bool show_ad_choices { get; set; }
        public string ad_title { get; set; }
        public string about_ad_params { get; set; }
        public bool direct_share { get; set; }
        public long ad_id { get; set; }
        public bool display_viewability_eligible { get; set; }
        public List<HideReasonsV2> hide_reasons_v2 { get; set; }
        public int hide_flow_type { get; set; }
        public List<string> cookies { get; set; }
    }

    public class Location
    {
        public double pk { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string short_name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string external_source { get; set; }
        public object facebook_places_id { get; set; }
    }

    public class Suggestion
    {
        public string fb_id { get; set; }
        public string full_name { get; set; }
        public string profile_pic_url { get; set; }
        public bool is_invited { get; set; }
        public int invite_time { get; set; }
        public double value { get; set; }
        public double ranking_score { get; set; }
        public bool down_ranked { get; set; }
    }

    public class SuggestedInvites
    {
        public int type { get; set; }
        public List<Suggestion> suggestions { get; set; }
        public string title { get; set; }
        public string view_all_text { get; set; }
        public string id { get; set; }
        public string tracking_token { get; set; }
    }

    public class RankedItem
    {
        public int taken_at { get; set; }
        public object pk { get; set; }
        public string id { get; set; }
        public object device_timestamp { get; set; }
        public int media_type { get; set; }
        public string code { get; set; }
        public string client_cache_key { get; set; }
        public int filter_type { get; set; }
        public ImageVersions2 image_versions2 { get; set; }
        public int original_width { get; set; }
        public int original_height { get; set; }
        public Location location { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public User user { get; set; }
        public Caption caption { get; set; }
        public bool caption_is_edited { get; set; }
        public int like_count { get; set; }
        public bool has_liked { get; set; }
        public bool comment_likes_enabled { get; set; }
        public bool comment_threading_enabled { get; set; }
        public bool has_more_comments { get; set; }
        public object next_max_id { get; set; }
        public int max_num_visible_preview_comments { get; set; }
        public List<object> preview_comments { get; set; }
        public int comment_count { get; set; }
        public bool photo_of_you { get; set; }
        public Usertags usertags { get; set; }
        public bool can_viewer_save { get; set; }
        public string organic_tracking_token { get; set; }
        public List<CarouselMedia> carousel_media { get; set; }
        public List<VideoVersion> video_versions { get; set; }
        public bool? has_audio { get; set; }
        public double? video_duration { get; set; }
        public double? view_count { get; set; }
        public int? is_dash_eligible { get; set; }
        public string video_dash_manifest { get; set; }
        public int? number_of_qualities { get; set; }
    }

    public class VideoVersion
    {
        public int type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

    public class CarouselMedia
    {
        public string id { get; set; }
        public int media_type { get; set; }
        public ImageVersions2 image_versions2 { get; set; }
        public int original_width { get; set; }
        public int original_height { get; set; }
        public object pk { get; set; }
        public string carousel_parent_id { get; set; }
        public object usertags { get; set; }
    }

    public class FeedItem
    {
        public int taken_at { get; set; }
        public string pk { get; set; }
        public string id { get; set; }
        public object device_timestamp { get; set; }
        public int media_type { get; set; }
        public string code { get; set; }
        public string client_cache_key { get; set; }
        public int filter_type { get; set; }
        public ImageVersions2 image_versions2 { get; set; }
        public int original_width { get; set; }
        public int original_height { get; set; }
        public User user { get; set; }
        public Caption caption { get; set; }
        public bool caption_is_edited { get; set; }
        public int like_count { get; set; }
        public bool has_liked { get; set; }
        public List<object> top_likers { get; set; }
        public bool comment_likes_enabled { get; set; }
        public bool comment_threading_enabled { get; set; }
        public bool has_more_comments { get; set; }
        public object next_max_id { get; set; }
        public int max_num_visible_preview_comments { get; set; }
        public List<object> preview_comments { get; set; }
        public int comment_count { get; set; }
        public bool photo_of_you { get; set; }
        public bool can_viewer_save { get; set; }
        public string organic_tracking_token { get; set; }
        public string preview { get; set; }
        public List<Liker> likers { get; set; }
        public Usertags usertags { get; set; }
        public Injected injected { get; set; }
        public bool? collapse_comments { get; set; }
        public string dominant_color { get; set; }
        public string fb_page_url { get; set; }
        public Location location { get; set; }
        public double? lat { get; set; }
        public double? lng { get; set; }
        public SuggestedInvites suggested_invites { get; set; }
    }
    public class Owner
    {
        public string type { get; set; }
        public string pk { get; set; }
        public string name { get; set; }
        public string profile_pic_url { get; set; }
        public string profile_pic_username { get; set; }
    }
    public class Story
    {
        public string id { get; set; }
        public int latest_reel_media { get; set; }
        public int expiring_at { get; set; }
        public object seen { get; set; }
        public bool can_reply { get; set; }
        public bool can_reshare { get; set; }
        public string reel_type { get; set; }
        public Owner owner { get; set; }
        public List<Item> items { get; set; }
        public int prefetch_count { get; set; }
        public long unique_integer_reel_id { get; set; }
        public bool muted { get; set; }
    }
    public class FeedData
    {
        public List<RankedItem> ranked_items { get; set; }
        public List<FeedItem> items { get; set; }
        public int num_results { get; set; }
        public bool more_available { get; set; }
        public bool auto_load_more_enabled { get; set; }
        public bool is_direct_v2_enabled { get; set; }
        public string next_max_id { get; set; }
        public Story story { get; set; }

        public string status { get; set; }
    }
    
}
