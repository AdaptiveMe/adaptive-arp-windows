/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * David Barranco Bonilla
 *             <https://github.com/aryslan>
 *             <mailto:ddbc@gft.com>
 *
 * =====================================================================================================================
 */

using Adaptive.Arp.Api;
using System;
using System.Net;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class NetworkReachabilityImpl : INetworkReachability
    {
        /// <summary>
        /// Tries to reach the specified URL to know if it is reachable
        /// </summary>
        /// <param name="url">URL to connect</param>
        /// <param name="callback">object that will be notified when results are ready</param>
        public void IsNetworkReachable(string url, INetworkReachabilityCallback callback)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                    {
                        HttpWebRequest request = WebRequest.CreateHttp(url);
                        request.Method = "HEAD";
                        request.GetResponseAsync().ContinueWith(x =>
                        {
                            var response = (HttpWebResponse)x.Result;
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.Forbidden:
                                    callback.OnError(INetworkReachabilityCallback.Error.Forbidden);
                                    break;

                                case HttpStatusCode.NotFound:
                                    callback.OnError(INetworkReachabilityCallback.Error.NotFound);
                                    break;

                                case HttpStatusCode.MethodNotAllowed:
                                    callback.OnError(INetworkReachabilityCallback.Error.MethodNotAllowed);
                                    break;

                                case HttpStatusCode.Unauthorized:
                                    //NOT ALLOWED
                                    callback.OnError(INetworkReachabilityCallback.Error.NotAllowed);
                                    break;

                                case HttpStatusCode.RequestTimeout:
                                case HttpStatusCode.GatewayTimeout:
                                    callback.OnError(INetworkReachabilityCallback.Error.TimeOut);
                                    break;

                                case HttpStatusCode.OK:
                                    callback.OnResult(url);
                                    break;

                                case HttpStatusCode.Redirect:
                                    callback.OnWarning(url, INetworkReachabilityCallback.Warning.Redirected);
                                    break;

                                case HttpStatusCode.NotImplemented:
                                    callback.OnWarning(url, INetworkReachabilityCallback.Warning.NotRegisteredService);
                                    break;
                            }
                        });
                    }
                    else
                    {
                        callback.OnWarning(url, INetworkReachabilityCallback.Warning.MalformedUrl);
                    }
                }
                else
                {
                    callback.OnError(INetworkReachabilityCallback.Error.Wrong_Params);
                }
            }
            catch (System.Exception)
            {
                callback.OnError(INetworkReachabilityCallback.Error.Forbidden);
            }
        }
    }
}