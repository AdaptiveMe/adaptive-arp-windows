using Adaptive.Arp.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        // Used only for when handling requests with file handler.
        IsolatedStorageFile fileIsolatedStorage = null;
        protected Random filenameRandomizer = null;

        // Internal Events
        public event ErrorOccured errorOccured;

        // Server Stuff
        private StreamSocketListener serverListener = new StreamSocketListener();
        private bool serverIsListening = false;
        private Dictionary<Regex, RuleDeletage> serverRules;
        private string serverIp = "127.0.0.1";
        private string serverPort = "-1";


        public AppServerImpl(Dictionary<Regex, RuleDeletage> rules, string ip, string port, bool useFileSystem)
        {
            //assign passed rules to the server
            serverRules = rules;
            try
            {
                //try to turn on the server
                

                Task.Run(async () =>
                {
                    // Receive connection events
                    // This serverListener handles very large posts such as file uploads but at a performance penalty.
                    // serverListener.ConnectionReceived += listener_ConnectionReceived_HandleAsFile;
                    // This serverListener handles requests in memory for fast performance but does not handle large posts efficiently.
                    serverListener.ConnectionReceived += listener_ConnectionReceived_HandleInMemory;
                    
                    //bing do ip:port
                    await serverListener.BindEndpointAsync(new Windows.Networking.HostName(ip), port);
                    
                    serverIp = ip;
                    serverPort = port;
                    serverIsListening = true;
                });
            }
            catch (Exception ex)
            {
                //if possible fire the error event with the exception message
                if (errorOccured != null)
                {
                    errorOccured(-1, ex.Message);
                }
            }

        }

        public AppServerImpl(Dictionary<Regex, RuleDeletage> rules, string port)
            : this(rules, "127.0.0.1", "8080", false)
        {

        }

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

        void listener_ConnectionReceived_HandleInMemory(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            if (serverIsListening == false)
            {
                // If local server is not listening, ignore call.
                return;
            }

            // Temporary file to handle large post requests
            MemoryStream memoryStream = new MemoryStream();

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
                    parseRequest(memoryStream, socket);
                }
            });
        }

        public void parseRequest(MemoryStream stream, StreamSocket socket)
        {

            StreamReader streamReader = new StreamReader(stream);
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
            stream.Dispose();
            stream = null;
        }

        public void parseRequest(string request_file, StreamSocket socket)
        {
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
                    string[] headerSeparator = new string[] {":"};
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
        }

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
            throw new NotImplementedException();
        }

        public override int GetPort()
        {
            throw new NotImplementedException();
        }

        public override string GetScheme()
        {
            throw new NotImplementedException();
        }

        public override void PauseServer()
        {
            throw new NotImplementedException();
        }

        public override void ResumeServer()
        {
            throw new NotImplementedException();
        }

        public override void StartServer()
        {
            throw new NotImplementedException();
        }

        public override void StopServer()
        {
            throw new NotImplementedException();
        }

        public override string GetPath()
        {
            throw new NotImplementedException();
        }
    }
}
