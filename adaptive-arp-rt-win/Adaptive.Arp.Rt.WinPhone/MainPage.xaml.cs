using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web;

// The WebView Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Adaptive.Arp.Rt.WinPhone
{
    public sealed class MainStreamResolver : IUriToStreamResolver
    {

        public IAsyncOperation<Windows.Storage.Streams.IInputStream> UriToStreamAsync(Uri uri)
        {
            if (uri == null) throw new Exception("No stream, no party.");
            string path = uri.AbsolutePath;
            return GetContent(path).AsAsyncOperation();
        }

        private async Task<IInputStream> GetContent(string path)
        {
            try
            {
                if (path.IndexOf('?') > 0)
                {
                    path = path.Substring(0, path.IndexOf('?'));
                }
                else if (path.IndexOf('#') > 0)
                {
                    path = path.Substring(0, path.IndexOf('#'));
                }
                else if (path.EndsWith("/"))
                {
                    path += "index.html";
                }
                Uri localUri = new Uri("ms-appx:///Html/WebResources/www" + path);
                Debug.WriteLine("- Content {0}", localUri);
                StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(localUri);
                IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
                return stream;
            }
            catch (Exception)
            {
                throw new Exception("Invalid path.");
            }
        }
    }

    public sealed partial class MainPage : Page
    {
        // TODO: Replace with your URL here.
        private static readonly Uri HomeUri = new Uri("ms-appx-web:///Html/WebResources/www/index.html", UriKind.Absolute);

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //WebViewControl.Navigate(HomeUri);     
            HardwareButtons.BackPressed += this.MainPage_BackPressed;

            Uri uri = WebViewControl.BuildLocalStreamUri("MyApp", "/index.html");
            MainStreamResolver resolver = new MainStreamResolver();
            WebViewControl.NavigateToLocalStreamUri(uri, resolver);

        }

        /// <summary>
        /// Invoked when this page is being navigated away.
        /// </summary>
        /// <param name="e">Event data that describes how this page is navigating.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= this.MainPage_BackPressed;
        }

        /// <summary>
        /// Overrides the back button press to navigate in the WebView's back stack instead of the application's.
        /// </summary>
        private void MainPage_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (WebViewControl.CanGoBack)
            {
                WebViewControl.GoBack();
                e.Handled = true;
            }
        }

        private void Browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                Debug.WriteLine("Navigation to this page failed, check your internet connection.");
            }
        }

        /// <summary>
        /// Navigates forward in the WebView's history.
        /// </summary>
        private void ForwardAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
            }
        }

        /// <summary>
        /// Navigates to the initial home page.
        /// </summary>
        private void HomeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            WebViewControl.Navigate(HomeUri);
        }

        private void WebViewControl_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            Debug.WriteLine("Navigation started: {0}", args.Uri.AbsoluteUri);
        }
    }
}
