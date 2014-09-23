using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Adaptive.Arp.Rt.WindowsPhoneSilverlight.Resources;
using Adaptive.Impl.WindowsPhoneSilverlight;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Adaptive.Arp.Rt.WindowsPhoneSilverlight
{

    public partial class App : Application
    {
        public static AppServerImpl server = null;


        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            //get all network adapters
            var adapters = Windows.Networking.Connectivity.NetworkInformation.GetHostNames();
            bool found = false;
            string ip = "127.0.0.1";
            if (adapters != null && adapters.Count > 0)
            {

                foreach (var adapter in adapters)
                {

                    if (adapter.IPInformation != null)
                    {
                        //find the Wifi adapter (interface type == 71)
                        if (adapter.IPInformation.NetworkAdapter.IanaInterfaceType == 71 && (adapter.Type == Windows.Networking.HostNameType.Ipv4 || adapter.Type == Windows.Networking.HostNameType.Ipv6))
                        {
                            //if found assign it's ip to a variable
                            found = true;
                            ip = adapter.RawName;
                            break;
                        }
                    }
                }
            }

            server = new AppServerImpl(null, ip, "8080");

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            Regex rgx = new Regex("^/*");
            server.addRule(rgx, homePage);

        }

        // Code to execute when a contract activation such as a file open or save picker returns 
        // with the picked file or other return values
        public void Application_ContractActivated(object sender, Windows.ApplicationModel.Activation.IActivatedEventArgs e)
        {
            Debug.WriteLine("ContractActivated");
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            server.StartServer();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            Debug.WriteLine("Activated");
            server.ResumeServer();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            Debug.WriteLine("Deactivated");
            server.PauseServer();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            Debug.WriteLine("Closing");
            server.StopServer();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        private AppServerRequestResponse homePage(AppServerRequestResponse request)
        {
            //prepare the response object
            AppServerRequestResponse newResponse = new AppServerRequestResponse();

            //create a new dictionary for headers - this could be done using a more advanced class for webResponse object - i just used a simple struct
            newResponse.httpHeaders = new Dictionary<string, string>();

            //add httpContent type httpHeaders
            newResponse.httpHeaders.Add("Content-Type", "text/html");

            Stream resposneText = new MemoryStream();
            StreamWriter contentWriter = new StreamWriter(resposneText);
            contentWriter.AutoFlush = false;

            if (request.httpUri.Equals("/index.html"))
            {
                contentWriter.WriteLine("<html>");
                contentWriter.WriteLine("   <head>");
                contentWriter.WriteLine("       <title>Dummy Page</title>");
                contentWriter.WriteLine("   </head>");
                contentWriter.WriteLine("   <body>");
                contentWriter.WriteLine("   <h1>Adaptive RT</h1>");
                contentWriter.WriteLine("   <div><iframe id src=\"/iframe1.html\" width=\"100%\" height=\"15%\"></iframe></div>");
                contentWriter.WriteLine("   <div><iframe id src=\"/iframe1.html\" width=\"100%\" height=\"15%\"></iframe></div>");
                contentWriter.WriteLine("   <div><iframe id src=\"/iframe1.html\" width=\"100%\" height=\"15%\"></iframe></div>");
                contentWriter.WriteLine("   <div><iframe id src=\"/iframe1.html\" width=\"100%\" height=\"15%\"></iframe></div>");
                contentWriter.WriteLine("   <a href=\"/index.html\"><h2><b>Refresh!</b></h2></a>");
                contentWriter.WriteLine("   </body>");
                contentWriter.WriteLine("</html>");

            }
            else
            {
                contentWriter.WriteLine("<html>");
                contentWriter.WriteLine("   <head>");
                contentWriter.WriteLine("       <title>Dummy Page</title>");
                contentWriter.WriteLine("       <meta http-equiv=\"refresh\" content=\"0\">");
                contentWriter.WriteLine("   </head>");
                contentWriter.WriteLine("   <body>");
                contentWriter.WriteLine("   <h1>Adaptive RT</h1>");
                contentWriter.WriteLine("   <p><b>Platform: </b>" + Environment.OSVersion.Platform + "</p>");
                contentWriter.WriteLine("   <p><b>Device Name: </b>" + Microsoft.Phone.Info.DeviceStatus.DeviceName + "</p>");
                contentWriter.WriteLine("   <p><b>Device Name: </b>" + DateTime.Now.ToString("HH:mm:ss.ff dd-MM-yyyy") + "</p>");
                contentWriter.WriteLine("   <p><b>Request: </b>" + request.httpUri + "</p>");
                contentWriter.WriteLine("   <p><b>Concurrent: </b>" + server.getConcurrentCount() + "</p>");
                contentWriter.WriteLine("   <a href=\"/\"><h2><b>Refresh!</b></h2></a>");
                contentWriter.WriteLine("   </body>");
                contentWriter.WriteLine("</html>");
            }
            contentWriter.Flush();
            //assign the response
            newResponse.httpContent = resposneText;

            //return the response
            return newResponse;
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Handle contract activation such as a file open or save picker
            PhoneApplicationService.Current.ContractActivated += Application_ContractActivated;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}