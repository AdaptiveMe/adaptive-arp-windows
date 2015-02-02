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
        Interface for testing the Capabilities operations
        Auto-generated implementation of ICapabilities specification.
     */
public class CapabilitiesBridge : BaseSystemBridge, ICapabilities, APIBridge
{

          /**
             API Delegate.
          */
          private ICapabilities _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public CapabilitiesBridge(ICapabilities _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return ICapabilities delegate that manages platform specific functions..
          */
          public ICapabilities GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(ICapabilities _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Obtains the default orientation of the device/display. If no default orientation is available on
the platform, this method will return the current orientation. To capture device or display orientation
changes please use the IDevice and IDisplay functions and listeners API respectively.

             @return The default orientation for the device/display.
             @since v2.0.5
          */
          public ICapabilitiesOrientation GetOrientationDefault()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing getOrientationDefault.");
               }

               ICapabilitiesOrientation result = ICapabilitiesOrientation.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetOrientationDefault();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'getOrientationDefault' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'getOrientationDefault'.");
                    }
                    }
                    return result;                    
               }

          /**
             Provides the device/display orientations supported by the platform. A platform will usually
support at least one orientation. This is usually PortaitUp.

             @return The orientations supported by the device/display of the platform.
             @since v2.0.5
          */
          public ICapabilitiesOrientation[] GetOrientationsSupported()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing getOrientationsSupported.");
               }

               ICapabilitiesOrientation[] result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetOrientationsSupported();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'getOrientationsSupported' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'getOrientationsSupported'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific hardware button is supported for interaction.

             @param type Type of feature to check.
             @return true is supported, false otherwise.
             @since v2.0
          */
          public bool HasButtonSupport(ICapabilitiesButton type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasButtonSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasButtonSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasButtonSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasButtonSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Communication capability is supported by
the device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasCommunicationSupport(ICapabilitiesCommunication type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasCommunicationSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasCommunicationSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasCommunicationSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasCommunicationSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Data capability is supported by the device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasDataSupport(ICapabilitiesData type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasDataSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasDataSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasDataSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasDataSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Media capability is supported by the
device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasMediaSupport(ICapabilitiesMedia type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasMediaSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasMediaSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasMediaSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasMediaSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Net capability is supported by the device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasNetSupport(ICapabilitiesNet type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasNetSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasNetSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasNetSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasNetSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Notification capability is supported by the
device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasNotificationSupport(ICapabilitiesNotification type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasNotificationSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasNotificationSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasNotificationSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasNotificationSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether the device/display supports a given orientation.

             @param orientation Orientation type.
             @return True if the given orientation is supported, false otherwise.
             @since v2.0.5
          */
          public bool HasOrientationSupport(ICapabilitiesOrientation orientation)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasOrientationSupport({"+orientation+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasOrientationSupport(orientation);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasOrientationSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasOrientationSupport'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determines whether a specific Sensor capability is supported by the
device.

             @param type Type of feature to check.
             @return true if supported, false otherwise.
             @since v2.0
          */
          public bool HasSensorSupport(ICapabilitiesSensor type)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executing hasSensorSupport({"+type+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.HasSensorSupport(type);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"CapabilitiesBridge executed 'hasSensorSupport' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"CapabilitiesBridge no delegate for 'hasSensorSupport'.");
                    }
                    }
                    return result;                    
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
                    case "getOrientationDefault":
                         ICapabilitiesOrientation response0 = this.GetOrientationDefault();
                         responseJSON = GetJSONProcessor().SerializeObject(response0);
                         break;
                    case "getOrientationsSupported":
                         ICapabilitiesOrientation[] response1 = this.GetOrientationsSupported();
                         if (response1 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response1);
                         }
                         break;
                    case "hasButtonSupport":
                         ICapabilitiesButton type2 = GetJSONProcessor().DeserializeObject<ICapabilitiesButton>(request.GetParameters()[0]);
                         bool response2 = this.HasButtonSupport(type2);
                         responseJSON = GetJSONProcessor().SerializeObject(response2);
                         break;
                    case "hasCommunicationSupport":
                         ICapabilitiesCommunication type3 = GetJSONProcessor().DeserializeObject<ICapabilitiesCommunication>(request.GetParameters()[0]);
                         bool response3 = this.HasCommunicationSupport(type3);
                         responseJSON = GetJSONProcessor().SerializeObject(response3);
                         break;
                    case "hasDataSupport":
                         ICapabilitiesData type4 = GetJSONProcessor().DeserializeObject<ICapabilitiesData>(request.GetParameters()[0]);
                         bool response4 = this.HasDataSupport(type4);
                         responseJSON = GetJSONProcessor().SerializeObject(response4);
                         break;
                    case "hasMediaSupport":
                         ICapabilitiesMedia type5 = GetJSONProcessor().DeserializeObject<ICapabilitiesMedia>(request.GetParameters()[0]);
                         bool response5 = this.HasMediaSupport(type5);
                         responseJSON = GetJSONProcessor().SerializeObject(response5);
                         break;
                    case "hasNetSupport":
                         ICapabilitiesNet type6 = GetJSONProcessor().DeserializeObject<ICapabilitiesNet>(request.GetParameters()[0]);
                         bool response6 = this.HasNetSupport(type6);
                         responseJSON = GetJSONProcessor().SerializeObject(response6);
                         break;
                    case "hasNotificationSupport":
                         ICapabilitiesNotification type7 = GetJSONProcessor().DeserializeObject<ICapabilitiesNotification>(request.GetParameters()[0]);
                         bool response7 = this.HasNotificationSupport(type7);
                         responseJSON = GetJSONProcessor().SerializeObject(response7);
                         break;
                    case "hasOrientationSupport":
                         ICapabilitiesOrientation orientation8 = GetJSONProcessor().DeserializeObject<ICapabilitiesOrientation>(request.GetParameters()[0]);
                         bool response8 = this.HasOrientationSupport(orientation8);
                         responseJSON = GetJSONProcessor().SerializeObject(response8);
                         break;
                    case "hasSensorSupport":
                         ICapabilitiesSensor type9 = GetJSONProcessor().DeserializeObject<ICapabilitiesSensor>(request.GetParameters()[0]);
                         bool response9 = this.HasSensorSupport(type9);
                         responseJSON = GetJSONProcessor().SerializeObject(response9);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "CapabilitiesBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.2.";
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
