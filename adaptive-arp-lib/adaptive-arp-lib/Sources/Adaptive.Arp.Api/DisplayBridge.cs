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
        Interface for Managing the Display operations
        Auto-generated implementation of IDisplay specification.
     */
public class DisplayBridge : BaseSystemBridge, IDisplay, APIBridge
{

          /**
             API Delegate.
          */
          private IDisplay _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public DisplayBridge(IDisplay _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IDisplay delegate that manages platform specific functions..
          */
          public IDisplay GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IDisplay _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Add a listener to start receiving display orientation change events.

             @param listener Listener to add to receive orientation change events.
             @since v2.0.5
          */
          public void AddDisplayOrientationListener(IDisplayOrientationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executing addDisplayOrientationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddDisplayOrientationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executed 'addDisplayOrientationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DisplayBridge no delegate for 'addDisplayOrientationListener'.");
                    }
                    }
                    
               }

          /**
             Returns the current orientation of the display. Please note that this may be different from the orientation
of the device. For device orientation, use the IDevice APIs.

             @return The current orientation of the display.
             @since v2.0.5
          */
          public ICapabilitiesOrientation GetOrientationCurrent()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executing getOrientationCurrent.");
               }

               ICapabilitiesOrientation result = ICapabilitiesOrientation.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetOrientationCurrent();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executed 'getOrientationCurrent' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DisplayBridge no delegate for 'getOrientationCurrent'.");
                    }
                    }
                    return result;                    
               }

          /**
             Remove a listener to stop receiving display orientation change events.

             @param listener Listener to remove from receiving orientation change events.
             @since v2.0.5
          */
          public void RemoveDisplayOrientationListener(IDisplayOrientationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executing removeDisplayOrientationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveDisplayOrientationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executed 'removeDisplayOrientationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DisplayBridge no delegate for 'removeDisplayOrientationListener'.");
                    }
                    }
                    
               }

          /**
             Remove all listeners receiving display orientation events.

             @since v2.0.5
          */
          public void RemoveDisplayOrientationListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executing removeDisplayOrientationListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveDisplayOrientationListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DisplayBridge executed 'removeDisplayOrientationListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DisplayBridge no delegate for 'removeDisplayOrientationListeners'.");
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
                    case "addDisplayOrientationListener":
                         IDisplayOrientationListener listener0 = new DisplayOrientationListenerImpl(request.GetAsyncId());
                         this.AddDisplayOrientationListener(listener0);
                         break;
                    case "getOrientationCurrent":
                         ICapabilitiesOrientation response1 = this.GetOrientationCurrent();
                         responseJSON = GetJSONProcessor().SerializeObject(response1);
                         break;
                    case "removeDisplayOrientationListener":
                         IDisplayOrientationListener listener2 = new DisplayOrientationListenerImpl(request.GetAsyncId());
                         this.RemoveDisplayOrientationListener(listener2);
                         break;
                    case "removeDisplayOrientationListeners":
                         this.RemoveDisplayOrientationListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "DisplayBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.2.";
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
