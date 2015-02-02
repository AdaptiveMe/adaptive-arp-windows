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

    * @version v2.1.2

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Geolocation operations
        Auto-generated implementation of IGeolocation specification.
     */
public class GeolocationBridge : BaseSensorBridge, IGeolocation, APIBridge
{

          /**
             API Delegate.
          */
          private IGeolocation _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public GeolocationBridge(IGeolocation _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IGeolocation delegate that manages platform specific functions..
          */
          public IGeolocation GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IGeolocation _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Register a new listener that will receive geolocation events.

             @param listener to be registered.
             @since v2.0
          */
          public void AddGeolocationListener(IGeolocationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executing addGeolocationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddGeolocationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executed 'addGeolocationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GeolocationBridge no delegate for 'addGeolocationListener'.");
                    }
                    }
                    
               }

          /**
             De-registers an existing listener from receiving geolocation events.

             @param listener to be registered.
             @since v2.0
          */
          public void RemoveGeolocationListener(IGeolocationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executing removeGeolocationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveGeolocationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executed 'removeGeolocationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GeolocationBridge no delegate for 'removeGeolocationListener'.");
                    }
                    }
                    
               }

          /**
             Removed all existing listeners from receiving geolocation events.

             @since v2.0
          */
          public void RemoveGeolocationListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executing removeGeolocationListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveGeolocationListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GeolocationBridge executed 'removeGeolocationListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GeolocationBridge no delegate for 'removeGeolocationListeners'.");
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
                    case "addGeolocationListener":
                         IGeolocationListener listener0 = new GeolocationListenerImpl(request.GetAsyncId());
                         this.AddGeolocationListener(listener0);
                         break;
                    case "removeGeolocationListener":
                         IGeolocationListener listener1 = new GeolocationListenerImpl(request.GetAsyncId());
                         this.RemoveGeolocationListener(listener1);
                         break;
                    case "removeGeolocationListeners":
                         this.RemoveGeolocationListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "GeolocationBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.2.";
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
