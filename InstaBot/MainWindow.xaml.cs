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

            AutoBot = (Bot)instaGrid.DataContext;
            //AutoBot.LoginAsync();

            //TestApiAsync();
        }

        private async void ShowInbox(object data, string type)
        {
          
        }

        private async Task ShowMessageAsync(string threadId)
        {
            var gotData = await Api.Inbox.GetInboxThread(threadId);
            if (gotData)
            {

            }
        }


        private void ShowTime(Dictionary<string, int> next)
        {
        }

        private void ShowBotMedia(object data, string type)
        {
           
        }

        private void WriteLog(string log)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            AutoBot.LoginAsync();
        }


        private void OpenInstagramProfile(object sender, RoutedEventArgs e)
        {
            if (AutoBot.LoggedInUser != null)
            {
                System.Diagnostics.Process.Start("https://www.instagram.com/" + AutoBot.LoggedInUser.user.username);



            }
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var code = ((FeedItem) (sender as Image)?.DataContext)?.code;
            System.Diagnostics.Process.Start($"https://www.instagram.com/p/{code}");

        }
    }
}
