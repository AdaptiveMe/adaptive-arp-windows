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
using System.Net;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    //VALID FOR 8.1 AND SILVERLIGHT
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
                    HttpWebRequest request = WebRequest.CreateHttp(url);
                    request.Method = "HEAD";
                    request.GetResponseAsync().ContinueWith(x =>
                    {
                        var response = (HttpWebResponse)x.Result;
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                callback.OnResult(url);
                                break;
                        }
                    });
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