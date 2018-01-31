using InstaBot.Callbacks;
using InstaBot.Objects;
using InstaBot.Objects.InstagramData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstaBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bot AutoBot = null;

        public MainWindow()
        {
            CallBackLog.CallbackEventHandler = WriteLog;
            CallBackMedia.CallbackEventHandler = ShowBotMedia;
            CallBackTime.callbackEventHandler = ShowTime;
            CallBackInbox.CallbackEventHandler = ShowInbox;

            InitializeComponent();

            TestApiAsync();
        }

        private async void ShowInbox(object data, string type)
        {
            //switch (type)
            //{
            //    case "inbox":
            //        var d = data as InboxData;
            //        listBox.ItemsSource = d.inbox.threads;
                   
            //        //ShowMessageAsync(d.inbox.threads[1].thread_id)
            //        break;
            //    case "inbox2":
            //        var d2 = data as InboxData;
            //        var appendItems = listBox.ItemsSource as List<Thread>;
            //        appendItems.AddRange(d2.inbox.threads);
            //        //listBox.ItemsSource = appendItems;
            //        break;
            //    case "thread":
            //        var t = data as ThreadData;
            //        listBox1.ItemsSource = t.thread.items;
            //        break;
            //}
        }

        private async Task ShowMessageAsync(string threadId)
        {
            var gotData = await Api.Inbox.GetInboxThread(threadId);
            if (gotData)
            {

            }
        }

        private async Task TestApiAsync()
        {
            //var IsConnected = await Api.Actions.Login("photography_in_israel", "p226prs6");

            //if (IsConnected != null)
            //{
            //    var a = await Api.Inbox.GetInbox();
            //    textbox.Text = "Login success";
            //}
            //else
            //{
            //    textbox.Text = "Bot can't login!  Error Detected while login";
            //}
        }

        private void ShowTime(Dictionary<string, int> next)
        {
        }

        private void ShowBotMedia(object data, string type)
        {
            
            //if (type == "profile")
            //{
            //    UserDetail userData = (UserDetail)data;

            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.UriSource = new Uri(userData.user.hd_profile_pic_url_info.url, UriKind.Absolute);
            //    bitmap.EndInit();

            //    profilePicture.Source = bitmap;
            //    //lblFollower.Content = media.user.follower_count.Value;
            //    //lblFollowing.Content = media.user.following_count.Value;
            //    //lblMediaCount.Content = media.user.media_count.Value;
            //}
        }

        private void WriteLog(string log)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AutoBot = new Bot("shaharbukra", "p226prs6i");

            //testLoginAsync();
        }

        private async Task testLoginAsync()
        {
        //    var IsConnected = await Api.Actions.Login("shaharbukra", "p226prs6i");

        //    if (IsConnected != null)
        //    {
        //        var a = await Api.Inbox.GetInbox();

        //        textbox.Text = "Login success";
        //    }
        //    else
        //    {
        //        textbox.Text = "Bot can't login!  Error Detected while login";
        //    }
        //}

        //private async void ListBox_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    var t_id = ((sender as ListBox).SelectedItem as Thread).thread_id;
        //    await Api.Inbox.GetInboxThread(t_id);
        //    //var gotData = await Api.Inbox.GetInboxThread(threadId);
        }

        private void Button_Click_1()
        {

        }
    }
}
