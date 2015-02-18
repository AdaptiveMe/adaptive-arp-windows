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

    * @version v2.1.9

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{

     /**
        Interface to retrieve auto-registered service implementation references.

        @author Carlos Lozano Diez
        @since v2.0
        @version 1.0
     */
     public interface IAppRegistry
     {

          /**
             Returns a reference to the registered AccelerationBridge.

             @return AccelerationBridge reference or null if a bridge of this type is not registered.
          */
          AccelerationBridge GetAccelerationBridge();

          /**
             Returns a reference to the registered AdsBridge.

             @return AdsBridge reference or null if a bridge of this type is not registered.
          */
          AdsBridge GetAdsBridge();

          /**
             Returns a reference to the registered AlarmBridge.

             @return AlarmBridge reference or null if a bridge of this type is not registered.
          */
          AlarmBridge GetAlarmBridge();

          /**
             Returns a reference to the registered AmbientLightBridge.

             @return AmbientLightBridge reference or null if a bridge of this type is not registered.
          */
          AmbientLightBridge GetAmbientLightBridge();

          /**
             Returns a reference to the registered AnalyticsBridge.

             @return AnalyticsBridge reference or null if a bridge of this type is not registered.
          */
          AnalyticsBridge GetAnalyticsBridge();

          /**
             Returns a reference to the registered AudioBridge.

             @return AudioBridge reference or null if a bridge of this type is not registered.
          */
          AudioBridge GetAudioBridge();

          /**
             Returns a reference to the registered BarcodeBridge.

             @return BarcodeBridge reference or null if a bridge of this type is not registered.
          */
          BarcodeBridge GetBarcodeBridge();

          /**
             Returns a reference to the registered BarometerBridge.

             @return BarometerBridge reference or null if a bridge of this type is not registered.
          */
          BarometerBridge GetBarometerBridge();

          /**
             Returns a reference to the registered BluetoothBridge.

             @return BluetoothBridge reference or null if a bridge of this type is not registered.
          */
          BluetoothBridge GetBluetoothBridge();

          /**
             Returns a reference to the registered BrowserBridge.

             @return BrowserBridge reference or null if a bridge of this type is not registered.
          */
          BrowserBridge GetBrowserBridge();

          /**
             Returns a reference to the registered CalendarBridge.

             @return CalendarBridge reference or null if a bridge of this type is not registered.
          */
          CalendarBridge GetCalendarBridge();

          /**
             Returns a reference to the registered CameraBridge.

             @return CameraBridge reference or null if a bridge of this type is not registered.
          */
          CameraBridge GetCameraBridge();

          /**
             Returns a reference to the registered CapabilitiesBridge.

             @return CapabilitiesBridge reference or null if a bridge of this type is not registered.
          */
          CapabilitiesBridge GetCapabilitiesBridge();

          /**
             Returns a reference to the registered CloudBridge.

             @return CloudBridge reference or null if a bridge of this type is not registered.
          */
          CloudBridge GetCloudBridge();

          /**
             Returns a reference to the registered CompressionBridge.

             @return CompressionBridge reference or null if a bridge of this type is not registered.
          */
          CompressionBridge GetCompressionBridge();

          /**
             Returns a reference to the registered ConcurrentBridge.

             @return ConcurrentBridge reference or null if a bridge of this type is not registered.
          */
          ConcurrentBridge GetConcurrentBridge();

          /**
             Returns a reference to the registered ContactBridge.

             @return ContactBridge reference or null if a bridge of this type is not registered.
          */
          ContactBridge GetContactBridge();

          /**
             Returns a reference to the registered CryptoBridge.

             @return CryptoBridge reference or null if a bridge of this type is not registered.
          */
          CryptoBridge GetCryptoBridge();

          /**
             Returns a reference to the registered DataStreamBridge.

             @return DataStreamBridge reference or null if a bridge of this type is not registered.
          */
          DataStreamBridge GetDataStreamBridge();

          /**
             Returns a reference to the registered DatabaseBridge.

             @return DatabaseBridge reference or null if a bridge of this type is not registered.
          */
          DatabaseBridge GetDatabaseBridge();

          /**
             Returns a reference to the registered DesktopBridge.

             @return DesktopBridge reference or null if a bridge of this type is not registered.
          */
          DesktopBridge GetDesktopBridge();

          /**
             Returns a reference to the registered DeviceBridge.

             @return DeviceBridge reference or null if a bridge of this type is not registered.
          */
          DeviceBridge GetDeviceBridge();

          /**
             Returns a reference to the registered DisplayBridge.

             @return DisplayBridge reference or null if a bridge of this type is not registered.
          */
          DisplayBridge GetDisplayBridge();

          /**
             Returns a reference to the registered FacebookBridge.

             @return FacebookBridge reference or null if a bridge of this type is not registered.
          */
          FacebookBridge GetFacebookBridge();

          /**
             Returns a reference to the registered FileBridge.

             @return FileBridge reference or null if a bridge of this type is not registered.
          */
          FileBridge GetFileBridge();

          /**
             Returns a reference to the registered FileSystemBridge.

             @return FileSystemBridge reference or null if a bridge of this type is not registered.
          */
          FileSystemBridge GetFileSystemBridge();

          /**
             Returns a reference to the registered GeolocationBridge.

             @return GeolocationBridge reference or null if a bridge of this type is not registered.
          */
          GeolocationBridge GetGeolocationBridge();

          /**
             Returns a reference to the registered GlobalizationBridge.

             @return GlobalizationBridge reference or null if a bridge of this type is not registered.
          */
          GlobalizationBridge GetGlobalizationBridge();

          /**
             Returns a reference to the registered GooglePlusBridge.

             @return GooglePlusBridge reference or null if a bridge of this type is not registered.
          */
          GooglePlusBridge GetGooglePlusBridge();

          /**
             Returns a reference to the registered GyroscopeBridge.

             @return GyroscopeBridge reference or null if a bridge of this type is not registered.
          */
          GyroscopeBridge GetGyroscopeBridge();

          /**
             Returns a reference to the registered ImagingBridge.

             @return ImagingBridge reference or null if a bridge of this type is not registered.
          */
          ImagingBridge GetImagingBridge();

          /**
             Returns a reference to the registered InternalStorageBridge.

             @return InternalStorageBridge reference or null if a bridge of this type is not registered.
          */
          InternalStorageBridge GetInternalStorageBridge();

          /**
             Returns a reference to the registered LifecycleBridge.

             @return LifecycleBridge reference or null if a bridge of this type is not registered.
          */
          LifecycleBridge GetLifecycleBridge();

          /**
             Returns a reference to the registered LinkedInBridge.

             @return LinkedInBridge reference or null if a bridge of this type is not registered.
          */
          LinkedInBridge GetLinkedInBridge();

          /**
             Returns a reference to the registered LoggingBridge.

             @return LoggingBridge reference or null if a bridge of this type is not registered.
          */
          LoggingBridge GetLoggingBridge();

          /**
             Returns a reference to the registered MagnetometerBridge.

             @return MagnetometerBridge reference or null if a bridge of this type is not registered.
          */
          MagnetometerBridge GetMagnetometerBridge();

          /**
             Returns a reference to the registered MailBridge.

             @return MailBridge reference or null if a bridge of this type is not registered.
          */
          MailBridge GetMailBridge();

          /**
             Returns a reference to the registered ManagementBridge.

             @return ManagementBridge reference or null if a bridge of this type is not registered.
          */
          ManagementBridge GetManagementBridge();

          /**
             Returns a reference to the registered MapBridge.

             @return MapBridge reference or null if a bridge of this type is not registered.
          */
          MapBridge GetMapBridge();

          /**
             Returns a reference to the registered MessagingBridge.

             @return MessagingBridge reference or null if a bridge of this type is not registered.
          */
          MessagingBridge GetMessagingBridge();

          /**
             Returns a reference to the registered NFCBridge.

             @return NFCBridge reference or null if a bridge of this type is not registered.
          */
          NFCBridge GetNFCBridge();

          /**
             Returns a reference to the registered NetworkInfoBridge.

             @return NetworkInfoBridge reference or null if a bridge of this type is not registered.
          */
          NetworkInfoBridge GetNetworkInfoBridge();

          /**
             Returns a reference to the registered NetworkNamingBridge.

             @return NetworkNamingBridge reference or null if a bridge of this type is not registered.
          */
          NetworkNamingBridge GetNetworkNamingBridge();

          /**
             Returns a reference to the registered NetworkReachabilityBridge.

             @return NetworkReachabilityBridge reference or null if a bridge of this type is not registered.
          */
          NetworkReachabilityBridge GetNetworkReachabilityBridge();

          /**
             Returns a reference to the registered NetworkStatusBridge.

             @return NetworkStatusBridge reference or null if a bridge of this type is not registered.
          */
          NetworkStatusBridge GetNetworkStatusBridge();

          /**
             Returns a reference to the registered NotificationBridge.

             @return NotificationBridge reference or null if a bridge of this type is not registered.
          */
          NotificationBridge GetNotificationBridge();

          /**
             Returns a reference to the registered NotificationLocalBridge.

             @return NotificationLocalBridge reference or null if a bridge of this type is not registered.
          */
          NotificationLocalBridge GetNotificationLocalBridge();

          /**
             Returns a reference to the registered OAuthBridge.

             @return OAuthBridge reference or null if a bridge of this type is not registered.
          */
          OAuthBridge GetOAuthBridge();

          /**
             Returns a reference to the registered OCRBridge.

             @return OCRBridge reference or null if a bridge of this type is not registered.
          */
          OCRBridge GetOCRBridge();

          /**
             Returns a reference to the registered OSBridge.

             @return OSBridge reference or null if a bridge of this type is not registered.
          */
          OSBridge GetOSBridge();

          /**
             Returns a reference to the registered OpenIdBridge.

             @return OpenIdBridge reference or null if a bridge of this type is not registered.
          */
          OpenIdBridge GetOpenIdBridge();

          /**
             Returns a reference to the registered PrintingBridge.

             @return PrintingBridge reference or null if a bridge of this type is not registered.
          */
          PrintingBridge GetPrintingBridge();

          /**
             Returns a reference to the registered ProximityBridge.

             @return ProximityBridge reference or null if a bridge of this type is not registered.
          */
          ProximityBridge GetProximityBridge();

          /**
             Returns a reference to the registered QRCodeBridge.

             @return QRCodeBridge reference or null if a bridge of this type is not registered.
          */
          QRCodeBridge GetQRCodeBridge();

          /**
             Returns a reference to the registered RSSBridge.

             @return RSSBridge reference or null if a bridge of this type is not registered.
          */
          RSSBridge GetRSSBridge();

          /**
             Returns a reference to the registered RuntimeBridge.

             @return RuntimeBridge reference or null if a bridge of this type is not registered.
          */
          RuntimeBridge GetRuntimeBridge();

          /**
             Returns a reference to the registered SecurityBridge.

             @return SecurityBridge reference or null if a bridge of this type is not registered.
          */
          SecurityBridge GetSecurityBridge();

          /**
             Returns a reference to the registered ServiceBridge.

             @return ServiceBridge reference or null if a bridge of this type is not registered.
          */
          ServiceBridge GetServiceBridge();

          /**
             Returns a reference to the registered SettingsBridge.

             @return SettingsBridge reference or null if a bridge of this type is not registered.
          */
          SettingsBridge GetSettingsBridge();

          /**
             Returns a reference to the registered SocketBridge.

             @return SocketBridge reference or null if a bridge of this type is not registered.
          */
          SocketBridge GetSocketBridge();

          /**
             Returns a reference to the registered StoreBridge.

             @return StoreBridge reference or null if a bridge of this type is not registered.
          */
          StoreBridge GetStoreBridge();

          /**
             Returns a reference to the registered TelephonyBridge.

             @return TelephonyBridge reference or null if a bridge of this type is not registered.
          */
          TelephonyBridge GetTelephonyBridge();

          /**
             Returns a reference to the registered TimerBridge.

             @return TimerBridge reference or null if a bridge of this type is not registered.
          */
          TimerBridge GetTimerBridge();

          /**
             Returns a reference to the registered TwitterBridge.

             @return TwitterBridge reference or null if a bridge of this type is not registered.
          */
          TwitterBridge GetTwitterBridge();

          /**
             Returns a reference to the registered UIBridge.

             @return UIBridge reference or null if a bridge of this type is not registered.
          */
          UIBridge GetUIBridge();

          /**
             Returns a reference to the registered UpdateBridge.

             @return UpdateBridge reference or null if a bridge of this type is not registered.
          */
          UpdateBridge GetUpdateBridge();

          /**
             Returns a reference to the registered VibrationBridge.

             @return VibrationBridge reference or null if a bridge of this type is not registered.
          */
          VibrationBridge GetVibrationBridge();

          /**
             Returns a reference to the registered VideoBridge.

             @return VideoBridge reference or null if a bridge of this type is not registered.
          */
          VideoBridge GetVideoBridge();

          /**
             Returns a reference to the registered WalletBridge.

             @return WalletBridge reference or null if a bridge of this type is not registered.
          */
          WalletBridge GetWalletBridge();

          /**
             Returns a reference to the registered XMLBridge.

             @return XMLBridge reference or null if a bridge of this type is not registered.
          */
          XMLBridge GetXMLBridge();

          /**
             Provides a bridge to handle API calls for the given interface name.

             @param bridgeType String with the interface name required.
             @return APIBridge That handles calls for the specified interface or null if the given bridge is not registered.
          */
          APIBridge GetBridge(String bridgeType);

          /**
             Method that returns the API version of the implementation.

             @return API Version string.
             @since v2.0
          */
          string GetAPIVersion();

          /**
             Returns a reference to the Platform Context

             @return Reference to the platform context
             @since v2.0
          */
          AppContextBridge GetPlatformContext();

          /**
             Returns a reference to the Webview platform context

             @return Reference to the Webview Context
             @since v2.0
          */
          AppContextWebviewBridge GetPlatformContextWeb();

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
