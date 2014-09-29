using Adaptive.Arp.Api;
using Adaptive.Arp.Impl.WinPhone;
using Adaptive.Arp.Impl.WinPhone.Internals;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The WebView Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Adaptive.Arp.Rt.WinPhone
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application, IAppServerListener
    {
        private TransitionCollection transitions;
        private IAppServer server = null;
        private IAppServerManager manager = null;
        

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.Resuming += this.OnResuming;
            this.UnhandledException += App_UnhandledException;
            manager = AppServerManagerImpl.Instance;
            manager.AddServerListener(this);
            manager.StartServer();
        }

        void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Error(e.Exception);
        }

        void Current_VisibilityChanged(object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
        {
            (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).IsVisible = e.Visible;
            if (e.Visible)
            {
                (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Resuming();
                (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Running();
            }
            else
            {
                (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Paused();
            }
            
        }

        void OnResuming(object sender, object e)
        {
            (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Resuming();
            this.manager.ResumeServer(this.server);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Starting();
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    Debug.WriteLine("Application - launching - recover state.");
                }
                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                Window.Current.VisibilityChanged += Current_VisibilityChanged;
                (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Started();
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            
            Window.Current.Activate();
            (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Running();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            (AppRegistryImpl.Instance.GetApplicationLifecycle() as LifecycleImpl).Stopping();
            var deferral = e.SuspendingOperation.GetDeferral();
            this.manager.PauseServer(this.server);
            deferral.Complete();
        }

        public void OnPaused(IAppServer server)
        {
            Debug.WriteLine("SERVER paused. {0}", server.GetBaseURI());
        }

        public void OnPausing(IAppServer server)
        {
            Debug.WriteLine("SERVER pausing. {0}", server.GetBaseURI());
        }

        public void OnResumed(IAppServer server)
        {
            Debug.WriteLine("SERVER resumed. {0}", server.GetBaseURI());
        }

        public void OnResuming(IAppServer server)
        {
            Debug.WriteLine("SERVER resuming. {0}", server.GetBaseURI());
        }

        public void OnStart(IAppServer server)
        {
            this.server = server;
            Debug.WriteLine("SERVER started. {0}", server.GetBaseURI());
        }

        public void OnStopped(IAppServer server)
        {
            Debug.WriteLine("SERVER stopped. {0}", server.GetBaseURI());
        }

        public void OnStopping(IAppServer server)
        {
            Debug.WriteLine("SERVER stopping. {0}", server.GetBaseURI());
        }
    }
}
