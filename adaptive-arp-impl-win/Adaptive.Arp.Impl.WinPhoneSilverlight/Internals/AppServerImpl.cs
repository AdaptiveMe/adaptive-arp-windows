using Adaptive.Arp.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace Adaptive.Impl.WindowsPhoneSilverlight
{
    // Delegate method to invoke on rule match.
    public delegate AppServerRequestResponse RuleDeletage(AppServerRequestResponse response);

    // Delegate method to invoke on error.
    public delegate void ErrorOccured(int code, string message);

    public struct AppServerRequestResponse
    {
        public Dictionary<string, string> httpHeaders;
        public string httpMethod;
        public string httpUri;
        public Stream httpContent;
    }

    public class AppServerImpl : AbstractAppServerImpl
    {
        private static List<StreamSocket> socketList = new List<StreamSocket>();

        // Used only for when handling requests with file handler.
        #region File based support
        /*
        IsolatedStorageFile fileIsolatedStorage = null;
        protected Random filenameRandomizer = null;
        */
        #endregion

        // Internal Events
        public event ErrorOccured errorOccured;

        // Server Stuff
        private StreamSocketListener serverListener = null;
        private bool serverIsListening = false;
        private Dictionary<Regex, RuleDeletage> serverRules = null;
        private string serverIp = "127.0.0.1";
        private string serverPort = "-1";
        private bool serverIpAll = false;
        private static int concurrentCount = 0;


        public AppServerImpl(string ip, string port)
        {
            this.serverIp = ip;
            this.serverPort = port;
            this.serverIsListening = false;
        }

        public int getConcurrentCount()
        {
            return AppServerImpl.concurrentCount;
        }

        public bool addRule(Regex regEx, RuleDeletage rule)
        {
            bool added = false;

            if (serverRules == null)
            {
                serverRules = new Dictionary<Regex, RuleDeletage>();
            }

            if (!serverRules.ContainsKey(regEx))
            {
                serverRules.Add(regEx, rule);
                added = true;
            }
            return added;
        }

        public bool removeRule(Regex regEx)
        {
            if (serverRules != null)
            {
                return serverRules.Remove(regEx);
            }
            else
            {
                return false;
            }
        }

        public AppServerImpl(string port) : this("127.0.0.1", port)
        {
            this.serverIpAll = true;
        }


        #region Support for store and process.
        /*
        void listener_ConnectionReceived_HandleAsFile(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            if (fileIsolatedStorage == null)
            {
                fileIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                filenameRandomizer = new Random();
            }

            if (serverIsListening == false)
            {
                // If local server is not listening, ignore call.
                return;
            }

            // Temporary file to handle large post requests
            string filenameTemp = "/temp_" + filenameRandomizer.Next(100, 999).ToString() + filenameRandomizer.Next(1000, 9999).ToString();
            IsolatedStorageFileStream fileIsolated = fileIsolatedStorage.CreateFile(filenameTemp);

            // Socket reader
            StreamSocket socket = args.Socket;
            DataReader dataReader = new DataReader(socket.InputStream);
            dataReader.InputStreamOptions = InputStreamOptions.Partial;

            // Data dataReader flags and options
            bool readFinished = false;
            uint readMaxBufferSize = 4096;


            Task.Run(async () =>
            {
                while (!readFinished)
                {
                    // await a full buffer or eof
                    await dataReader.LoadAsync(readMaxBufferSize);

                    if (dataReader.UnconsumedBufferLength > 0)
                    {
                        // Read buffer
                        uint readLength = dataReader.UnconsumedBufferLength;
                        byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                        dataReader.ReadBytes(readBuffer);
                        // Write buffer
                        fileIsolated.Write(readBuffer, 0, readBuffer.Length);

                        // Not full buffer, reached eof
                        if (readLength < readMaxBufferSize) readFinished = true;
                    }
                    else
                    {
                        // Reached eof 
                        readFinished = true;
                    }
                }

                if (readFinished == true)
                {
                    fileIsolated.Close();
                    parseRequest(filenameTemp, socket);
                }
            });
        }
         */
        #endregion

        void listener_ConnectionReceived_HandleInMemory(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            if (serverIsListening == false)
            {
                // If local server is not listening, ignore call.
                return;
            }

            // Increment current count of processes being served.
            lock (socketList)
            {
                concurrentCount++;
                socketList.Add(args.Socket);
            }


            Task.Run(async () =>
            {
                // Socket reader
                //StreamSocket socket = args.Socket;
                DataReader dataReader = new DataReader(args.Socket.InputStream);
                dataReader.InputStreamOptions = InputStreamOptions.Partial;

                // Temporary stream
                MemoryStream memoryStream = new MemoryStream();

                // Data dataReader flags and options
                bool readFinished = false;
                uint readMaxBufferSize = 4096;

                while (!readFinished)
                {
                    // await a full buffer or eof
                    await dataReader.LoadAsync(readMaxBufferSize);

                    if (dataReader.UnconsumedBufferLength > 0)
                    {
                        // Read buffer
                        uint readLength = dataReader.UnconsumedBufferLength;
                        byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                        dataReader.ReadBytes(readBuffer);
                        // Write buffer
                        memoryStream.Write(readBuffer, 0, readBuffer.Length);

                        // Not full buffer, reached eof
                        if (readLength < readMaxBufferSize) readFinished = true;
                    }
                    else
                    {
                        // Reached eof 
                        readFinished = true;
                    }
                }

                if (readFinished == true)
                {
                    // Flush stream and reset position to start.
                    memoryStream.Flush();
                    memoryStream.Position = 0;
                    parseRequest(memoryStream, args.Socket);
                }
            });
        }

        public void parseRequest(MemoryStream inStream, StreamSocket socket)
        {
            try
            {
                StreamReader streamReader = new StreamReader(inStream);
                AppServerRequestResponse request = new AppServerRequestResponse();

                string readLine = streamReader.ReadLine();
                if (readLine != null && readLine.Length > 0)
                {
                    // Process request type.
                    if (readLine.Substring(0, 3) == "GET")
                    {
                        request.httpMethod = "GET";
                        request.httpUri = readLine.Substring(4);
                        request.httpUri = Regex.Replace(request.httpUri, " HTTP.*$", "");

                    }
                    else if (readLine.Substring(0, 4) == "POST")
                    {
                        request.httpMethod = "POST";
                        request.httpUri = readLine.Substring(5);
                        request.httpUri = Regex.Replace(request.httpUri, " HTTP.*$", "");
                    }

                    // Process request headers.
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    string readLineHeader = "";
                    do
                    {
                        readLineHeader = streamReader.ReadLine();
                        string[] headerSeparator = new string[] { ":" };
                        string[] headerElements = readLineHeader.Split(headerSeparator, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (headerElements.Length > 0)
                        {
                            headers.Add(headerElements[0], headerElements[1]);
                        }
                    } while (readLineHeader.Length > 0);
                    request.httpHeaders = headers;

                    // Assign content body (remaining stream).
                    request.httpContent = streamReader.BaseStream;

                    // Response writer.
                    DataWriter dataWriter = new DataWriter(socket.OutputStream);

                    // Process rules.
                    bool ruleFound = false;
                    if (serverRules != null)
                    {
                        // For each rule
                        foreach (Regex rule_part in serverRules.Keys)
                        {
                            //if it matches the URL
                            if (rule_part.IsMatch(request.httpUri))
                            {
                                try
                                {
                                    // Call delegate.
                                    AppServerRequestResponse toSend = serverRules[rule_part](request);
                                    ruleFound = true;

                                    // Process response headers.
                                    if (toSend.httpHeaders.ContainsKey("Location"))
                                        // Location change.
                                        dataWriter.WriteString("HTTP/1.1 302\r\n");
                                    else
                                        // OK.
                                        dataWriter.WriteString("HTTP/1.1 200 OK\r\n");

                                    dataWriter.WriteString("Content-Length: " + toSend.httpContent.Length + "\r\n");
                                    foreach (string key in toSend.httpHeaders.Keys)
                                    {
                                        dataWriter.WriteString(key + ": " + toSend.httpHeaders[key] + "\r\n");
                                    }
                                    dataWriter.WriteString("Connection: close\r\n");

                                    String osPlatform = Environment.OSVersion.Platform.ToString();
#if WINDOWS_APP || WINDOWS 
                            osPlatform = "Windows";
#endif
#if WINDOWS_PHONE_APP || WINDOWS_PHONE
                                    osPlatform = "Windows Phone";
#endif
#if WINDOWS_PHONE_APP || WINDOWS_PHONE && SILVERLIGHT
                                    osPlatform = "Windows Phone Silverlight";
#endif
                                    // Internal headers.
                                    dataWriter.WriteString("X-Server: Adaptive 1.0 (" + osPlatform + " " + Environment.OSVersion.Version + "/" + Environment.OSVersion.Platform + "/" + Environment.ProcessorCount + " Cores)\r\n");
                                    dataWriter.WriteString("X-ServerBind: http://" + this.serverIp + ":" + this.serverPort + "/\r\n");

                                    // Process body.
                                    dataWriter.WriteString("\r\n");
                                    //lock (this)
                                    //{
                                    // Reset stream to beginning.
                                    toSend.httpContent.Seek(0, SeekOrigin.Begin);
                                    Task.Run(async () =>
                                    {
                                        try
                                        {
                                            // Commit Headers
                                            await dataWriter.FlushAsync(); // flush output
                                            await dataWriter.StoreAsync(); // store output
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine("HeaderBlock: " + ex.StackTrace);
                                        }
                                        try
                                        {

                                            // write data to output using 1024 buffer
                                            while (toSend.httpContent.Position < toSend.httpContent.Length)
                                            {
                                                byte[] buffer;
                                                if (toSend.httpContent.Length - toSend.httpContent.Position < 1024)
                                                {
                                                    buffer = new byte[toSend.httpContent.Length - toSend.httpContent.Position];
                                                }
                                                else
                                                {
                                                    buffer = new byte[1024];
                                                }
                                                toSend.httpContent.Read(buffer, 0, buffer.Length);
                                                dataWriter.WriteBytes(buffer);


                                            }

                                            // Commit Body
                                            await dataWriter.FlushAsync(); // flush output
                                            await dataWriter.StoreAsync(); // store output
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine("BodyBlock: " + ex.StackTrace);
                                        }
                                        try
                                        {
                                            toSend.httpContent.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine("CloseBlock: " + ex.StackTrace);
                                        }
                                    });
                                    //}
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.StackTrace);
                                }
                            }
                        }
                    }

                    if (ruleFound == false)
                    {
                        dataWriter.WriteString("HTTP/1.1 404 Not Found\r\n");
                        dataWriter.WriteString("Content-Type: text/html\r\n");
                        dataWriter.WriteString("Content-Length: 9\r\n");
                        dataWriter.WriteString("Pragma: no-cache\r\n");
                        dataWriter.WriteString("Connection: close\r\n");
                        dataWriter.WriteString("\r\n");
                        dataWriter.WriteString("Not found");
                        Task.Run(async () =>
                        {
                            await dataWriter.FlushAsync();
                            await dataWriter.StoreAsync();
                        });
                    }

                }
                // Decrement current requests being processes.
                lock (socketList)
                {
                    concurrentCount--;
                    /**
                     * This list keeps StreamSocket alive to avoid premature disposal of the object (ObjectDisposedException of StreamSocket underlying DataWriter). 
                     * One would expect the runtime to keep reference counts correctly BUT with async calls, it clearly falls over when under stress. So this list
                     * is crucial and only does something useful when the local server is processing > 8 calls concurrently. If you remove it, expect ObjectDisposedExceptions.
                     * This synthetically keeps Socket objects alive, with their reference in the list, long enough to process the request. On clear, the references are 
                     * released and the object is disposed. We do this every 50 entries to avoid more overhead. It works. C!
                     */
                    if (socketList.Count > 100)
                    {
                        socketList.RemoveRange(0, 50);
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("ENDBLOCK: " + ex.StackTrace);
            }
        }

        #region Support for store and process.
        /*
        public void parseRequest(string request_file, StreamSocket socket)
        {
            concurrentCount++;
            Debug.WriteLine("Concurrent petitions: {0}", concurrentCount);

            IsolatedStorageFileStream fileIsolated = fileIsolatedStorage.OpenFile(request_file, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileIsolated);
            AppServerRequestResponse request = new AppServerRequestResponse();

            // Read data.
            string readLine = streamReader.ReadLine();
            if (readLine != null && readLine.Length > 0)
            {
                // Process request type.
                if (readLine.Substring(0, 3) == "GET")
                {
                    request.httpMethod = "GET";
                    request.httpUri = readLine.Substring(4);
                    request.httpUri = Regex.Replace(request.httpUri, " HTTP.*$", "");
                }
                else if (readLine.Substring(0, 4) == "POST")
                {
                    request.httpMethod = "POST";
                    request.httpUri = readLine.Substring(5);
                    request.httpUri = Regex.Replace(request.httpUri, " HTTP.*$", "");
                }

                // Process request headers.
                Dictionary<string, string> headers = new Dictionary<string, string>();
                string readLineHeader = "";
                do
                {
                    readLineHeader = streamReader.ReadLine();
                    string[] headerSeparator = new string[] { ":" };
                    string[] headerElements = readLineHeader.Split(headerSeparator, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (headerElements.Length > 0)
                    {
                        headers.Add(headerElements[0], headerElements[1]);
                    }
                } while (readLineHeader.Length > 0);
                request.httpHeaders = headers;

                // Assign content body (remaining stream).
                request.httpContent = streamReader.BaseStream;

                // Response writer.
                DataWriter dataWriter = new DataWriter(socket.OutputStream);

                // Process rules.
                bool ruleFound = false;
                if (serverRules != null)
                {
                    //for every rule...
                    foreach (Regex rule_part in serverRules.Keys)
                    {
                        //if it matches the URL
                        if (rule_part.IsMatch(request.httpUri))
                        {
                            // Call delegate.
                            AppServerRequestResponse toSend = serverRules[rule_part](request);
                            ruleFound = true;

                            // Process response headers.
                            if (toSend.httpHeaders.ContainsKey("Location"))
                                // Location change.
                                dataWriter.WriteString("HTTP/1.1 302\r\n");
                            else
                                // OK.
                                dataWriter.WriteString("HTTP/1.1 200 OK\r\n");

                            dataWriter.WriteString("Content-Length: " + toSend.httpContent.Length + "\r\n");
                            foreach (string key in toSend.httpHeaders.Keys)
                            {
                                dataWriter.WriteString(key + ": " + toSend.httpHeaders[key] + "\r\n");
                            }
                            dataWriter.WriteString("Connection: close\r\n");

                            String osPlatform = Environment.OSVersion.Platform.ToString();
#if WINDOWS_APP || WINDOWS 
                            osPlatform = "Windows";
#endif
#if WINDOWS_PHONE_APP || WINDOWS_PHONE
                            osPlatform = "Windows Phone";
#endif
#if WINDOWS_PHONE_APP || WINDOWS_PHONE && SILVERLIGHT
                            osPlatform = "Windows Phone Silverlight";
#endif
                            // Internal headers.
                            dataWriter.WriteString("X-Server: Adaptive 1.0 (" + osPlatform + " " + Environment.OSVersion.Version + "/" + Environment.OSVersion.Platform + "/" + Environment.ProcessorCount + " Cores)\r\n");
                            dataWriter.WriteString("X-ServerBind: http://" + this.serverIp + ":" + this.serverPort + "/\r\n");

                            // Process body.
                            dataWriter.WriteString("\r\n");
                            lock (this)
                            {
                                // Reset stream to beginning.
                                toSend.httpContent.Seek(0, SeekOrigin.Begin);
                                Task.Run(async () =>
                                {
                                    await dataWriter.StoreAsync(); // store output
                                    await dataWriter.FlushAsync(); // flush output

                                    // write data to output using 1024 buffer
                                    while (toSend.httpContent.Position < toSend.httpContent.Length)
                                    {
                                        byte[] buffer;
                                        if (toSend.httpContent.Length - toSend.httpContent.Position < 1024)
                                        {
                                            buffer = new byte[toSend.httpContent.Length - toSend.httpContent.Position];
                                        }
                                        else
                                        {
                                            buffer = new byte[1024];
                                        }
                                        toSend.httpContent.Read(buffer, 0, buffer.Length);
                                        dataWriter.WriteBytes(buffer);

                                        await dataWriter.StoreAsync();
                                        await dataWriter.FlushAsync();
                                    }
                                    toSend.httpContent.Close();
                                });
                            }
                            break;
                        }
                    }
                }

                if (ruleFound == false)
                {
                    dataWriter.WriteString("HTTP/1.1 404 Not Found\r\n");
                    dataWriter.WriteString("Content-Type: text/html\r\n");
                    dataWriter.WriteString("Content-Length: 9\r\n");
                    dataWriter.WriteString("Pragma: no-cache\r\n");
                    dataWriter.WriteString("Connection: close\r\n");
                    dataWriter.WriteString("\r\n");
                    dataWriter.WriteString("Not found");
                    Task.Run(async () =>
                    {
                        await dataWriter.StoreAsync();
                    });
                }
            }
            fileIsolated.Dispose();
            fileIsolated = null;
            concurrentCount--;
        }
        */
        #endregion

        public string getServerAddress()
        {
            if (serverListener != null)
            {
                return this.serverIp;
            }
            else
            {
                return null;
            }
        }

        public int getServerPort()
        {
            if (serverListener != null)
            {
                return Convert.ToInt32(this.serverPort);
            }
            else
            {
                return -1;
            }
        }

        public bool isServerListening()
        {
            return this.serverIsListening;
        }

        public override string GetHost()
        {
            return this.serverIp;
        }

        public override int GetPort()
        {
            return Convert.ToInt32(this.serverPort);
        }

        public override string GetScheme()
        {
            return "http";
        }

        public override void PauseServer()
        {
            Debug.WriteLine("Pausing server.");
            this.StopServer();
        }

        public override void ResumeServer()
        {
            Debug.WriteLine("Resuming server.");
            this.StartServer();
        }

        public override void StartServer()
        {
            bool serverInitializing = false;

            serverListener = new StreamSocketListener();
            serverListener.ConnectionReceived += listener_ConnectionReceived_HandleInMemory;

            while (!serverIsListening)
            {
                try
                {
                    if (!serverInitializing)
                    {
                        Task serverTask = new Task(async () =>
                        {
                            try
                            {
                                serverInitializing = true;
                                Debug.WriteLine("Trying to bind to {0}:{1}.", this.serverIp, this.serverPort);
                                
                                if (this.serverIpAll)
                                {
                                    Debug.WriteLine("Binding on all IPv4 and IPv6 interfaces.");
                                    await serverListener.BindServiceNameAsync(this.serverPort);
                                    Debug.WriteLine("Bound to {0}:{1}.", this.serverIp, this.serverPort);
                                    foreach (string ip in getIpAll()) Debug.WriteLine("Bound also to {0}:{1}.", ip, this.serverPort);
                                }
                                else
                                {
                                    Debug.WriteLine("Binding on loopback interface.");
                                    await serverListener.BindEndpointAsync(new Windows.Networking.HostName(this.serverIp), this.serverPort);
                                    Debug.WriteLine("Bound to {0}:{1}.", this.serverIp, this.serverPort);                                  
                                }       
                                this.serverIsListening = true;
                                serverInitializing = false;
                            }
                            catch (Exception e)
                            {
                                serverInitializing = false;
                                if (!this.serverIsListening)
                                {
                                    Debug.WriteLine("Failed to bind to {0}:{1} with error {2}.", this.serverIp, this.serverPort, e);
                                    // Try with next port
                                    int nextPort = Convert.ToInt32(this.serverPort);
                                    if (nextPort > 1024 && nextPort < 32000)
                                    {
                                        nextPort++;
                                        this.serverPort = Convert.ToString(nextPort);
                                    }
                                }
                            }
                        });
                        serverTask.Start();
                        serverTask.Wait(2000);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error binding to {0}:{1} with error {2}.", this.serverIp, this.serverPort, e.Message);
                    serverIsListening = false;
                    serverInitializing = false;
                }
            }
        }

        private List<string> getIpAll()
        {
            List<string> ipList = new List<string>();
            IReadOnlyList<HostName> hostList = NetworkInformation.GetHostNames();

            foreach (HostName host in hostList) {
                /*
                Debug.WriteLine("--------------------------------------------------------------");
                Debug.WriteLine("CanonicalName: {0}", host.CanonicalName);
                Debug.WriteLine("DisplayName: {0}", host.DisplayName);
                Debug.WriteLine("IPInformation: {0}", host.IPInformation);
                Debug.WriteLine("RawName: {0}", host.RawName);
                Debug.WriteLine("Type: {0}", host.Type);
                */
                if ((host.Type == HostNameType.Ipv4 || host.Type == HostNameType.Ipv6) && host.IPInformation!=null)
                {
                    ipList.Add(host.RawName);
                }
            }
            return ipList;
        }

        public override void StopServer()
        {
            if (this.serverIsListening)
            {
                this.serverListener.Dispose();
                this.serverListener = null;
                this.serverIsListening = false;
                socketList.Clear();
                Debug.WriteLine("Unbound from {0}:{1}.", this.serverIp, this.serverPort);
                GC.Collect();
            }
        }

        public override string GetPath()
        {
            return "/";
        }
    }
}
