using InstaBot.Api;
using InstaBot.Objects.InstagramData;
using InstaBot.Objects.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using InstaBot.Helpers;
using Newtonsoft.Json;

namespace InstaBot.Objects
{
    public class Bot : ModelBase
    {
      
        #region Data members
        private UserDetail _loggedInUser;
        private ObservableCollection<FeedItem> _userFeedData = new ObservableCollection<FeedItem>();
        private bool _isBusy;
        private bool _isBotActive;
        private int _totalLikes = 0;

        public bool IsBusy
        {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }


        public bool IsBotActive
        {
            get => _isBotActive;
            set
            {
                _isBotActive = value;
                RaisePropertyChanged();
            }
        }

        public int MediaByTagCount { get; set; } = 0;
        public List<FeedItem> MediaByTag { get; set; }

        public UserDetail LoggedInUser
        {
            get => _loggedInUser;
            private set
            {
                _loggedInUser = value;
                RaisePropertyChanged();
                RaisePropertyChanged("LoggedInUserImage");
            }
        }

        public ObservableDictionary<string, int> _nextIteration = new ObservableDictionary<string, int>()
        {
            {"Like",0 },
            {"LikeCounter",0 },
            {"Unfollow",0 },
            {"UnfollowCounter",0 },
            {"Comment",0 },
            {"CommentCounter",0 },
            {"Follow",0 },
            {"FollowCounter",0 },
        };

        public ObservableDictionary<string, int> NextIteration
        {
            get => _nextIteration;
            set
            {
                _nextIteration = value;
                RaisePropertyChanged();
            }
        }

        public int LikeTimer => NextIteration["Like"] == 0 ? 0 : NextIteration["Like"] - DateNow;

        public List<int> LongListTill1000 => Enumerable.Range(0, 1000).ToList();
        public List<int> LongListTill200 => Enumerable.Range(0, 200).ToList();

        public int SelectedMinLikes { get; set; } = 0;

        public int SelectedMaxLikes { get; set; } = 300;

        public int SelectedLikePerHour { get; set; } = 60;
        public int SelectedLikeDelayPerHour => 3600 / SelectedLikePerHour;

        public int SelectedFollowPerHour { get; set; } = 0;
        public int SelectedFollowDelayPerHour => 3600 / SelectedFollowPerHour;


        public int SelectedUnfollowPerHour { get; set; } = 0;
        public int SelectedUnFollowDelayPerHour => 3600 / SelectedUnfollowPerHour;

        public int SelectedCommentPerHour { get; set; } = 0;
        public int SelectedCommentDelayPerHour => 3600 / SelectedCommentPerHour;

        public bool TagSelected
        {
            get => InstaInfo.TagSelected;
            set
            {
                InstaInfo.TagSelected = value;
                RaisePropertyChanged();
            }
        }
        public bool GeoIdSelected
        {
            get => InstaInfo.GeoIdSelected;
            set
            {
                InstaInfo.GeoIdSelected = value;
                RaisePropertyChanged();
            }
        }

        public bool GeoNameSelected
        {
            get => InstaInfo.GeoNameSelected;
            set
            {
                InstaInfo.GeoNameSelected = value;
                RaisePropertyChanged();
            }
        }

        public FeedItem LastFeedItem => _userFeedData.Count > 0 ? UserFeedData.Last() : new FeedItem();
            //: JsonConvert.DeserializeObject<FeedItem>(
            //    "{\"taken_at\":1517904674,\"pk\":\"1708630710656856315\",\"id\":\"1708630710656856315_233992139\",\"device_timestamp\":1517904557703,\"media_type\":1,\"code\":\"Be2RpQhBnD7\",\"client_cache_key\":\"MTcwODYzMDcxMDY1Njg1NjMxNQ==.2\",\"filter_type\":112,\"image_versions2\":{\"candidates\":[{\"width\":750,\"height\":937,\"url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/1a850c8f1ab60d8e9d458f5507ca6fda/5B189093/t51.2885-15/sh0.08/e35/p750x750/26873067_2048095632133567_6997638057566404608_n.jpg?ig_cache_key=MTcwODYzMDcxMDY1Njg1NjMxNQ%3D%3D.2\"},{\"width\":240,\"height\":300,\"url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/3a7302c837ecd54a6bbc1a8458490e1d/5B1926D4/t51.2885-15/e35/p240x240/26873067_2048095632133567_6997638057566404608_n.jpg?ig_cache_key=MTcwODYzMDcxMDY1Njg1NjMxNQ%3D%3D.2\"}],\"candidate\":{\"width\":750,\"height\":937,\"url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/1a850c8f1ab60d8e9d458f5507ca6fda/5B189093/t51.2885-15/sh0.08/e35/p750x750/26873067_2048095632133567_6997638057566404608_n.jpg?ig_cache_key=MTcwODYzMDcxMDY1Njg1NjMxNQ%3D%3D.2\"}},\"original_width\":1080,\"original_height\":1350,\"user\":{\"pk\":233992139,\"username\":\"itsik92\",\"full_name\":\"Itsik Kuzenogi\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/4c722f25ad9fc05c6afcb3d4136b511e/5B19E30F/t51.2885-19/s150x150/26398996_144356082937699_8514693984814104576_n.jpg\",\"profile_pic_id\":\"1692100417158607070_233992139\",\"is_verified\":false,\"has_anonymous_profile_picture\":false,\"media_count\":0,\"geo_media_count\":0,\"follower_count\":0,\"following_count\":0,\"biography\":null,\"external_url\":null,\"can_boost_post\":false,\"can_see_organic_insights\":false,\"show_insights_terms\":false,\"can_convert_to_business\":false,\"can_create_sponsor_tags\":false,\"can_be_tagged_as_sponsor\":false,\"reel_auto_archive\":null,\"is_profile_action_needed\":false,\"usertags_count\":0,\"usertag_review_enabled\":false,\"is_needy\":false,\"has_chaining\":false,\"hd_profile_pic_versions\":null,\"hd_profile_pic_url_info\":null,\"is_business\":false,\"show_business_conversion_icon\":false,\"show_conversion_edit_entry\":false,\"aggregate_promote_engagement\":false,\"allowed_commenter_type\":null,\"is_video_creator\":false,\"has_profile_video_feed\":false,\"has_highlight_reels\":false,\"include_direct_blacklist_status\":false,\"can_follow_hashtag\":false,\"besties_count\":0,\"show_besties_badge\":false,\"auto_expand_chaining\":false},\"caption\":{\"pk\":17905481266082220,\"user_id\":233992139,\"text\":\"שלישי פעמיים כי טוב 🤗\\nזה שלישי עם חופש !\\n#חופש #אשדוד  #ישראל #שלישי #טוב #שבוע #טוב #שבועטוב  #לייק #like #ashdod #israel #ашдод  #израиль  #лайк\",\"type\":1,\"created_at\":1517904675,\"created_at_utc\":1517904675,\"content_type\":\"comment\",\"status\":\"Active\",\"bit_flags\":0,\"user\":{\"pk\":233992139,\"username\":\"itsik92\",\"full_name\":\"Itsik Kuzenogi\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/4c722f25ad9fc05c6afcb3d4136b511e/5B19E30F/t51.2885-19/s150x150/26398996_144356082937699_8514693984814104576_n.jpg\",\"profile_pic_id\":\"1692100417158607070_233992139\",\"friendship_status\":{\"following\":false,\"outgoing_request\":false,\"is_bestie\":false},\"is_verified\":false,\"has_anonymous_profile_picture\":false,\"is_unpublished\":false,\"is_favorite\":false},\"did_report_as_spam\":false,\"media_id\":1708630710656856315,\"has_translation\":true},\"caption_is_edited\":false,\"like_count\":91,\"has_liked\":false,\"top_likers\":null,\"comment_likes_enabled\":false,\"comment_threading_enabled\":true,\"has_more_comments\":true,\"next_max_id\":17910001768111570,\"max_num_visible_preview_comments\":2,\"preview_comments\":[{\"pk\":17923315261044405,\"user_id\":233992139,\"text\":\"@roeekay שלוק גם נחשב 😂\",\"type\":2,\"created_at\":1517914993,\"created_at_utc\":1517914993,\"content_type\":\"comment\",\"status\":\"Active\",\"bit_flags\":0,\"user\":{\"pk\":233992139,\"username\":\"itsik92\",\"full_name\":\"Itsik Kuzenogi\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/4c722f25ad9fc05c6afcb3d4136b511e/5B19E30F/t51.2885-19/s150x150/26398996_144356082937699_8514693984814104576_n.jpg\",\"profile_pic_id\":\"1692100417158607070_233992139\",\"is_verified\":false},\"did_report_as_spam\":false,\"media_id\":1708630710656856315,\"has_translation\":true},{\"pk\":17910001768111570,\"user_id\":3167481285,\"text\":\"איזה נשרט\",\"type\":0,\"created_at\":1517984774,\"created_at_utc\":1517984774,\"content_type\":\"comment\",\"status\":\"Active\",\"bit_flags\":0,\"user\":{\"pk\":3167481285,\"username\":\"omrynidam2\",\"full_name\":\"omry nidam\",\"is_private\":true,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/062a7d33fe7b229af8ed73dba4a62ba0/5B0F6950/t51.2885-19/s150x150/22582186_181076035801182_3212074752118095872_n.jpg\",\"profile_pic_id\":\"1629938681551220097_3167481285\",\"is_verified\":false},\"did_report_as_spam\":false,\"media_id\":1708630710656856315,\"has_translation\":true}],\"comment_count\":7,\"photo_of_you\":false,\"can_viewer_save\":true,\"organic_tracking_token\":\"eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6ImUzYWNjMDNiODIzODQ2MTliZWRkYzhmMWFlNmE5ODZlMTcwODYzMDcxMDY1Njg1NjMxNSIsInNlcnZlcl90b2tlbiI6IjE1MTc5ODcyMDY2MDl8MTcwODYzMDcxMDY1Njg1NjMxNXwzMTk5NzQyOXwwOTIwYzk0MWMxNDhmYmUyMmFiMjlmOTc2ODQzNjBmZDQxOTg1MmE4NGEwYzNhYTJjNTIyMjVjMGVhNDY0NjdhIn0sInNpZ25hdHVyZSI6IiJ9\",\"preview\":null,\"likers\":null,\"usertags\":{\"in\":[{\"user\":{\"pk\":49188784,\"username\":\"castrofashion\",\"full_name\":\"CASTRO\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/3e6b74ab4eaead34ac64e56fc64fa18c/5B0DD376/t51.2885-19/11848944_827230584032805_1057437513_a.jpg\",\"is_verified\":true},\"position\":[0.8552246,0.9111111],\"start_time_in_video_in_sec\":null,\"duration_in_video_in_sec\":null},{\"user\":{\"pk\":242236354,\"username\":\"asics\",\"full_name\":\"ASICS America\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/f1ef21b4d95a162d8d345f797edb9826/5B16C436/t51.2885-19/s150x150/20478588_939622722843855_3593631451897135104_a.jpg\",\"profile_pic_id\":\"1571203001879583329_242236354\",\"is_verified\":true},\"position\":[0.8640747,0.92890626],\"start_time_in_video_in_sec\":null,\"duration_in_video_in_sec\":null},{\"user\":{\"pk\":1310042385,\"username\":\"mjmaniajeans\",\"full_name\":\"Mania Jeans\",\"is_private\":false,\"profile_pic_url\":\"https://instagram.fhfa2-1.fna.fbcdn.net/vp/9d685146523fa2b809439e8ff28ef622/5B23179D/t51.2885-19/s150x150/17662605_417394858605258_4917581152685916160_a.jpg\",\"profile_pic_id\":\"1481735901804843829_1310042385\",\"is_verified\":false},\"position\":[0.9305556,0.86032987],\"start_time_in_video_in_sec\":null,\"duration_in_video_in_sec\":null}]},\"injected\":null,\"collapse_comments\":null,\"dominant_color\":null,\"fb_page_url\":null,\"location\":{\"pk\":215021641.0,\"name\":\"Ashdod\",\"address\":\"\",\"city\":\"\",\"short_name\":\"Ashdod\",\"lng\":34.65,\"lat\":31.8,\"external_source\":\"facebook_places\",\"facebook_places_id\":105695102798184},\"lat\":31.8,\"lng\":34.65,\"suggested_invites\":null}");
        

        public ObservableCollection<FeedItem> UserFeedData
        {
            get => _userFeedData;
            private set
            {
                _userFeedData = value;
                RaisePropertyChanged();
            }
        }
        public BitmapImage LoggedInUserImage
        {
            get
            {
                if (LoggedInUser != null)
                    return new BitmapImage(new Uri(_loggedInUser.user.hd_profile_pic_url_info.url));
                else
                    return new BitmapImage(new Uri("https://media.wired.com/photos/59268651cefba457b079a47e/2:1/w_2500,c_limit/Instagram-v051916-f.jpg"));
            }
           
        }

        public string StopWordList
        {
            get => InstaInfo.StopWordList;
            set
            {
                InstaInfo.StopWordList = value;
                RaisePropertyChanged();
            }
        }

        public string TagsList
        {
            get => InstaInfo.TagsList;
            set
            {
                InstaInfo.TagsList = value;
                RaisePropertyChanged();
            }
        }

        public string Username
        {
            get => InstaInfo.Login;
            set {
                InstaInfo.Login = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get => InstaInfo.Password;
            set
            {
                InstaInfo.Password = value;
                RaisePropertyChanged();
            }
        }

        public static int DateNow => (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        public Random Random = new Random(DateNow);
        
        #endregion Data members

        #region Ctor
        public Bot()
        {
            if (IsDesignMode)
            {
                LoggedInUser = null;
            }
            else
            {
                if (File.Exists(Environment.CurrentDirectory + "\\data\\logininfo.dat"))
                {
                    var loginDetail = File.ReadAllText(Environment.CurrentDirectory + "\\data\\logininfo.dat");
                    dynamic jsonData = JsonConvert.DeserializeObject(loginDetail);
                    InstaInfo.Login = jsonData.Login.Value;
                    InstaInfo.Password = jsonData.Password.Value;
                }
                else
                {

                    InstaInfo.Login = "username";
                    InstaInfo.Password = "password";
                }
            }
        }

        public Bot(string userName, string password)
        {
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {
                
                InstaInfo.Login = userName;
                InstaInfo.Password = password;
                LoginAsync();
            }

            //LoggedInUser = null;

        }
        #endregion Ctor

        public async Task<bool> LoginAsync()
        {
            IsBusy = true;
            UserDetail loginUserDetail = null;
            Log.WriteLog($"Try to login as {InstaInfo.Login}...");


            if (File.Exists(Environment.CurrentDirectory + $@"\data\{InstaInfo.Login}-session.dat") 
                //&&
                //System.Windows.MessageBox.Show("You have saved session, use it?", "Login method",
                //    MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes
                )
            {
                loginUserDetail = await Actions.LoginFromFile(InstaInfo.Login, InstaInfo.Password);

                if (loginUserDetail == null)
                {
                    Log.WriteLog($"Try to login with outh login");
                    loginUserDetail = await Actions.Login(InstaInfo.Login, InstaInfo.Password);

                }
                else
                {
                    Log.WriteLog($"Logged in from old session data.");

                }
                //return false;
            }
            else
            {
                loginUserDetail = await Actions.Login(InstaInfo.Login, InstaInfo.Password);
            }

            if (loginUserDetail != null)
            {
                LoggedInUser = loginUserDetail;
                IsBusy = false;
                return true;
            }
            else
            {
                MessageBox.Show("Error login");
                IsBusy = false;
                return false;
            }
        }

        public async Task StartBot()
        {
            IsBotActive = true;
            Log.WriteLog($"Start Bot!");

            while (IsBotActive)
            {
                if (NextIteration["Like"] < DateNow && SelectedLikePerHour > 0)
                {
                    NextIteration["Like"] = DateNow * 2;
                    if (await AutoModHelper("like"))
                        NextIteration["Like"] = DateNow + Convert.ToInt32(SelectedLikeDelayPerHour * 0.8 + SelectedLikeDelayPerHour * 0.2 * Random.NextDouble());
                    else
                        NextIteration["Like"] = DateNow;
                }

                RaisePropertyChanged("LikeTimer");
                await Task.Delay(1000);
            }

            MediaByTagCount = 0;
        }

        public async Task<bool> AutoModHelper(string type)
        {
            if (MediaByTagCount > 0 && InstaInfo.NumberOfLikePerTag > 0)
            {
                try
                {
                    switch (type)
                    {
                        case "like":
                            await LikeTag();
                            break;
                        case "comment":
                            await CommentTag();
                            break;
                        default:
                            break;
                    }
                    return true;

                }
                catch (Exception e)
                {
                    Log.WriteLog(e.Message);
                    throw;
                }
                
            }
            else
            {
                var tags = TagsList.Split(',');
                var selectedTag = tags[Random.Next(0, tags.Length)];
                InstaInfo.NumberOfLikePerTag = Random.Next(5, 20);

                Log.WriteLog($"Going to like {selectedTag} for {InstaInfo.NumberOfLikePerTag} times....");


                if (TagSelected)
                {
                    await GetHashtagFeed(selectedTag);
                }
                else if (GeoIdSelected)
                {
                    await GetGeoIdFeed(selectedTag);

                }
                else if (GeoNameSelected)
                {
                    await GetGeoNameFeed(selectedTag);
                }

                GetIdFromLastReponse();
            }

            return false;
        }

        private async Task<bool> LikeTag()
        {
            var tagToLike = MediaByTag[Random.Next(0, MediaByTag.Count)];
            if (tagToLike.like_count > SelectedMinLikes && tagToLike.like_count < SelectedMaxLikes)
            {
                
                Log.WriteLog($"Try to like media_id {tagToLike.pk} ....");

                if (await Actions.Like(tagToLike.pk))
                {
                    NextIteration["LikeCounter"]++;
                    UserFeedData.Add(tagToLike);
                    MediaByTag.Remove(tagToLike);
                    MediaByTagCount--;
                    InstaInfo.NumberOfLikePerTag--;

                    if (InstaInfo.NumberOfLikePerTag == 0)
                    {
                        MediaByTag.Clear();
                        MediaByTagCount = 0;
                    }

                    Log.WriteLog($"Success liking picture #{UserFeedData.Count} with url ", $"https://www.instagram.com/p/{tagToLike.code}");

                    RaisePropertyChanged("UserFeedData");
                    RaisePropertyChanged("LastFeedItem");

                    return true;
                }

                Log.WriteLog($"Failed to like with url ", $"https://www.instagram.com/p/{tagToLike.code}");
                MediaByTag.Remove(tagToLike);
                MediaByTagCount--;
            }
            else
            {
                Log.WriteLog($"Too many likes -", $"https://www.instagram.com/p/{tagToLike.code}");
            }

            return false;
        }

        private async Task<bool> CommentTag()
        {
            var tagToLike = MediaByTag[Random.Next(0, MediaByTag.Count)];
            if (tagToLike.like_count > SelectedMinLikes && tagToLike.like_count < SelectedMaxLikes)
            {

                Log.WriteLog($"Try to like media_id {tagToLike.pk} ....");

                if (await Actions.Like(tagToLike.pk))
                {
                    Log.WriteLog($"Success liking with url ", $"https://www.instagram.com/p/{tagToLike.code}");
                    NextIteration["LikeCounter"]++;
                    UserFeedData.Add(tagToLike);
                    MediaByTag.Remove(tagToLike);
                    MediaByTagCount--;

                    if (UserFeedData.Count % InstaInfo.NumberOfLikePerTag == 0)
                    {
                        MediaByTag.Clear();
                        MediaByTagCount = 0;
                    }

                    return true;
                }

                Log.WriteLog($"Failed to like with url", $"https://www.instagram.com/p/{tagToLike.code}");
                MediaByTag.Remove(tagToLike);
                MediaByTagCount--;
            }

            return false;
        }

        private void GetIdFromLastReponse()
        {
            MediaByTagCount = 0;
            try
            {
                var feedData = JsonConvert.DeserializeObject<FeedData>(InstaInfo.LastResponse);
                if (feedData.items.Count > 0)
                {
                    MediaByTag = new List<FeedItem>();
                        
                    var stopWords = StopWordList.Split(',').ToList().Select(w=>w.Trim().ToLower());
                    try
                    {
                        foreach (var item in feedData.items)
                        {
                            if (item.caption == null)
                            {
                                MediaByTag.Add(item);
                            }
                            else
                            {
                                var isContainStopWord = stopWords.Any(w => item.caption.text.ToLower().Contains(w));
                                if (!isContainStopWord)
                                {
                                    MediaByTag.Add(item);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    MediaByTagCount = MediaByTag.Count;

                    //MediaByTag = feedData.items;
                    //MediaByTagCount = feedData.items.Count;
                    Log.WriteLog("Succes! Got " + MediaByTagCount + " media_id.");
                }
                else if(feedData.ranked_items.Count>0)
                {
                    Log.WriteLog("Bad #hashtag for boting, return just ranked media_id.");

                }

            }
            catch (Exception e)
            {
                Log.WriteLog("Error! Can't get media_id...");
            }
        }

        private async Task<bool> GetHashtagFeed(string hashtag, string maxid = null)
        {
            hashtag = hashtag.Trim();
            Log.WriteLog($"Get media_id by tag: #{hashtag}.");
            var endpoint = (maxid != null) ? 
                ("feed/tag/" + hashtag + "/?max_id=" + maxid + "&rank_token=" + InstaInfo.RankToken + "&ranked_content=true&") : 
                ("feed/tag/" + hashtag + "/?rank_token=" + InstaInfo.RankToken+ "&ranked_content=true&");

            if (await Request.SendRequestAsync(endpoint, null, false))
            {
                return true;
            }
            return false;
        }

        private async Task<bool> GetGeoIdFeed(string hashtag, string maxid = null)
        {
            hashtag = hashtag.Trim();
            Log.WriteLog($"Get media_id by GeoId #{hashtag}.");
            var endpoint = (maxid != null) ?
                ("feed/location/" + hashtag + "/?max_id=" + maxid + "&rank_token=" + InstaInfo.RankToken + "&ranked_content=true&") :
                ("feed/location/" + hashtag + "/?rank_token=" + InstaInfo.RankToken + "&ranked_content=true&");

            if (await Request.SendRequestAsync(endpoint, null, false))
            {
                return true;
            }
            return false;
        }

        private async Task<bool> GetGeoNameFeed(string hashtag, string maxid = null)
        {
            hashtag = hashtag.Trim();
            Log.WriteLog($"Get media_id by Geo Name: #{hashtag}.");
            var endpoint = "fbsearch/places/?rank_token=" + InstaInfo.RankToken + "&query=" + hashtag;
            if (await Request.SendRequestAsync(endpoint, null, false))
            {
                return true;
            }
            return false;
        }

        private async Task GetSelfFeedAsync()
        {
            IsBusy = true;

            var feed = await Actions.GetSelfTimelineFeed();

            //var feed = await Actions.GetSelfFeed();
            if (feed != null)
            {
                feed.items.ForEach(i=> UserFeedData.Add(i));
                
                while (UserFeedData.Count < InstaInfo.FeedImages)
                {
                    feed = await Actions.GetSelfTimelineFeed(feed.next_max_id);
                    feed.items.ForEach(i => UserFeedData.Add(i));

                }
            }
            IsBusy = false;

        }
    }
}
