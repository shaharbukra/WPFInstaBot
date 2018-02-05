﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace InstaBot.Objects
{
    /// <summary>
    /// Defines the <see cref="InstaInfo" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public static  class InstaInfo
    {
        internal static string SigKeyVersion = "4";
        public static int FeedImages => 20;
        public static string ApiUrl => "https://i.instagram.com/api/v1/";
        public static string IgSigKey => "b4946d296abf005163e72346a6d33dd083cadde638e6ad9c5eb92e381b35784a";
        public static string UserAgent => "Instagram 12.0.0.7.91 Android (18/4.3; 320dpi; 720x1280; Xiaomi; HM 1SW; armani; qcom; en_US)";
        public static int BanSleepTime => 7200;
        public static int SurfaceParam = 5095;

        public static int Error400ToBan => 3;
        public static string Experiments => "ig_android_ad_holdout_16m5_universe,ig_android_progressive_jpeg,ig_creation_growth_holdout,ig_android_oppo_app_badging,ig_android_ad_remove_username_from_caption_universe,ig_android_enable_share_to_whatsapp,ig_android_direct_drawing_in_quick_cam_universe,ig_android_ad_always_send_ad_attribution_id_universe,ig_android_universe_video_production,ig_android_direct_plus_button,ig_android_ads_heatmap_overlay_universe,ig_android_http_stack_experiment_2016,ig_android_infinite_scrolling,ig_fbns_blocked,ig_android_post_auto_retry_v7_21,ig_fbns_push,ig_android_video_playback_bandwidth_threshold,ig_android_direct_link_preview,ig_android_direct_typing_indicator,ig_android_preview_capture,ig_android_feed_pill,ig_android_profile_link_iab,ig_android_story_caption,ig_android_network_cancellation,ig_android_histogram_reporter,ig_android_anrwatchdog,ig_android_search_client_matching,ig_android_follow_request_text_buttons,ig_android_feed_zoom,ig_android_drafts_universe,ig_android_disable_comment,ig_android_user_detail_endpoint,ig_android_os_version_blocking,ig_android_blocked_list,ig_android_event_creation,ig_android_high_res_upload_2,ig_android_2fac,ig_android_mark_reel_seen_on_Swipe_forward,ig_android_comment_redesign,ig_android_ad_sponsored_label_universe,ig_android_mentions_dismiss_rule,ig_android_disable_chroma_subsampling,ig_android_share_spinner,ig_android_video_reuse_surface,ig_explore_v3_android_universe,ig_android_media_favorites,ig_android_nux_holdout,ig_android_insta_video_universe,ig_android_search_null_state,ig_android_universe_reel_video_production,liger_instagram_android_univ,ig_android_direct_emoji_picker,ig_feed_holdout_universe,ig_android_direct_send_auto_retry_universe,ig_android_samsung_app_badging,ig_android_disk_usage,ig_android_business_promotion,ig_android_direct_swipe_to_inbox,ig_android_feed_reshare_button_nux,ig_android_react_native_boost_post,ig_android_boomerang_feed_attribution,ig_fbns_shared,ig_fbns_dump_ids,ig_android_react_native_universe,ig_show_promote_button_in_feed,ig_android_ad_metadata_behavior_universe,ig_android_video_loopcount_int,ig_android_inline_gallery_backoff_hours_universe,ig_android_rendering_controls,ig_android_profile_photo_as_media,ig_android_async_stack_image_cache,ig_video_max_duration_qe_preuniverse,ig_video_copyright_whitelist,ig_android_render_stories_with_content_override,ig_android_ad_intent_to_highlight_universe,ig_android_swipe_navigation_x_angle_universe,ig_android_disable_comment_public_test,ig_android_profile,ig_android_direct_blue_tab,ig_android_enable_share_to_messenger,ig_android_fetch_reel_tray_on_resume_universe,ig_android_promote_again,ig_feed_event_landing_page_channel,ig_ranking_following,ig_android_pending_request_search_bar,ig_android_feed_ufi_redesign,ig_android_pending_edits_dialog_universe,ig_android_business_conversion_flow_universe,ig_android_show_your_story_when_empty_universe,ig_android_ad_drop_cookie_early,ig_android_app_start_config,ig_android_fix_ise_two_phase,ig_android_ppage_toggle_universe,ig_android_pbia_normal_weight_universe,ig_android_profanity_filter,ig_ios_su_activity_feed,ig_android_search,ig_android_boomerang_entry,ig_android_mute_story,ig_android_inline_gallery_universe,ig_android_ad_remove_one_tap_indicator_universe,ig_android_view_count_decouple_likes_universe,ig_android_contact_button_redesign_v2,ig_android_periodic_analytics_upload_v2,ig_android_send_direct_typing_indicator,ig_android_ad_holdout_16h2m1_universe,ig_android_react_native_comment_moderation_settings,ig_video_use_sve_universe,ig_android_inline_gallery_no_backoff_on_launch_universe,ig_android_immersive_viewer,ig_android_discover_people_icon,ig_android_profile_follow_back_button,is_android_feed_seen_state,ig_android_dense_feed_unit_cards,ig_android_drafts_video_universe,ig_android_exoplayer,ig_android_add_to_last_post,ig_android_ad_remove_cta_chevron_universe,ig_android_ad_comment_cta_universe,ig_android_search_event_icon,ig_android_channels_home,ig_android_feed,ig_android_dv2_realtime_private_share,ig_android_non_square_first,ig_android_video_interleaved_v2,ig_android_video_cache_policy,ig_android_react_native_universe_kill_switch,ig_android_video_captions_universe,ig_android_follow_search_bar,ig_android_last_edits,ig_android_two_step_capture_flow,ig_android_video_download_logging,ig_android_share_link_to_whatsapp,ig_android_facebook_twitter_profile_photos,ig_android_swipeable_filters_blacklist,ig_android_ad_pbia_profile_tap_universe,ig_android_use_software_layer_for_kc_drawing_universe,ig_android_react_native_ota,ig_android_direct_mutually_exclusive_experiment_universe,ig_android_following_follower_social_context";
        public static int DateNow => (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        public static bool Proxy { get; internal set; }
        public static string ProxyIp { get; internal set; }
        public static string ProxyPort { get; internal set; }
        public static string ProxyUser { get; internal set; }
        public static string ProxyPassword { get; internal set; }
        public static CookieContainer CookieContainer { get; internal set; }
        public static string CsrfToken { get; internal set; }
        public static string UserNameId { get; internal set; }
        public static string SessionId { get; internal set; }
        public static string Mid { get; internal set; }
        public static string LastResponse { get; internal set; }
        public static bool LoginStatus { get; internal set; }
        public static string RankToken { get; internal set; }
        public static string Uuid { get; internal set; }
        [JsonProperty]
        public static string Login { get; set; }
        [JsonProperty]
        public static string Password { get; internal set; }
        public static string DeviceId { get; internal set; }
        //public static string TagsList { get; set; } = "ballerina,dance, ballet, pointe,ig_israel,israel,wolford,ballerinaproject_,ballerina,ballerinaproject,insta_global,ישראל";
        public static string TagsList { get; set; } = "ישראל,תלאביב,בוקרטוב,בוקר,ים,ירושלים";
        public static string StopWordList { get; set; } = "sex, porno, money, trade, fuck, pussy";
        public static bool GeoNameSelected { get; set; } = false;
        public static bool GeoIdSelected { get; set; } = false;
        public static bool TagSelected { get; set; } = true;

      

    }
}
