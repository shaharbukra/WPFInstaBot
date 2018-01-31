using InstaBot.Api;
using InstaBot.Objects.InstagramData;
using InstaBot.Objects.Models;
using System;
using System.Collections.Generic;
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
        #region Commands

        #endregion Commands

        #region Data members
        private UserDetail _loggedInUser;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }
        public UserDetail LoggedInUser
        {
            get { return _loggedInUser; }
            private set
            {
                _loggedInUser = value;
                RaisePropertyChanged();
                RaisePropertyChanged("LoggedInUserImage");
            }
        }
        public BitmapImage LoggedInUserImage
        {
            get {
                if(LoggedInUser!=null)
                    return new BitmapImage(new Uri(_loggedInUser.user.hd_profile_pic_url_info.url));
                else
                    return new BitmapImage(new Uri("https://media.wired.com/photos/59268651cefba457b079a47e/2:1/w_2500,c_limit/Instagram-v051916-f.jpg"));
            }
           
        }
        public string Username
        {
            get { return InstaInfo.Login; }
            set {
                InstaInfo.Login = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get { return InstaInfo.Password; }
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
             
            }
        }

        public Bot(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                IsBusy = true;
                InstaInfo.Login = userName;
                InstaInfo.Password = password;
                LoginAsync();
            }

            //LoggedInUser = null;

        }
        #endregion Ctor

  
        public async void LoginAsync()
        {
            var loginUserDetail = await Actions.Login(InstaInfo.Login, InstaInfo.Password);
            if (loginUserDetail != null)
            {
                LoggedInUser = loginUserDetail;
            }
            IsBusy = false;
        }

    }
}
