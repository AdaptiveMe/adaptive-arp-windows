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
        Interface to retrieve auto-registered service implementation references.
        Auto-generated implementation of IAppRegistry specification.
     */
public class AppRegistryBridge : IAppRegistry
{

          /**
             Group of API.
          */
          private IAdaptiveRPGroup apiGroup = IAdaptiveRPGroup.Kernel;

          /**
             API Delegate.
          */
          private IAppRegistry _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public AppRegistryBridge(IAppRegistry _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IAppRegistry delegate that manages platform specific functions..
          */
          public IAppRegistry GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IAppRegistry _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Common JSONProcessor.
          */
          private static JSONProcessor singletonJSONProcessor;

          /**
             Singleton instance.
          */
          private static AppRegistryBridge singleton;

          /**
             Get singleton instance.
             @return AppRegistryBridge singleton instance.
          */
          public static AppRegistryBridge GetInstance()
          {
               if (singleton == null)
               {
                    singleton = new AppRegistryBridge(new AppRegistryDelegate());
               }
               return singleton;
          }

          /**
             Get singleton JSONProcessor instance.
             @return JSONProcessor singleton instance.
          */
          public static JSONProcessor GetJSONProcessor()
          {
               if (singletonJSONProcessor == null)
               {
                    singletonJSONProcessor = JSONProcessor.GetInstance();
               }
               return singletonJSONProcessor;
          }

          /**
             Returns a reference to the registered AccelerationBridge.

             @return AccelerationBridge reference or null if a bridge of this type is not registered.
          */
          public AccelerationBridge GetAccelerationBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AccelerationBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAccelerationBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAccelerationBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAccelerationBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAccelerationBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered AdsBridge.

             @return AdsBridge reference or null if a bridge of this type is not registered.
          */
          public AdsBridge GetAdsBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AdsBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAdsBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAdsBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAdsBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAdsBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered AlarmBridge.

             @return AlarmBridge reference or null if a bridge of this type is not registered.
          */
          public AlarmBridge GetAlarmBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AlarmBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAlarmBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAlarmBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAlarmBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAlarmBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered AmbientLightBridge.

             @return AmbientLightBridge reference or null if a bridge of this type is not registered.
          */
          public AmbientLightBridge GetAmbientLightBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AmbientLightBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAmbientLightBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAmbientLightBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAmbientLightBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAmbientLightBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered AnalyticsBridge.

             @return AnalyticsBridge reference or null if a bridge of this type is not registered.
          */
          public AnalyticsBridge GetAnalyticsBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AnalyticsBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAnalyticsBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAnalyticsBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAnalyticsBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAnalyticsBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered AudioBridge.

             @return AudioBridge reference or null if a bridge of this type is not registered.
          */
          public AudioBridge GetAudioBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               AudioBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetAudioBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetAudioBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetAudioBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetAudioBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered BarcodeBridge.

             @return BarcodeBridge reference or null if a bridge of this type is not registered.
          */
          public BarcodeBridge GetBarcodeBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               BarcodeBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetBarcodeBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetBarcodeBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetBarcodeBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetBarcodeBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered BarometerBridge.

             @return BarometerBridge reference or null if a bridge of this type is not registered.
          */
          public BarometerBridge GetBarometerBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               BarometerBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetBarometerBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetBarometerBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetBarometerBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetBarometerBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered BluetoothBridge.

             @return BluetoothBridge reference or null if a bridge of this type is not registered.
          */
          public BluetoothBridge GetBluetoothBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               BluetoothBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetBluetoothBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetBluetoothBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetBluetoothBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetBluetoothBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered BrowserBridge.

             @return BrowserBridge reference or null if a bridge of this type is not registered.
          */
          public BrowserBridge GetBrowserBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               BrowserBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetBrowserBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetBrowserBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetBrowserBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetBrowserBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CalendarBridge.

             @return CalendarBridge reference or null if a bridge of this type is not registered.
          */
          public CalendarBridge GetCalendarBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CalendarBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCalendarBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCalendarBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCalendarBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCalendarBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CameraBridge.

             @return CameraBridge reference or null if a bridge of this type is not registered.
          */
          public CameraBridge GetCameraBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CameraBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCameraBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCameraBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCameraBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCameraBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CapabilitiesBridge.

             @return CapabilitiesBridge reference or null if a bridge of this type is not registered.
          */
          public CapabilitiesBridge GetCapabilitiesBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CapabilitiesBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCapabilitiesBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCapabilitiesBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCapabilitiesBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCapabilitiesBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CloudBridge.

             @return CloudBridge reference or null if a bridge of this type is not registered.
          */
          public CloudBridge GetCloudBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CloudBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCloudBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCloudBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCloudBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCloudBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CompressionBridge.

             @return CompressionBridge reference or null if a bridge of this type is not registered.
          */
          public CompressionBridge GetCompressionBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CompressionBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCompressionBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCompressionBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCompressionBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCompressionBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ConcurrentBridge.

             @return ConcurrentBridge reference or null if a bridge of this type is not registered.
          */
          public ConcurrentBridge GetConcurrentBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ConcurrentBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetConcurrentBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetConcurrentBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetConcurrentBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetConcurrentBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ContactBridge.

             @return ContactBridge reference or null if a bridge of this type is not registered.
          */
          public ContactBridge GetContactBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ContactBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetContactBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetContactBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetContactBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetContactBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered CryptoBridge.

             @return CryptoBridge reference or null if a bridge of this type is not registered.
          */
          public CryptoBridge GetCryptoBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               CryptoBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetCryptoBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetCryptoBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetCryptoBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetCryptoBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered DataStreamBridge.

             @return DataStreamBridge reference or null if a bridge of this type is not registered.
          */
          public DataStreamBridge GetDataStreamBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               DataStreamBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetDataStreamBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetDataStreamBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetDataStreamBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetDataStreamBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered DatabaseBridge.

             @return DatabaseBridge reference or null if a bridge of this type is not registered.
          */
          public DatabaseBridge GetDatabaseBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               DatabaseBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetDatabaseBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetDatabaseBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetDatabaseBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetDatabaseBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered DesktopBridge.

             @return DesktopBridge reference or null if a bridge of this type is not registered.
          */
          public DesktopBridge GetDesktopBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               DesktopBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetDesktopBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetDesktopBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetDesktopBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetDesktopBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered DeviceBridge.

             @return DeviceBridge reference or null if a bridge of this type is not registered.
          */
          public DeviceBridge GetDeviceBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               DeviceBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetDeviceBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetDeviceBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetDeviceBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetDeviceBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered DisplayBridge.

             @return DisplayBridge reference or null if a bridge of this type is not registered.
          */
          public DisplayBridge GetDisplayBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               DisplayBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetDisplayBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetDisplayBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetDisplayBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetDisplayBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered FacebookBridge.

             @return FacebookBridge reference or null if a bridge of this type is not registered.
          */
          public FacebookBridge GetFacebookBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               FacebookBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetFacebookBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetFacebookBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetFacebookBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetFacebookBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered FileBridge.

             @return FileBridge reference or null if a bridge of this type is not registered.
          */
          public FileBridge GetFileBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               FileBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetFileBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetFileBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetFileBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetFileBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered FileSystemBridge.

             @return FileSystemBridge reference or null if a bridge of this type is not registered.
          */
          public FileSystemBridge GetFileSystemBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               FileSystemBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetFileSystemBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetFileSystemBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetFileSystemBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetFileSystemBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered GeolocationBridge.

             @return GeolocationBridge reference or null if a bridge of this type is not registered.
          */
          public GeolocationBridge GetGeolocationBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               GeolocationBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetGeolocationBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetGeolocationBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetGeolocationBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetGeolocationBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered GlobalizationBridge.

             @return GlobalizationBridge reference or null if a bridge of this type is not registered.
          */
          public GlobalizationBridge GetGlobalizationBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               GlobalizationBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetGlobalizationBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetGlobalizationBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetGlobalizationBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetGlobalizationBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered GooglePlusBridge.

             @return GooglePlusBridge reference or null if a bridge of this type is not registered.
          */
          public GooglePlusBridge GetGooglePlusBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               GooglePlusBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetGooglePlusBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetGooglePlusBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetGooglePlusBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetGooglePlusBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered GyroscopeBridge.

             @return GyroscopeBridge reference or null if a bridge of this type is not registered.
          */
          public GyroscopeBridge GetGyroscopeBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               GyroscopeBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetGyroscopeBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetGyroscopeBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetGyroscopeBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetGyroscopeBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ImagingBridge.

             @return ImagingBridge reference or null if a bridge of this type is not registered.
          */
          public ImagingBridge GetImagingBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ImagingBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetImagingBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetImagingBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetImagingBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetImagingBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered InternalStorageBridge.

             @return InternalStorageBridge reference or null if a bridge of this type is not registered.
          */
          public InternalStorageBridge GetInternalStorageBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               InternalStorageBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetInternalStorageBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetInternalStorageBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetInternalStorageBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetInternalStorageBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered LifecycleBridge.

             @return LifecycleBridge reference or null if a bridge of this type is not registered.
          */
          public LifecycleBridge GetLifecycleBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               LifecycleBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetLifecycleBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetLifecycleBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetLifecycleBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetLifecycleBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered LinkedInBridge.

             @return LinkedInBridge reference or null if a bridge of this type is not registered.
          */
          public LinkedInBridge GetLinkedInBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               LinkedInBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetLinkedInBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetLinkedInBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetLinkedInBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetLinkedInBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered LoggingBridge.

             @return LoggingBridge reference or null if a bridge of this type is not registered.
          */
          public LoggingBridge GetLoggingBridge()
          {
               LoggingBridge result = null;

               if (this._delegate != null)
               {
                    result = this._delegate.GetLoggingBridge();
               }
               return result;
          }

          /**
             Returns a reference to the registered MagnetometerBridge.

             @return MagnetometerBridge reference or null if a bridge of this type is not registered.
          */
          public MagnetometerBridge GetMagnetometerBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               MagnetometerBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetMagnetometerBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetMagnetometerBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetMagnetometerBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetMagnetometerBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered MailBridge.

             @return MailBridge reference or null if a bridge of this type is not registered.
          */
          public MailBridge GetMailBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               MailBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetMailBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetMailBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetMailBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetMailBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ManagementBridge.

             @return ManagementBridge reference or null if a bridge of this type is not registered.
          */
          public ManagementBridge GetManagementBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ManagementBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetManagementBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetManagementBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetManagementBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetManagementBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered MapBridge.

             @return MapBridge reference or null if a bridge of this type is not registered.
          */
          public MapBridge GetMapBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               MapBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetMapBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetMapBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetMapBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetMapBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered MessagingBridge.

             @return MessagingBridge reference or null if a bridge of this type is not registered.
          */
          public MessagingBridge GetMessagingBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               MessagingBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetMessagingBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetMessagingBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetMessagingBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetMessagingBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NFCBridge.

             @return NFCBridge reference or null if a bridge of this type is not registered.
          */
          public NFCBridge GetNFCBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NFCBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNFCBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNFCBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNFCBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNFCBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NetworkInfoBridge.

             @return NetworkInfoBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkInfoBridge GetNetworkInfoBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NetworkInfoBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNetworkInfoBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNetworkInfoBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNetworkInfoBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNetworkInfoBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NetworkNamingBridge.

             @return NetworkNamingBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkNamingBridge GetNetworkNamingBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NetworkNamingBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNetworkNamingBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNetworkNamingBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNetworkNamingBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNetworkNamingBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NetworkReachabilityBridge.

             @return NetworkReachabilityBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkReachabilityBridge GetNetworkReachabilityBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NetworkReachabilityBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNetworkReachabilityBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNetworkReachabilityBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNetworkReachabilityBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNetworkReachabilityBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NetworkStatusBridge.

             @return NetworkStatusBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkStatusBridge GetNetworkStatusBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NetworkStatusBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNetworkStatusBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNetworkStatusBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNetworkStatusBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNetworkStatusBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NotificationBridge.

             @return NotificationBridge reference or null if a bridge of this type is not registered.
          */
          public NotificationBridge GetNotificationBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NotificationBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNotificationBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNotificationBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNotificationBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNotificationBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered NotificationLocalBridge.

             @return NotificationLocalBridge reference or null if a bridge of this type is not registered.
          */
          public NotificationLocalBridge GetNotificationLocalBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               NotificationLocalBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetNotificationLocalBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetNotificationLocalBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetNotificationLocalBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetNotificationLocalBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered OAuthBridge.

             @return OAuthBridge reference or null if a bridge of this type is not registered.
          */
          public OAuthBridge GetOAuthBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               OAuthBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetOAuthBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetOAuthBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetOAuthBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetOAuthBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered OCRBridge.

             @return OCRBridge reference or null if a bridge of this type is not registered.
          */
          public OCRBridge GetOCRBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               OCRBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetOCRBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetOCRBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetOCRBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetOCRBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered OSBridge.

             @return OSBridge reference or null if a bridge of this type is not registered.
          */
          public OSBridge GetOSBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               OSBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetOSBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetOSBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetOSBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetOSBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered OpenIdBridge.

             @return OpenIdBridge reference or null if a bridge of this type is not registered.
          */
          public OpenIdBridge GetOpenIdBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               OpenIdBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetOpenIdBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetOpenIdBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetOpenIdBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetOpenIdBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered PrintingBridge.

             @return PrintingBridge reference or null if a bridge of this type is not registered.
          */
          public PrintingBridge GetPrintingBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               PrintingBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetPrintingBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetPrintingBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetPrintingBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetPrintingBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ProximityBridge.

             @return ProximityBridge reference or null if a bridge of this type is not registered.
          */
          public ProximityBridge GetProximityBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ProximityBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetProximityBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetProximityBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetProximityBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetProximityBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered QRCodeBridge.

             @return QRCodeBridge reference or null if a bridge of this type is not registered.
          */
          public QRCodeBridge GetQRCodeBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               QRCodeBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetQRCodeBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetQRCodeBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetQRCodeBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetQRCodeBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered RSSBridge.

             @return RSSBridge reference or null if a bridge of this type is not registered.
          */
          public RSSBridge GetRSSBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               RSSBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetRSSBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetRSSBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetRSSBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetRSSBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered RuntimeBridge.

             @return RuntimeBridge reference or null if a bridge of this type is not registered.
          */
          public RuntimeBridge GetRuntimeBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               RuntimeBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetRuntimeBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetRuntimeBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetRuntimeBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetRuntimeBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered SecurityBridge.

             @return SecurityBridge reference or null if a bridge of this type is not registered.
          */
          public SecurityBridge GetSecurityBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               SecurityBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetSecurityBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetSecurityBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetSecurityBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetSecurityBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered ServiceBridge.

             @return ServiceBridge reference or null if a bridge of this type is not registered.
          */
          public ServiceBridge GetServiceBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               ServiceBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetServiceBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetServiceBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetServiceBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetServiceBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered SettingsBridge.

             @return SettingsBridge reference or null if a bridge of this type is not registered.
          */
          public SettingsBridge GetSettingsBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               SettingsBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetSettingsBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetSettingsBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetSettingsBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetSettingsBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered SocketBridge.

             @return SocketBridge reference or null if a bridge of this type is not registered.
          */
          public SocketBridge GetSocketBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               SocketBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetSocketBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetSocketBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetSocketBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetSocketBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered StoreBridge.

             @return StoreBridge reference or null if a bridge of this type is not registered.
          */
          public StoreBridge GetStoreBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               StoreBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetStoreBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetStoreBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetStoreBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetStoreBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered TelephonyBridge.

             @return TelephonyBridge reference or null if a bridge of this type is not registered.
          */
          public TelephonyBridge GetTelephonyBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               TelephonyBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetTelephonyBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetTelephonyBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetTelephonyBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetTelephonyBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered TimerBridge.

             @return TimerBridge reference or null if a bridge of this type is not registered.
          */
          public TimerBridge GetTimerBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               TimerBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetTimerBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetTimerBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetTimerBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetTimerBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered TwitterBridge.

             @return TwitterBridge reference or null if a bridge of this type is not registered.
          */
          public TwitterBridge GetTwitterBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               TwitterBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetTwitterBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetTwitterBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetTwitterBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetTwitterBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered UIBridge.

             @return UIBridge reference or null if a bridge of this type is not registered.
          */
          public UIBridge GetUIBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               UIBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetUIBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetUIBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetUIBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetUIBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered UpdateBridge.

             @return UpdateBridge reference or null if a bridge of this type is not registered.
          */
          public UpdateBridge GetUpdateBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               UpdateBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetUpdateBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetUpdateBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetUpdateBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetUpdateBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered VibrationBridge.

             @return VibrationBridge reference or null if a bridge of this type is not registered.
          */
          public VibrationBridge GetVibrationBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               VibrationBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetVibrationBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetVibrationBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetVibrationBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetVibrationBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered VideoBridge.

             @return VideoBridge reference or null if a bridge of this type is not registered.
          */
          public VideoBridge GetVideoBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               VideoBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetVideoBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetVideoBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetVideoBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetVideoBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered WalletBridge.

             @return WalletBridge reference or null if a bridge of this type is not registered.
          */
          public WalletBridge GetWalletBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               WalletBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetWalletBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetWalletBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetWalletBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetWalletBridge'.");
                    }
               }
               return result;
          }

          /**
             Returns a reference to the registered XMLBridge.

             @return XMLBridge reference or null if a bridge of this type is not registered.
          */
          public XMLBridge GetXMLBridge()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               XMLBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executing GetXMLBridge().");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetXMLBridge();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(), "AppRegistryBridge executed 'GetXMLBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               }
               else
               {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(), "AppRegistryBridge no delegate for 'GetXMLBridge'.");
                    }
               }
               return result;
          }

          /**
             Provides a bridge to handle API calls for the given interface name.

             @param bridgeType String with the interface name required.
             @return APIBridge That handles calls for the specified interface or null if the given bridge is not registered.
          */
          public APIBridge GetBridge(String bridgeType) {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();
               APIBridge result = null;

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executing getBridge("+bridgeType+").");
               }

               if (this._delegate != null)
               {
                    result = this._delegate.GetBridge(bridgeType);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executed 'getBridge' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AppRegistryBridge no delegate for 'getBridge'.");
                    }
               }
               return result;
          }

          /**
             Method that returns the API version of the implementation.

             @return API Version string.
             @since v2.0
          */
          public string GetAPIVersion()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executing getAPIVersion.");
               }

               string result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetAPIVersion();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executed 'getAPIVersion' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AppRegistryBridge no delegate for 'getAPIVersion'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the Platform Context

             @return Reference to the platform context
             @since v2.0
          */
          public AppContextBridge GetPlatformContext()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executing getPlatformContext.");
               }

               AppContextBridge result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetPlatformContext();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executed 'getPlatformContext' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AppRegistryBridge no delegate for 'getPlatformContext'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the Webview platform context

             @return Reference to the Webview Context
             @since v2.0
          */
          public AppContextWebviewBridge GetPlatformContextWeb()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executing getPlatformContextWeb.");
               }

               AppContextWebviewBridge result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetPlatformContextWeb();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"AppRegistryBridge executed 'getPlatformContextWeb' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"AppRegistryBridge no delegate for 'getPlatformContextWeb'.");
                    }
                    }
                    return result;                    
               }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
