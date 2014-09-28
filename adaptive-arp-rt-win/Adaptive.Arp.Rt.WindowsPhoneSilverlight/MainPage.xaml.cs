using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Adaptive.Arp.Rt.WindowsPhoneSilverlight.Resources;
using Adaptive.Impl.WindowsPhoneSilverlight;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Windows.Resources;


namespace Adaptive.Arp.Rt.WindowsPhoneSilverlight
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //SupportedPageOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
            //Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();            
            

            Debug.WriteLine("Server started at base URI {0}.", App.server.GetBaseURI());
            webView.IsGeolocationEnabled = true;
            webView.IsScriptEnabled = true;
            webView.Navigating += webView_Navigating;
            //UriParser.Register(new AppServerInterceptor(), "flowers", 8080);

            //string MainUri = "/Web/examples/kitchensink/index.html";
            //webView.Navigate(new Uri(MainUri, UriKind.Relative));

            //webView.Navigate(new Uri("http://docs.sencha.com/touch/2.4.0/touch-build/examples/"));
            //webView.Navigate(new Uri(App.server.GetBaseURI() + "index.html"));

        }

        void webView_Navigating(object sender, NavigatingEventArgs e)
        {
            Debug.WriteLine("Nav: {0}", e.Uri);
        }

        private bool existsLocalResource(string relativePath)
        {
            return Application.GetResourceStream(new Uri(@"/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Web/" + relativePath, UriKind.Relative)) != null;
        }

        private StreamResourceInfo loadLocalResource(string relativePath)
        {
            return Application.GetResourceStream(new Uri(@"/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Web/" + relativePath, UriKind.Relative));
        }

        private AppServerRequestResponse homePage(AppServerRequestResponse response)
        {
            //prepare the response object
            AppServerRequestResponse newResponse = new AppServerRequestResponse();

            //create a new dictionary for headers - this could be done using a more advanced class for webResponse object - i just used a simple struct
            newResponse.httpHeaders = new Dictionary<string, string>();

            //add httpContent type httpHeaders
            newResponse.httpHeaders.Add("Content-Type", "text/html");

            Stream resposneText = new MemoryStream();
            StreamWriter contentWriter = new StreamWriter(resposneText);
            contentWriter.WriteLine("<html>");
            contentWriter.WriteLine("   <head>");
            contentWriter.WriteLine("       <title>Dummy Page</title>");
            contentWriter.WriteLine("   </head>");
            contentWriter.WriteLine("   <body>");
            contentWriter.WriteLine("   <h1>Adaptive RT</h1>");
            contentWriter.WriteLine("   <p><b>Platform: </b>" + Environment.OSVersion.Platform + "</p>");
            contentWriter.WriteLine("   <p><b>Device Name: </b>" + Microsoft.Phone.Info.DeviceStatus.DeviceName + "</p>");
            contentWriter.WriteLine("   <p><b>Device Name: </b>" + DateTime.Now.ToString("HH:mm:ss.ff dd-MM-yyyy") + "</p>");
            contentWriter.WriteLine("   </body>");
            contentWriter.WriteLine("</html>");
            contentWriter.Flush();
            //assign the response
            newResponse.httpContent = resposneText;

            //return the response
            return newResponse;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}