using Adaptive.Arp.Impl.WinPhone.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    
    public class AppverseBridge
    {
        public AppServerRequestResponse HandleAppverse(AppServerRequestResponse request)
        {
            AppServerRequestResponse newResponse = new AppServerRequestResponse();
            Stream responseStream = new MemoryStream();

            Task task = new Task(async () =>
            {

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
    }
}
