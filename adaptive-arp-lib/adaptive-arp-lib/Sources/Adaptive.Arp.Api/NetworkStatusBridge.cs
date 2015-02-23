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
        Interface for Managing the Network status
        Auto-generated implementation of INetworkStatus specification.
     */
public class NetworkStatusBridge : BaseCommunicationBridge, INetworkStatus, APIBridge
{

          /**
             API Delegate.
          */
          private INetworkStatus _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public NetworkStatusBridge(INetworkStatus _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return INetworkStatus delegate that manages platform specific functions..
          */
          public INetworkStatus GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(INetworkStatus _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Add the listener for network status changes of the app

             @param listener Listener with the result
             @since v2.0
          */
          public void AddNetworkStatusListener(INetworkStatusListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executing addNetworkStatusListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddNetworkStatusListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executed 'addNetworkStatusListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"NetworkStatusBridge no delegate for 'addNetworkStatusListener'.");
                    }
                    }
                    
               }

          /**
             Un-registers an existing listener from receiving network status events.

             @param listener Listener with the result
             @since v2.0
          */
          public void RemoveNetworkStatusListener(INetworkStatusListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executing removeNetworkStatusListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveNetworkStatusListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executed 'removeNetworkStatusListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"NetworkStatusBridge no delegate for 'removeNetworkStatusListener'.");
                    }
                    }
                    
               }

          /**
             Removes all existing listeners from receiving network status events.

             @since v2.0
          */
          public void RemoveNetworkStatusListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executing removeNetworkStatusListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveNetworkStatusListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"NetworkStatusBridge executed 'removeNetworkStatusListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"NetworkStatusBridge no delegate for 'removeNetworkStatusListeners'.");
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
                    case "addNetworkStatusListener":
                         INetworkStatusListener listener0 = new NetworkStatusListenerImpl(request.GetAsyncId());
                         this.AddNetworkStatusListener(listener0);
                         break;
                    case "removeNetworkStatusListener":
                         INetworkStatusListener listener1 = new NetworkStatusListenerImpl(request.GetAsyncId());
                         this.RemoveNetworkStatusListener(listener1);
                         break;
                    case "removeNetworkStatusListeners":
                         this.RemoveNetworkStatusListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "NetworkStatusBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.2.0.";
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
