/**
--| ADAPTIVE RUNTIME PLATFORM |----------------------------------------------------------------------------------------

(C) Copyright 2013-2015 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the
License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 . Unless required by appli-
-cable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,  WITHOUT
WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the  License  for the specific language governing
permissions and limitations under the License.

Original author:

    * Carlos Lozano Diez
            <http://github.com/carloslozano>
            <http://twitter.com/adaptivecoder>
            <mailto:carlos@adaptive.me>

Contributors:

    * Ferran Vila Conesa
             <http://github.com/fnva>
             <http://twitter.com/ferran_vila>
             <mailto:ferran.vila.conesa@gmail.com>

    * See source code files for contributors.

Release:

    * @version v2.2.0

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Network reachability operations
        Auto-generated implementation of INetworkReachability specification.
     */
public class NetworkReachabilityBridge : BaseCommunicationBridge, INetworkReachability, APIBridge
{

          /**
             API Delegate.
          */
          private INetworkReachability _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public NetworkReachabilityBridge(INetworkReachability _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return INetworkReachability delegate that manages platform specific functions..
          */
          public INetworkReachability GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(INetworkReachability _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Whether there is connectivity to a host, via domain name or ip address, or not.

             @param host     domain name or ip address of host.
             @param callback Callback called at the end.
             @since v2.0
          */
          public void IsNetworkReachable(string host, INetworkReachabilityCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkReachabilityBridge executing isNetworkReachable({"+host+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.IsNetworkReachable(host, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkReachabilityBridge executed 'isNetworkReachable' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"NetworkReachabilityBridge no delegate for 'isNetworkReachable'.");
                    }
                    }
                    
               }

          /**
             Whether there is connectivity to an url of a service or not.

             @param url      to look for
             @param callback Callback called at the end
             @since v2.0
          */
          public void IsNetworkServiceReachable(string url, INetworkReachabilityCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkReachabilityBridge executing isNetworkServiceReachable({"+url+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.IsNetworkServiceReachable(url, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkReachabilityBridge executed 'isNetworkServiceReachable' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"NetworkReachabilityBridge no delegate for 'isNetworkServiceReachable'.");
                    }
                    }
                    
               }

          /**
             Invokes the given method specified in the API request object.

             @param request APIRequest object containing method name and parameters.
             @return APIResponse with status code, message and JSON response or a JSON null string for void functions. Status code 200 is OK, all others are HTTP standard error conditions.
          */
          public new APIResponse Invoke(APIRequest request)
          {
               APIResponse response = new APIResponse();
               int responseCode = 200;
               String responseMessage = "OK";
               String responseJSON = "null";
               switch (request.GetMethodName())
               {
                    case "isNetworkReachable":
                         string host0 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         INetworkReachabilityCallback callback0 = new NetworkReachabilityCallbackImpl(request.GetAsyncId());
                         this.IsNetworkReachable(host0, callback0);
                         break;
                    case "isNetworkServiceReachable":
                         string url1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         INetworkReachabilityCallback callback1 = new NetworkReachabilityCallbackImpl(request.GetAsyncId());
                         this.IsNetworkServiceReachable(url1, callback1);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "NetworkReachabilityBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.2.0.";
                         break;
               }
               response.SetResponse(responseJSON);
               response.SetStatusCode(responseCode);
               response.SetStatusMessage(responseMessage);
               return response;
          }
     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
