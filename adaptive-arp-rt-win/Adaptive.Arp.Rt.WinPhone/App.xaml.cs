using Adaptive.Arp.Impl.Util;
using Adaptive.Arp.Impl.WinPhone.Internals;
using Adaptive.Impl.WindowsPhone;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The WebView Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Adaptive.Arp.Rt.WinPhone
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;
        private AppServerImpl server = null;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.InitializeServer();
            this.Suspending += this.OnSuspending;
        }

        private void InitializeServer()
        {
            if (Debugger.IsAttached)
            {
                server = new AppServerImpl("8080");
            }
            else
            {
                server = new AppServerImpl("127.0.0.1", "8080");
            }
            //Regex rgx = new Regex("^/*");
            //server.addRule(rgx, HandleHttpMessage);
            //server.addRule(rgx, HandleHttpMessage);
            Regex appverseCompatibility = new Regex("^/service/*");
            server.addRule(appverseCompatibility, HandleAppverse);
            server.StartServer();
        }

        private AppServerRequestResponse HandleAppverse(AppServerRequestResponse request)
        {
            AppServerRequestResponse newResponse = new AppServerRequestResponse();
            Stream responseStream = new MemoryStream();

            Task task = new Task(async () => {
                
                newResponse.httpHeaders = new Dictionary<string, string>();            
                StreamWriter contentWriter = new StreamWriter(responseStream);
                DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
                contentWriter.AutoFlush = false;

                Debug.WriteLine("API Request: {0} {1}", request.httpMethod, request.httpUri);
                if (request.httpMethod == "OPTIONS")
                {
                    foreach (string key in request.httpHeaders.Keys)
                    {
                        Debug.WriteLine("Key: {0}  Value: {1}", key, request.httpHeaders[key]);
                    }

                    newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                    newResponse.httpHeaders.Add("Access-Control-Allow-Methods", request.httpHeaders["Access-Control-Request-Method"]);
                    newResponse.httpHeaders.Add("Access-Control-Allow-Headers", request.httpHeaders["Access-Control-Request-Headers"]);
                    newResponse.httpHeaders.Add("Connection", "close");
                    newResponse.httpHeaders.Add("Pragma", "no-cache");
                    await dataWriter.FlushAsync();
                    await dataWriter.StoreAsync();
                }
                else if (request.httpUri == "/service/system/GetUnityContext")
                {
                        foreach (string key in request.httpHeaders.Keys)
                        {
                            Debug.WriteLine("Key: {0}  Value: {1}", key, request.httpHeaders[key]);
                        }
                        newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                        newResponse.httpHeaders.Add("Content-Type", "application/json");
                        newResponse.httpHeaders.Add("Connection", "close");
                        newResponse.httpHeaders.Add("Pragma", "no-cache");
                        dataWriter.WriteString("{\"Emulator\":true,\"EmulatorOrientation\":0,\"EmulatorScreen\":\"{height:640,widht:320}\",\"Windows\":false,\"iPod\":false,\"iPad\":false,\"iPhone\":false,\"Android\":false,\"Blackberry\":false,\"TabletDevice\":false,\"Tablet\":false,\"Phone\":true,\"iOS\":false}\r\n");
                        await dataWriter.FlushAsync();
                        await dataWriter.StoreAsync();
                }
                else if (request.httpUri == "/service/system/DismissSplashScreen")
                {
                    newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                    newResponse.httpHeaders.Add("Content-Type", "application/json");
                    newResponse.httpHeaders.Add("Connection", "close");
                    newResponse.httpHeaders.Add("Pragma", "no-cache");
                    await dataWriter.FlushAsync();
                    await dataWriter.StoreAsync();
                }
                else
                {
                    dataWriter.WriteString("HTTP/1.1 404 Not Found\r\n");
                    dataWriter.WriteString("Content-Type: text/html\r\n");
                    dataWriter.WriteString("Content-Length: 9\r\n");
                    dataWriter.WriteString("Pragma: no-cache\r\n");
                    dataWriter.WriteString("Connection: close\r\n");
                    dataWriter.WriteString("\r\n");
                    dataWriter.WriteString("Not found");
                    await dataWriter.FlushAsync();
                    await dataWriter.StoreAsync();
                }
            });
            task.Start();
            task.Wait();
            newResponse.httpContent = responseStream;
            return newResponse;
        }

        private  AppServerRequestResponse HandleHttpMessage(AppServerRequestResponse request)
        {
            AppServerRequestResponse newResponse = new AppServerRequestResponse();

            //create a new dictionary for headers - this could be done using a more advanced class for webResponse object - i just used a simple struct
            newResponse.httpHeaders = new Dictionary<string, string>();
            Stream responseStream = new MemoryStream();
            StreamWriter contentWriter = new StreamWriter(responseStream);
            contentWriter.AutoFlush = false;

            Task task = new Task(async () =>
            {
                Debug.WriteLine("");
                if (request.httpUri == "/" || request.httpUri.EndsWith("/"))
                {
                    if (await existsLocalResource(request.httpUri + "index.html"))
                    {
                        request.httpUri = request.httpUri + "index.html";
                    }
                    else if (await existsLocalResource(request.httpUri + "index.htm"))
                    {
                        request.httpUri = request.httpUri + "index.htm";
                    }
                }
                //add httpContent type httpHeaders

                if (await existsLocalResource(request.httpUri))
                {
                    StreamResourceInfo resource = await loadLocalResource(request.httpUri);
                    newResponse.httpHeaders.Add("Content-Type", resource.ContentType);

                    bool readFinished = false;
                    uint readMaxBufferSize = 4096 * 4;

                    DataReader reader = new DataReader(resource.Stream.AsInputStream());
                    while (!readFinished)
                    {
                        // await a full buffer or eof
                        await reader.LoadAsync(readMaxBufferSize);

                        if (reader.UnconsumedBufferLength > 0)
                        {
                            // Read buffer
                            uint readLength = reader.UnconsumedBufferLength;
                            byte[] readBuffer = new byte[reader.UnconsumedBufferLength];
                            reader.ReadBytes(readBuffer);
                            // Write buffer
                            responseStream.Write(readBuffer, 0, readBuffer.Length);

                            // Not full buffer, reached eof
                            if (readLength < readMaxBufferSize) readFinished = true;
                        }
                        else
                        {
                            // Reached eof 
                            readFinished = true;
                            await responseStream.FlushAsync();
                            newResponse.httpContent = responseStream;
                        }
                    }

                }
                else
                {
                    if (request.httpUri == "/service/system/GetUnityContext")
                    {
                        //if (request.httpMethod == "OPTIONS")
                        //{
                        //    newResponse.httpHeaders.Add("Allow", "POST");
                        //    contentWriter.Flush();
                        //}
                        //else if (request.httpMethod == "POST")
                        //{
                            newResponse.httpHeaders.Add("Content-Type","application/json");
                            contentWriter.WriteLine("{\"Emulator\":true,\"EmulatorOrientation\":0,\"EmulatorScreen\":\"{height:1232,widht:720}\",\"Windows\":false,\"iPod\":false,\"iPad\":false,\"iPhone\":false,\"Android\":true,\"Blackberry\":false,\"TabletDevice\":false,\"Tablet\":false,\"Phone\":true,\"iOS\":false}");
                            await contentWriter.FlushAsync();
                            responseStream.Flush();
                            Debug.WriteLine("Stream size1: {0}", responseStream.Length);
                            newResponse.httpContent = responseStream;
                        //}
                    }
                    else
                    {
                        Debug.WriteLine("---- MISSING {0}", request.httpUri);
                    }
                    
                }

            });
            Debug.WriteLine("Starting task...");
            task.Start();
            Debug.WriteLine("Waiting task...");
            task.Wait();
            newResponse.httpContent = responseStream;
            Debug.WriteLine("Stream size2: {0}", newResponse.httpContent.Length);

            //return the response
            return newResponse;
        }

        private async Task<bool> existsLocalResource(string relativePath)
        {
            if (relativePath.IndexOf('?') > 0)
            {
                relativePath = relativePath.Substring(0, relativePath.IndexOf('?'));
            }
            Uri uri = new Uri("ms-appx:///Html/WebResources/www_" + relativePath);

            //return Application.GetResourceStream(new Uri(@"/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Web" + relativePath, UriKind.Relative)) != null;
            try
            {
                StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(uri);
                Debug.WriteLine("File {0}", f.Path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private async Task<StreamResourceInfo> loadLocalResource(string relativePath)
        {
            Debug.WriteLine("loadLocalResource");
            if (relativePath.IndexOf('?') > 0)
            {
                relativePath = relativePath.Substring(0, relativePath.IndexOf('?'));
            }
            Uri uri = new Uri("ms-appx:///Html/WebResources/www_" + relativePath);
            //return Application.GetResourceStream(new Uri(@"/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Web" + relativePath, UriKind.Relative));
            //return Application.GetResourceStream(new Uri("ms-appx:///Web" + relativePath, UriKind.Relative));
            try
            {
                StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(uri);
                IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);

                bool readFinished = false;
                uint readMaxBufferSize = 4096 * 4;
                MemoryStream responseStream = new MemoryStream();
                DataReader reader = new DataReader(stream);
                while (!readFinished)
                {
                    // await a full buffer or eof
                    await reader.LoadAsync(readMaxBufferSize);

                    if (reader.UnconsumedBufferLength > 0)
                    {
                        // Read buffer
                        uint readLength = reader.UnconsumedBufferLength;
                        byte[] readBuffer = new byte[reader.UnconsumedBufferLength];
                        reader.ReadBytes(readBuffer);
                        // Write buffer
                        responseStream.Write(readBuffer, 0, readBuffer.Length);

                        // Not full buffer, reached eof
                        if (readLength < readMaxBufferSize) readFinished = true;
                    }
                    else
                    {
                        // Reached eof 
                        readFinished = true;
                        responseStream.Flush();
                        responseStream.Position = 0;
                    }
                }
                StreamResourceInfo sri = new StreamResourceInfo(responseStream, mimeLocalResource(relativePath));
                return sri;
            }
            catch (Exception)
            {
                return null;
            }
        }



        private string mimeLocalResource(string relativePath)
        {
            if (relativePath.IndexOf('?') > 0)
            {
                relativePath = relativePath.Substring(0, relativePath.IndexOf('?'));
            }
            return MimetypeResolver.GetMimeType(relativePath.Substring(relativePath.LastIndexOf('.')));

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
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
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
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
