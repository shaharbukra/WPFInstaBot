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
using InstaBot.Helpers;

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
            HandleLoginActionAsync("Login");
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

        private void WriteLog(string log, string url)
        {
            logTxtBox.Inlines.Add(new Run(log));

            if (url != null)
            {
                var urlRun = new Run(url);
                Hyperlink hyperlink = new Hyperlink(urlRun)
                {
                    NavigateUri = new Uri(url)
                };
                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;

                logTxtBox.Inlines.Add(hyperlink);

            }
            logTxtBox.Inlines.Add(Environment.NewLine);

            //logTxtBox.Text = log + Environment.NewLine + logTxtBox.Text;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());

        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await HandleLoginActionAsync(LoginBtn.Content.ToString());


        }

        private async Task HandleLoginActionAsync(string action)
        {
            switch (action)
            {
                case "Login":
                    if (await AutoBot.LoginAsync())
                    {
                        BotButton.IsEnabled = true;
                        LoginBtn.Content = "Logout";
                    }
                    break;
                case "Logout":
                    Log.WriteLog("Logout success!");
                    LoginBtn.Content = "Login";
                    BotButton.IsEnabled = false;
                    await HandleStopBot("Stop Bot");
                    break;
                default:
                    break;
            }
        }

        private async Task HandleStopBot(string action)
        {
            switch (action)
            {
                case "Start Bot":
                    action = "Stop Bot";
                    BotStopButton.Visibility = Visibility.Visible;
                    BotButton.Visibility = Visibility.Collapsed;
                    await AutoBot.StartBot();
                    break;
                case "Stop Bot":
                    action = "Start Bot";
                    BotStopButton.Visibility = Visibility.Collapsed;
                    BotButton.Visibility = Visibility.Visible;
                    AutoBot.IsBotActive = false;

                    break;
                default:
                    break;
            }
        }


        private void OpenInstagramProfile(object sender, RoutedEventArgs e)
        {
            if (AutoBot.LoggedInUser != null)
            {
                System.Diagnostics.Process.Start("https://www.instagram.com/" + AutoBot.LoggedInUser.user.username);
            }
        }

        private void OpenBrowser_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var code = ((FeedItem)(sender as Image)?.DataContext)?.code;
            System.Diagnostics.Process.Start($"https://www.instagram.com/p/{code}");

        }

        private async void Bot_Click(object sender, RoutedEventArgs e)
        {
            var action = (sender as Button).Content.ToString();
            HandleStopBot(action);
        }

        private void BotButton_Click(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
