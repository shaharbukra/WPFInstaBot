using InstaBot.Api;
using InstaBot.Objects.InstagramData;
using InstaBot.Objects.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InstaBot.Objects
{
    public class Bot : ModelBase
    {
      
        #region Data members
        private UserDetail _loggedInUser;
        private ObservableCollection<FeedItem> _userFeedData = new ObservableCollection<FeedItem>();
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }
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

        public List<int> LongListTill1000 => Enumerable.Range(0, 1000).ToList();
        public List<int> LongListTill200 => Enumerable.Range(0, 200).ToList();

        public int SelectedMinLikes { get; set; } = 0;

        public int SelectedMaxLikes { get; set; } = 300;

        public int SelectedLikePerHour { get; set; } = 100;

        public int SelectedFollowPerHour { get; set; } = 100;

        public int SelectedUnfollowPerHour { get; set; } = 100;

        public int SelectedCommentPerHour { get; set; } = 100;

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
                InstaInfo.Login = "username";
                InstaInfo.Password = "password";
            }
        }

        public Bot(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                
                InstaInfo.Login = userName;
                InstaInfo.Password = password;
                LoginAsync();
            }

            //LoggedInUser = null;

        }
        #endregion Ctor

        public async void LoginAsync()
        {
            IsBusy = true;
            var loginUserDetail = await Actions.Login(InstaInfo.Login, InstaInfo.Password);
            if (loginUserDetail != null)
            {
                LoggedInUser = loginUserDetail;

                await GetSelfFeedAsync();
            }
            IsBusy = false;
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
