using InstaBot.Objects;
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
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using InstaBot.Callbacks;
using InstaBot.Helpers;
using InstaBot.Objects.InstagramData;

namespace InstaBot
{
    /// <summary>
    /// Interaction logic for InstaBot.xaml
    /// </summary>
    public partial class InstaBotWindow : Window
    {
        Bot InstaAutoBot = null;

        public InstaBotWindow()
        {
            CallBackLog.CallbackEventHandler = WriteLog;
            CallBackMedia.CallbackEventHandler = HandleMedia;

            InitializeComponent();

            InstaAutoBot = (Bot) instaGrid.DataContext;
            TagMapView.Map = new Map(Basemap.CreateLightGrayCanvas());
            TagMapView.GeoViewTapped += TagMapView_GeoViewTapped;

            //logTxtBox.Text =
            //    "Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon. Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.Short loin picanha boudin pork belly. Ground round porchetta biltong, cow t-bone tri-tip strip steak chuck filet mignon jowl turducken. Landjaeger strip steak pork chop, jowl sirloin pork capicola andouille. Kevin ribeye tongue, drumstick hamburger frankfurter t-bone corned beef beef biltong cow venison. Biltong picanha bresaola pork belly, filet mignon spare ribs doner pork chop kielbasa. Swine flank drumstick pork belly pancetta spare ribs rump filet mignon.";

        }

        private async void TagMapView_GeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            InstaAutoBot.SelectedGraphics.Clear();

            if (TagMapView.GraphicsOverlays.Count > 0)
            {
                var tolerance = 10d; // Use larger tolerance for touch
                var maximumResults = 3; // Only return one graphic  
                var onlyReturnPopups = false; // Return more than popups

                var graphicColl = new List<Graphic>();

                foreach (var go in TagMapView.GraphicsOverlays)
                {
                    // Use the following method to identify graphics in a specific graphics overlay
                    IdentifyGraphicsOverlayResult identifyResults = await TagMapView.IdentifyGraphicsOverlayAsync(
                        go,
                        e.Position,
                        tolerance,
                        onlyReturnPopups,
                        maximumResults);

                    if (identifyResults.Graphics.Count > 0)
                    {
                        IdentifyExpander.IsExpanded = true;
                        identifyResults.Graphics.ToList().ForEach(g=> InstaAutoBot.SelectedGraphics.Add(g));
                    }
                }

            }
        }

        private void HandleMedia(object media, string type)
        {
            switch (type)
            {
                case "profile":
                    var deatil = (UserDetail) media;
                    break;
                default:
                    break;
                    ;

            }
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
                hyperlink.RequestNavigate += (s, e) => { System.Diagnostics.Process.Start(e.Uri.ToString()); };

                logTxtBox.Inlines.Add(hyperlink);

            }
            logTxtBox.Inlines.Add(Environment.NewLine);
            logScroll.ScrollToBottom();
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
                    if (await InstaAutoBot.LoginAsync())
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
                    BotStopButton.Visibility = Visibility.Visible;
                    BotButton.Visibility = Visibility.Collapsed;
                    await InstaAutoBot.StartBot();
                    break;
                case "Stop Bot":
                    BotStopButton.Visibility = Visibility.Collapsed;
                    BotButton.Visibility = Visibility.Visible;
                    InstaAutoBot.IsBotActive = false;
                    break;
                default:
                    break;
            }
        }

        private void Bot_Click(object sender, RoutedEventArgs e)
        {
            HandleStopBot((sender as Button).Content.ToString());
        }

        private void OpenInstagramProfile(object sender, RoutedEventArgs e)
        {
            if (InstaAutoBot.LoggedInUser != null)
            {
                System.Diagnostics.Process.Start($"https://www.instagram.com/{InstaAutoBot.LoggedInUser.user.username}");
            }
        }

        private void OpenLink(object sender, MouseButtonEventArgs e)
        {
            var g = (sender as FrameworkElement)?.DataContext as Graphic;
            OpenUrl(g.Attributes["ig_link"].ToString());
        }

        private void OpenUrl(string url)
        {
            System.Diagnostics.Process.Start(url);

        }
    }
}
