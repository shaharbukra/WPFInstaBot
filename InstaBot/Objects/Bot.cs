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


        public List<int> LongListTill1000 => Enumerable.Range(0, 1000).ToList();
        public List<int> LongListTill200 => Enumerable.Range(0, 200).ToList();

        public int SelectedMinLikes { get; set; } = 0;

        public int SelectedMaxLikes { get; set; } = 300;

        public int SelectedLikePerHour { get; set; } = 15;
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

            var loginUserDetail = await Actions.Login(InstaInfo.Login, InstaInfo.Password);
            if (loginUserDetail != null)
            {
                LoggedInUser = loginUserDetail;
                //await GetSelfFeedAsync();
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

                await Task.Delay(1000);
            }

            MediaByTagCount = 0;
        }

        public async Task<bool> AutoModHelper(string type)
        {
            if (MediaByTagCount > 0)
            {
                try
                {
                    switch (type)
                    {
                        case "like":
                            await LikeTag();
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
                Log.WriteLog("The Selected tag is " + selectedTag + "....");

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
                    Log.WriteLog($"Success liking with url https://www.instagram.com/p/{tagToLike.code}");
                    NextIteration["LikeCounter"]++;
                    UserFeedData.Add(tagToLike);
                    MediaByTag.Remove(tagToLike);
                    MediaByTagCount--;
                    return true;
                }

                Log.WriteLog($"Failed to like with url https://www.instagram.com/p/{tagToLike.code}");
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
                    MediaByTag = feedData.items;
                    MediaByTagCount = feedData.items.Count;
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
