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

    * @version v2.1.4

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Device operations
        Auto-generated implementation of IDevice specification.
     */
public class DeviceBridge : BaseSystemBridge, IDevice, APIBridge
{

          /**
             API Delegate.
          */
          private IDevice _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public DeviceBridge(IDevice _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IDevice delegate that manages platform specific functions..
          */
          public IDevice GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IDevice _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Register a new listener that will receive button events.

             @param listener to be registered.
             @since v2.0
          */
          public void AddButtonListener(IButtonListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing addButtonListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddButtonListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'addButtonListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'addButtonListener'.");
                    }
                    }
                    
               }

          /**
             Add a listener to start receiving device orientation change events.

             @param listener Listener to add to receive orientation change events.
             @since v2.0.5
          */
          public void AddDeviceOrientationListener(IDeviceOrientationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing addDeviceOrientationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddDeviceOrientationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'addDeviceOrientationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'addDeviceOrientationListener'.");
                    }
                    }
                    
               }

          /**
             Returns the device information for the current device executing the runtime.

             @return DeviceInfo for the current device.
             @since v2.0
          */
          public DeviceInfo GetDeviceInfo()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing getDeviceInfo.");
               }

               DeviceInfo result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetDeviceInfo();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'getDeviceInfo' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'getDeviceInfo'.");
                    }
                    }
                    return result;                    
               }

          /**
             Gets the current Locale for the device.

             @return The current Locale information.
             @since v2.0
          */
          public Locale GetLocaleCurrent()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing getLocaleCurrent.");
               }

               Locale result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetLocaleCurrent();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'getLocaleCurrent' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'getLocaleCurrent'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns the current orientation of the device. Please note that this may be different from the orientation
of the display. For display orientation, use the IDisplay APIs.

             @return The current orientation of the device.
             @since v2.0.5
          */
          public ICapabilitiesOrientation GetOrientationCurrent()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing getOrientationCurrent.");
               }

               ICapabilitiesOrientation result = ICapabilitiesOrientation.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetOrientationCurrent();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'getOrientationCurrent' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'getOrientationCurrent'.");
                    }
                    }
                    return result;                    
               }

          /**
             De-registers an existing listener from receiving button events.

             @param listener to be removed.
             @since v2.0
          */
          public void RemoveButtonListener(IButtonListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing removeButtonListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveButtonListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'removeButtonListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'removeButtonListener'.");
                    }
                    }
                    
               }

          /**
             Removed all existing listeners from receiving button events.

             @since v2.0
          */
          public void RemoveButtonListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing removeButtonListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveButtonListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'removeButtonListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'removeButtonListeners'.");
                    }
                    }
                    
               }

          /**
             Remove a listener to stop receiving device orientation change events.

             @param listener Listener to remove from receiving orientation change events.
             @since v2.0.5
          */
          public void RemoveDeviceOrientationListener(IDeviceOrientationListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing removeDeviceOrientationListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveDeviceOrientationListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'removeDeviceOrientationListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'removeDeviceOrientationListener'.");
                    }
                    }
                    
               }

          /**
             Remove all listeners receiving device orientation events.

             @since v2.0.5
          */
          public void RemoveDeviceOrientationListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executing removeDeviceOrientationListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveDeviceOrientationListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DeviceBridge executed 'removeDeviceOrientationListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DeviceBridge no delegate for 'removeDeviceOrientationListeners'.");
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
                    case "addButtonListener":
                         IButtonListener listener0 = new ButtonListenerImpl(request.GetAsyncId());
                         this.AddButtonListener(listener0);
                         break;
                    case "addDeviceOrientationListener":
                         IDeviceOrientationListener listener1 = new DeviceOrientationListenerImpl(request.GetAsyncId());
                         this.AddDeviceOrientationListener(listener1);
                         break;
                    case "getDeviceInfo":
                         DeviceInfo response2 = this.GetDeviceInfo();
                         if (response2 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response2);
                         }
                         break;
                    case "getLocaleCurrent":
                         Locale response3 = this.GetLocaleCurrent();
                         if (response3 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response3);
                         }
                         break;
                    case "getOrientationCurrent":
                         ICapabilitiesOrientation response4 = this.GetOrientationCurrent();
                         responseJSON = GetJSONProcessor().SerializeObject(response4);
                         break;
                    case "removeButtonListener":
                         IButtonListener listener5 = new ButtonListenerImpl(request.GetAsyncId());
                         this.RemoveButtonListener(listener5);
                         break;
                    case "removeButtonListeners":
                         this.RemoveButtonListeners();
                         break;
                    case "removeDeviceOrientationListener":
                         IDeviceOrientationListener listener7 = new DeviceOrientationListenerImpl(request.GetAsyncId());
                         this.RemoveDeviceOrientationListener(listener7);
                         break;
                    case "removeDeviceOrientationListeners":
                         this.RemoveDeviceOrientationListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "DeviceBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.4.";
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
