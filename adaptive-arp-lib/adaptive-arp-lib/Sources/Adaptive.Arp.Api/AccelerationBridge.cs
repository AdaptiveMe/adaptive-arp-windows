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

    * @version v2.1.5

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface defining methods about the acceleration sensor
        Auto-generated implementation of IAcceleration specification.
     */
public class AccelerationBridge : BaseSensorBridge, IAcceleration, APIBridge
{

          /**
             API Delegate.
          */
          private IAcceleration _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public AccelerationBridge(IAcceleration _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IAcceleration delegate that manages platform specific functions..
          */
          public IAcceleration GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IAcceleration _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Register a new listener that will receive acceleration events.

             @param listener to be registered.
             @since v2.0
          */
          public void AddAccelerationListener(IAccelerationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executing addAccelerationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddAccelerationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executed 'addAccelerationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AccelerationBridge no delegate for 'addAccelerationListener'.");
                    }
                    }
                    
               }

          /**
             De-registers an existing listener from receiving acceleration events.

             @param listener to be registered.
             @since v2.0
          */
          public void RemoveAccelerationListener(IAccelerationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executing removeAccelerationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveAccelerationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executed 'removeAccelerationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AccelerationBridge no delegate for 'removeAccelerationListener'.");
                    }
                    }
                    
               }

          /**
             Removed all existing listeners from receiving acceleration events.

             @since v2.0
          */
          public void RemoveAccelerationListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executing removeAccelerationListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveAccelerationListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AccelerationBridge executed 'removeAccelerationListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AccelerationBridge no delegate for 'removeAccelerationListeners'.");
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
                    case "addAccelerationListener":
                         IAccelerationListener listener0 = new AccelerationListenerImpl(request.GetAsyncId());
                         this.AddAccelerationListener(listener0);
                         break;
                    case "removeAccelerationListener":
                         IAccelerationListener listener1 = new AccelerationListenerImpl(request.GetAsyncId());
                         this.RemoveAccelerationListener(listener1);
                         break;
                    case "removeAccelerationListeners":
                         this.RemoveAccelerationListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "AccelerationBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.5.";
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
