using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public abstract class AbstractIO : IIo
    {
        private static string SERVICES_CONFIG_FILE = "app/config/io-services-config.xml";
        private static int ABSOLUTE_INVOKE_TIMEOUT = 60000; // 60 seconds
        private static int DEFAULT_READWRITE_TIMEOUT = 15000; // 15 seconds
        private static int DEFAULT_RESPONSE_TIMEOUT = 100000; // 100 seconds
        private static int MAX_BINARY_SIZE = 8 * 1024 * 1024;  // 8 MB
        private static int DEFAULT_BUFFER_READ_SIZE = 4096;	// 4 KB
        private IOServicesConfig servicesConfig = new IOServicesConfig();  // empty list
        private static IDictionary<ServiceType, string> contentTypes = new Dictionary<ServiceType, string>();
        private CookieContainer cookieContainer = null;
        private HttpClient client = null;
        

        static AbstractIO()
        {
            contentTypes[ServiceType.XMLRPC_JSON] = "application/json";
            contentTypes[ServiceType.XMLRPC_XML] = "text/xml";
            contentTypes[ServiceType.REST_JSON] = "application/json";
            contentTypes[ServiceType.REST_XML] = "text/xml";
            contentTypes[ServiceType.SOAP_JSON] = "application/json";
            contentTypes[ServiceType.SOAP_XML] = "text/xml";
            contentTypes[ServiceType.AMF_SERIALIZATION] = "";
            contentTypes[ServiceType.REMOTING_SERIALIZATION] = "";
            contentTypes[ServiceType.OCTET_BINARY] = "application/octet-stream";
            contentTypes[ServiceType.GWT_RPC] = "text/x-gwt-rpc; charset=utf-8";

        }

        private string _servicesConfigFile = SERVICES_CONFIG_FILE;
        private string _IOUserAgent = "Unity 1.0";

        public virtual string IOUserAgent
        {
            get
            {
                return this._IOUserAgent;
            }

            set
            {
                this._IOUserAgent = value;
            }
        }

        public virtual string ServicesConfigFile
        {
            get
            {
                return this._servicesConfigFile;
            }
            set
            {
                this._servicesConfigFile = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AbstractIO()
        {
            LoadServicesConfig();
            CreateNewHttpClient();
        }

        private void CreateNewHttpClient()
        {
            this.cookieContainer = new CookieContainer();
            this.client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip, CookieContainer = this.cookieContainer });
            //ONLY DEFINE PROPERTIES ONCE, IF A REQUEST HAS BEEN SENT, HTTPCLIENT CANNOT MODIFY PROPERTIES
            this.client.Timeout = new TimeSpan(0, 0, DEFAULT_RESPONSE_TIMEOUT * 5);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", this.IOUserAgent);
        }

        /// <summary>
        /// Default method, to be overrided by platform implementation. 
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/>
        /// </returns>
        public virtual byte[] GetConfigFileBinaryData()
        {
            Debug.WriteLine("# Loading IO Services Configuration from file: " + ServicesConfigFile);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void LoadServicesConfig()
        {
            //throw new NotImplementedException();
        }


        public abstract String GetDirectoryRoot();

        #region Miembros de IIo

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOService[] GetServices()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the IO Service that matches the given name.
        /// </summary>
        /// <param name="name">Service name to match.</param>
        /// <returns>IO Service.</returns>
        public IOService GetService(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the IO Service that matches the given name and type.
        /// </summary>
        /// <param name="name">Service name to match.</param>
        /// <param name="type">Service type to match.</param>
        /// <returns>IO Service.</returns>
        public IOService GetService(string name, ServiceType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Callback to accept all certificates
        /// </summary>
        /// <returns>
        /// <c>true</c>, if web certificates was validated, <c>false</c> otherwise.
        /// </returns>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='certificate'>
        /// Certificate.
        /// </param>
        /// <param name='chain'>
        /// Chain.
        /// </param>
        /// <param name='sslPolicyErrors'>
        /// Ssl policy errors.
        /// </param>
        /*
        public virtual bool ValidateWebCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            Debug.WriteLine("*************** On ServerCertificateValidationCallback: accept all certificates");
            return true;
        }
        */

        /// <summary>
        /// Checks the invoke timeout.
        /// </summary>
        /// <param name='requestObject'>
        /// Request object.
        /// </param>
        private void CheckInvokeTimeout(object requestObject)
        {

            Task.Delay(ABSOLUTE_INVOKE_TIMEOUT);
            Debug.WriteLine("Absolute timeout checking completed.");
            HttpWebRequest req = requestObject as HttpWebRequest;
            if (req != null)
            {
                Debug.WriteLine("Aborting request...");
                req.Abort();
                // this causes a WebException with the Status property set to RequestCanceled
                // for any subsequent call to the GetResponse, BeginGetResponse, EndGetResponse, GetRequestStream, BeginGetRequestStream, or EndGetRequestStream methods.
            }
        }


        private string FormatRequestUriString(IORequest request, IOService service, string reqMethod)
        {

            string requestUriString = String.Format("{0}:{1}{2}", service.Endpoint.Host, service.Endpoint.Port, service.Endpoint.Path);
            if (service.Endpoint.Port == 0)
            {
                requestUriString = String.Format("{0}{1}", service.Endpoint.Host, service.Endpoint.Path);
            }

            if (reqMethod.Equals(RequestMethod.GET.ToString()) && request.Content != null)
            {
                // add request content to the URI string when NOT POST method (GET, PUT, DELETE).
                requestUriString += request.Content;
            }

            Debug.WriteLine("Requesting service: " + requestUriString);
            return requestUriString;
        }

        private HttpRequestMessage BuildWebRequest(IORequest request, IOService service, string requestUriString, string reqMethod)
        {
            //Not needed
            //client.BaseAddress = new Uri(requestUriString);
            
            HttpRequestMessage webRequest = new HttpRequestMessage(HttpMethod.Post, requestUriString);
            
            //HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(requestUriString);
            //webReq.Method = reqMethod; // default is POST
            webRequest.Method = HttpMethod.Post;
            //webReq.ContentType = contentTypes[service.Type];
            // check specific request ContentType defined, and override service type in that case
            if (webRequest.Content == null) webRequest.Content = new ByteArrayContent(request.GetRawContent());
            String sContentType = request.ContentType;

            if (String.IsNullOrWhiteSpace(sContentType))
            {
                sContentType = contentTypes[service.Type];
            }

            //Debug.WriteLine("Request content type: " + webReq.ContentType);
            //Debug.WriteLine("Request method: " + webReq.Method);
            Debug.WriteLine("Request method: " + webRequest.Method);

            //webReq.Accept = webReq.ContentType; // setting "Accept" header with the same value as "Content Type" header, it is needed to be defined for some services.
            webRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(sContentType));
            
            //webReq.ContentLength = request.GetContentLength();
            webRequest.Content.Headers.ContentLength = request.GetContentLength();
            Debug.WriteLine("Request content length: " + request.GetContentLength());
            webRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(sContentType);
            //webReq.ContinueTimeout = DEFAULT_RESPONSE_TIMEOUT; // in millisecods (default is 100 seconds)
            //webReq.ContinueTimeout = DEFAULT_READWRITE_TIMEOUT; // in milliseconds
            //client.Timeout = new TimeSpan(DEFAULT_READWRITE_TIMEOUT);
            
            //webReq.KeepAlive = false;
            
            webRequest.Headers.Add("Keep-Alive", "true");
            
            //webReq.ProtocolVersion = HttpVersion.Version10;
            //webRequest.Version = Version.Parse("1.0"); NOT NEEDED
            //if (request.ProtocolVersion == HTTPProtocolVersion.HTTP11) webReq.ProtocolVersion = HttpVersion.Version11;


            //webReq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            // user agent needs to be informed - some servers check this parameter and send 500 errors when not informed.
            //webReq.UserAgent = this.IOUserAgent;
            
            //webRequest.Headers.UserAgent.ParseAdd(this.IOUserAgent);
            //webRequest.Content.Headers.Add("User-Agent", this.IOUserAgent);
            
            Debug.WriteLine("Request UserAgent : " + this.IOUserAgent);
            
            /*************
             * HEADERS HANDLING
             *************/

            // add specific headers to the request
            if (request.Headers != null && request.Headers.Length > 0)
            {
                foreach (IOHeader header in request.Headers)
                {
                    //webReq.Headers.Add(header.Name, header.Value);
                    webRequest.Headers.Add(header.Name, header.Value);
                    //Debug.WriteLine("Added request header: " + header.Name + "=" + webRequest.Headers.[header.Name]);
                }
            }

            /*************
             * COOKIES HANDLING
             *************/

            // Assign the cookie container on the request to hold cookie objects that are sent on the response.
            // Required even though you no cookies are send.
            //webReq.CookieContainer = this.cookieContainer;
            //webRequest.Properties.
            
            // add cookies to the request cookie container
            if (request.Session != null && request.Session.Cookies != null && request.Session.Cookies.Length > 0)
            {
                foreach (IOCookie cookie in request.Session.Cookies)
                {
                    if (cookie != null && cookie.Name != null)
                    {
                        //webReq.CookieContainer.Add(webReq.RequestUri, new Cookie(cookie.Name, cookie.Value));
                        //this.cookieContainer.Add(new Uri(requestUriString), new Cookie(cookie.Name, cookie.Value));
                        this.cookieContainer.Add(webRequest.RequestUri, new Cookie(cookie.Name, cookie.Value));
                        Debug.WriteLine("Added cookie [" + cookie.Name + "] to request.");
                    }
                }
            }
            Debug.WriteLine("HTTP Request cookies: " + this.cookieContainer.GetCookieHeader(new Uri(requestUriString)));

            /*************
             * SETTING A PROXY (ENTERPRISE ENVIRONMENTS)
             *************/
            /*
            if (service.Endpoint.ProxyUrl != null)
            {
                WebProxy myProxy = new WebProxy();
                Uri proxyUri = new Uri(service.Endpoint.ProxyUrl);
                myProxy.Address = proxyUri;
                webReq.Proxy = myProxy;
            }
            */
            return webRequest;
        }

        private async Task<IOResponse> ReadWebResponse(HttpRequestMessage webRequest, HttpResponseMessage webResponse, IOService service)
        {
            IOResponse response = new IOResponse();

            // result types (string or byte array)
            byte[] resultBinary = null;
            string result = null;

            string responseMimeTypeOverride = webResponse.Content.Headers.ContentType.MediaType;

            using (Stream stream = await webResponse.Content.ReadAsStreamAsync())
            {
                Debug.WriteLine("getting response stream...");
                if (ServiceType.OCTET_BINARY.Equals(service.Type))
                {

                    int lengthContent = (int) webResponse.Content.Headers.ContentLength;
                    // testing log line
                    // SystemLogger.Log (SystemLogger.Module.CORE, "content-length header: " + lengthContent +", max file size: " + MAX_BINARY_SIZE);
                    int bufferReadSize = DEFAULT_BUFFER_READ_SIZE;
                    if (lengthContent >= 0 && lengthContent <= bufferReadSize)
                    {
                        bufferReadSize = lengthContent;
                    }

                    if (lengthContent > MAX_BINARY_SIZE)
                    {
                        Debug.WriteLine("WARNING! - file exceeds the maximum size defined in platform (" + MAX_BINARY_SIZE + " bytes)");
                    }
                    else
                    {
                        // Read to end of stream in blocks
                        Debug.WriteLine("buffer read: " + bufferReadSize + " bytes");
                        MemoryStream memBuffer = new MemoryStream();
                        byte[] readBuffer = new byte[bufferReadSize];
                        int readLen = 0;
                        do
                        {
                            readLen = stream.Read(readBuffer, 0, readBuffer.Length);
                            memBuffer.Write(readBuffer, 0, readLen);
                        } while (readLen > 0);

                        resultBinary = memBuffer.ToArray();
                        memBuffer.Flush();
                        memBuffer = null;
                    }
                }
                else
                {
                    Debug.WriteLine("reading response content...");
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            /*************
             * CACHE
             *************/

            // preserve cache-control header from remote server, if any
            string cacheControlHeader = (webResponse.Headers.CacheControl!=null)? webResponse.Headers.CacheControl.ToString():String.Empty;
            if (!String.IsNullOrWhiteSpace(cacheControlHeader))
            {
                Debug.WriteLine("Found Cache-Control header on response: " + cacheControlHeader + ", using it on internal response...");
                if (response.Headers == null)
                {
                    response.Headers = new IOHeader[1];
                }
                IOHeader cacheHeader = new IOHeader();
                cacheHeader.Name = "Cache-Control";
                cacheHeader.Value = cacheControlHeader;
                response.Headers[0] = cacheHeader;
            }

            /*************
             * COOKIES HANDLING
             *************/

            // get response cookies (stored on cookiecontainer)
            if (response.Session == null)
            {
                response.Session = new IOSessionContext();

            }
            response.Session.Cookies = new IOCookie[this.cookieContainer.Count];
            IEnumerator enumerator = this.cookieContainer.GetCookies(webRequest.RequestUri).GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                Cookie cookieFound = (Cookie)enumerator.Current;
                Debug.WriteLine("Found cookie on response: " + cookieFound.Name + "=" + cookieFound.Value);
                IOCookie cookie = new IOCookie();
                cookie.Name = cookieFound.Name;
                cookie.Value = cookieFound.Value;
                response.Session.Cookies[i] = cookie;
                i++;
            }

            if (ServiceType.OCTET_BINARY.Equals(service.Type))
            {
                if (responseMimeTypeOverride != null && !responseMimeTypeOverride.Equals(contentTypes[service.Type]))
                {
                    response.ContentType = responseMimeTypeOverride;
                }
                else
                {
                    response.ContentType = contentTypes[service.Type];
                }
                response.ContentBinary = resultBinary; // Assign binary content here
            }
            else
            {
                response.ContentType = contentTypes[service.Type];
                response.Content = result;
            }


            return response;

        }

        /*
        private async string ReadWebResponseAndStore(HttpRequestMessage webRequest, HttpResponseMessage webResponse, IOService service, string storePath)
        {

            using (Stream stream = await webResponse.Content.ReadAsStreamAsync())
            {
                Debug.WriteLine("getting response stream...");

                int lengthContent = (int)webResponse.Content.Headers.ContentLength;
                // testing log line
                // SystemLogger.Log (SystemLogger.Module.CORE, "content-length header: " + lengthContent +", max file size: " + MAX_BINARY_SIZE);
                int bufferReadSize = DEFAULT_BUFFER_READ_SIZE;
                if (lengthContent >= 0 && lengthContent <= bufferReadSize)
                {
                    bufferReadSize = lengthContent;
                }
                Debug.WriteLine("buffer read: " + bufferReadSize + " bytes");
                string fullStorePath = Path.Combine(this.GetDirectoryRoot(), storePath);
                Debug.WriteLine("storing file at: " + fullStorePath);
                //FileStream streamWriter = new FileStream(fullStorePath, FileMode.Create);

                byte[] readBuffer = new byte[bufferReadSize];
                int readLen = 0;
                int totalRead = 0;
                do
                {
                    readLen = stream.Read(readBuffer, 0, readBuffer.Length);
                    streamWriter.Write(readBuffer, 0, readLen);
                    totalRead = totalRead + readLen;
                } while (readLen > 0);


                Debug.WriteLine("total bytes: " + totalRead);
                streamWriter.Close();
                streamWriter = null;

                return storePath;
            }
        }
         * */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public virtual IOResponse InvokeService(IORequest request, IOService service)
        {
            IOResponse response = new IOResponse();
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            Task.Run( async () => {

                if (service != null)
                {

                    if (service.Endpoint == null)
                    {
                        Debug.WriteLine("No endpoint configured for this service name: " + service.Name);
                        //return response;
                    }

                    Debug.WriteLine("Request content: " + request.Content);
                    byte[] requestData = request.GetRawContent();

                    String reqMethod = service.RequestMethod.ToString(); // default is POST
                    if (request.Method != null && request.Method != String.Empty) reqMethod = request.Method.ToUpper();

                    String requestUriString = this.FormatRequestUriString(request, service, reqMethod);

                    try
                    {

                        // Security - VALIDATIONS
                        //ServicePointManager.ServerCertificateValidationCallback = ValidateWebCertificates;

                        // Building Web Request to send
                        HttpRequestMessage webReq = this.BuildWebRequest(request, service, requestUriString, reqMethod);

                        // Throw a new Thread to check absolute timeout
                        //timeoutThread = new Thread(CheckInvokeTimeout);
                        //timeoutThread.Start(webReq);

                        // POSTING DATA using timeout
                        //DONE IN BuildWebRequest METHOD
                       /* if (!reqMethod.Equals(RequestMethod.GET.ToString()) && requestData != null)
                        {
                            // send data only for POST method.
                            Debug.WriteLine("Sending data on the request stream... (POST)");
                            Debug.WriteLine("request data length: " + requestData.Length);
                            using (Stream requestStream = await webReq.Content.ReadAsStreamAsync())
                            {
                                Debug.WriteLine("request stream: " + requestStream);
                                requestStream.Write(requestData, 0, requestData.Length);
                            }
                        }*/

                        HttpResponseMessage webResp = await client.SendAsync(webReq);
                        Debug.WriteLine("getting response...");
                        response = await this.ReadWebResponse(webReq, webResp, service);
                    

                    }
                    catch (WebException ex)
                    {
                        Debug.WriteLine("WebException requesting service: " + requestUriString + ".", ex);
                        response.ContentType = contentTypes[ServiceType.REST_JSON];
                        response.Content = "WebException Requesting Service: " + requestUriString + ". Message: " + ex.Message;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Unnandled Exception requesting service: " + requestUriString + ".", ex);
                        response.ContentType = contentTypes[ServiceType.REST_JSON];
                        response.Content = "Unhandled Exception Requesting Service: " + requestUriString + ". Message: " + ex.Message;
                    }
                    finally
                    {

                    }
                }
                else
                {
                    Debug.WriteLine("Null service received for invoking.");
                }
                resetEvent.Set();
            });
            resetEvent.WaitOne();
            return response;
        }

        /// <summary>
        /// Invokes a service for getting a big binary, storing it into filesystem and returning the reference url.
        /// Only OCTET_BINARY service types are allowed.
        /// </summary>
        /// <returns>The reference Url for the stored file (if success, null otherwise.</returns>
        /// <param name="request">Request.</param>
        /// <param name="service">Service.</param>
        /// <param name="storePath">Store path.</param>
        /*
        public virtual string InvokeServiceForBinary(IORequest request, IOService service, string storePath)
        {

            if (service != null)
            {

                if (service.Endpoint == null)
                {
                    Debug.WriteLine("No endpoint configured for this service name: " + service.Name);
                    return null;
                }

                if (!ServiceType.OCTET_BINARY.Equals(service.Type))
                {
                    Debug.WriteLine("This method only admits OCTET_BINARY service types");
                    return null;
                }

                Debug.WriteLine("Request content: " + request.Content);
                byte[] requestData = request.GetRawContent();

                String reqMethod = service.RequestMethod.ToString(); // default is POST
                if (request.Method != null && request.Method != String.Empty) reqMethod = request.Method.ToUpper();

                String requestUriString = this.FormatRequestUriString(request, service, reqMethod);
                //Thread timeoutThread = null;

                try
                {

                    // Security - VALIDATIONS
                    //ServicePointManager.ServerCertificateValidationCallback = ValidateWebCertificates;

                    // Building Web Request to send
                    HttpRequestMessage webReq = this.BuildWebRequest(request, service, requestUriString, reqMethod);

                    // Throw a new Thread to check absolute timeout
                    //timeoutThread = new Thread(CheckInvokeTimeout);
                    //timeoutThread.Start(webReq);

                    // POSTING DATA using timeout
                    if (!reqMethod.Equals(RequestMethod.GET.ToString()) && requestData != null)
                    {
                        // send data only for POST method.
                        Debug.WriteLine("Sending data on the request stream... (POST)");
                        Debug.WriteLine("request data length: " + requestData.Length);
                        Stream requestStream = await webReq.Content.ReadAsStreamAsync();

                        Debug.WriteLine("request stream: " + requestStream);
                        requestStream.Write(requestData, 0, requestData.Length);

                    }

                    HttpResponseMessage webResp = await client.SendAsync(webReq);
                    {

                        Debug.WriteLine("getting response...");
                        return this.ReadWebResponseAndStore(webReq, webResp, service, storePath);
                    }

                }
                catch (WebException ex)
                {
                    Debug.WriteLine("WebException requesting service: " + requestUriString + ".", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unnandled Exception requesting service: " + requestUriString + ".", ex);
                }
                finally
                {
                    // abort any previous timeout checking thread
                    if (timeoutThread != null && timeoutThread.IsAlive)
                    {
                        timeoutThread.Abort();
                    }
                }
            }
            else
            {
                Debug.WriteLine("Null service received for invoking.");
            }

            return null;
        }
         * */

        /// <summary>
        /// Invokes service, given its name, using the provided request.
        /// </summary>
        /// <param name="request">IO request.</param>
        /// <param name="serviceName">Service Name.</param>
        /// <returns>IO response.</returns>
        public IOResponse InvokeService(IORequest request, string serviceName)
        {
            return InvokeService(request, GetService(serviceName));
        }

        /// <summary>
        /// Invokes service, given its name and type, using the provided request.
        /// </summary>
        /// <param name="request">IO request.</param>
        /// <param name="serviceName"></param>
        /// <param name="type"></param>
        /// <returns>IO response.</returns>
        public IOResponse InvokeService(IORequest request, string serviceName, ServiceType type)
        {
            return InvokeService(request, GetService(serviceName, type));
        }

        public IOResponseHandle InvokeService(IORequest request, IOService service, IOResponseHandler handler)
        {
            throw new NotImplementedException();
        }

        public IOResponseHandle InvokeService(IORequest request, string serviceName, IOResponseHandler handler)
        {
            throw new NotImplementedException();
        }

        public IOResponseHandle InvokeService(IORequest request, string serviceName, ServiceType type, IOResponseHandler handler)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
