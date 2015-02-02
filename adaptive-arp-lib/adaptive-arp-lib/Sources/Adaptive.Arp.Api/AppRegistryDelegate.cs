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
        Interface to retrieve auto-registered service implementation references.
        Auto-generated implementation of IAppRegistry specification.
     */
     public class AppRegistryDelegate : IAppRegistry
     {

          /**
             Default Constructor.
          */
          public AppRegistryDelegate() : base()
          {
          }

          /**
             Bridge references.
          */
          private AccelerationBridge __accelerationBridge = null;
          private AdsBridge __adsBridge = null;
          private AlarmBridge __alarmBridge = null;
          private AmbientLightBridge __ambientlightBridge = null;
          private AnalyticsBridge __analyticsBridge = null;
          private AudioBridge __audioBridge = null;
          private BarcodeBridge __barcodeBridge = null;
          private BarometerBridge __barometerBridge = null;
          private BluetoothBridge __bluetoothBridge = null;
          private BrowserBridge __browserBridge = null;
          private CalendarBridge __calendarBridge = null;
          private CameraBridge __cameraBridge = null;
          private CapabilitiesBridge __capabilitiesBridge = null;
          private CloudBridge __cloudBridge = null;
          private CompressionBridge __compressionBridge = null;
          private ConcurrentBridge __concurrentBridge = null;
          private ContactBridge __contactBridge = null;
          private CryptoBridge __cryptoBridge = null;
          private DataStreamBridge __datastreamBridge = null;
          private DatabaseBridge __databaseBridge = null;
          private DesktopBridge __desktopBridge = null;
          private DeviceBridge __deviceBridge = null;
          private DisplayBridge __displayBridge = null;
          private FacebookBridge __facebookBridge = null;
          private FileBridge __fileBridge = null;
          private FileSystemBridge __filesystemBridge = null;
          private GeolocationBridge __geolocationBridge = null;
          private GlobalizationBridge __globalizationBridge = null;
          private GooglePlusBridge __googleplusBridge = null;
          private GyroscopeBridge __gyroscopeBridge = null;
          private ImagingBridge __imagingBridge = null;
          private InternalStorageBridge __internalstorageBridge = null;
          private LifecycleBridge __lifecycleBridge = null;
          private LinkedInBridge __linkedinBridge = null;
          private LoggingBridge __loggingBridge = null;
          private MagnetometerBridge __magnetometerBridge = null;
          private MailBridge __mailBridge = null;
          private ManagementBridge __managementBridge = null;
          private MapBridge __mapBridge = null;
          private MessagingBridge __messagingBridge = null;
          private NFCBridge __nfcBridge = null;
          private NetworkInfoBridge __networkinfoBridge = null;
          private NetworkNamingBridge __networknamingBridge = null;
          private NetworkReachabilityBridge __networkreachabilityBridge = null;
          private NetworkStatusBridge __networkstatusBridge = null;
          private NotificationBridge __notificationBridge = null;
          private NotificationLocalBridge __notificationlocalBridge = null;
          private OAuthBridge __oauthBridge = null;
          private OCRBridge __ocrBridge = null;
          private OSBridge __osBridge = null;
          private OpenIdBridge __openidBridge = null;
          private PrintingBridge __printingBridge = null;
          private ProximityBridge __proximityBridge = null;
          private QRCodeBridge __qrcodeBridge = null;
          private RSSBridge __rssBridge = null;
          private RuntimeBridge __runtimeBridge = null;
          private SecurityBridge __securityBridge = null;
          private ServiceBridge __serviceBridge = null;
          private SettingsBridge __settingsBridge = null;
          private SocketBridge __socketBridge = null;
          private StoreBridge __storeBridge = null;
          private TelephonyBridge __telephonyBridge = null;
          private TimerBridge __timerBridge = null;
          private TwitterBridge __twitterBridge = null;
          private UIBridge __uiBridge = null;
          private UpdateBridge __updateBridge = null;
          private VibrationBridge __vibrationBridge = null;
          private VideoBridge __videoBridge = null;
          private WalletBridge __walletBridge = null;
          private XMLBridge __xmlBridge = null;

          /**
             Returns a reference to the registered AccelerationBridge.

             @return AccelerationBridge reference or null if a bridge of this type is not registered.
          */
          public AccelerationBridge GetAccelerationBridge()
          {
               if(__accelerationBridge == null)
               {
                    __accelerationBridge = new AccelerationBridge(null);
               }
               return __accelerationBridge;
          }

          /**
             Returns a reference to the registered AdsBridge.

             @return AdsBridge reference or null if a bridge of this type is not registered.
          */
          public AdsBridge GetAdsBridge()
          {
               if(__adsBridge == null)
               {
                    __adsBridge = new AdsBridge(null);
               }
               return __adsBridge;
          }

          /**
             Returns a reference to the registered AlarmBridge.

             @return AlarmBridge reference or null if a bridge of this type is not registered.
          */
          public AlarmBridge GetAlarmBridge()
          {
               if(__alarmBridge == null)
               {
                    __alarmBridge = new AlarmBridge(null);
               }
               return __alarmBridge;
          }

          /**
             Returns a reference to the registered AmbientLightBridge.

             @return AmbientLightBridge reference or null if a bridge of this type is not registered.
          */
          public AmbientLightBridge GetAmbientLightBridge()
          {
               if(__ambientlightBridge == null)
               {
                    __ambientlightBridge = new AmbientLightBridge(null);
               }
               return __ambientlightBridge;
          }

          /**
             Returns a reference to the registered AnalyticsBridge.

             @return AnalyticsBridge reference or null if a bridge of this type is not registered.
          */
          public AnalyticsBridge GetAnalyticsBridge()
          {
               if(__analyticsBridge == null)
               {
                    __analyticsBridge = new AnalyticsBridge(null);
               }
               return __analyticsBridge;
          }

          /**
             Returns a reference to the registered AudioBridge.

             @return AudioBridge reference or null if a bridge of this type is not registered.
          */
          public AudioBridge GetAudioBridge()
          {
               if(__audioBridge == null)
               {
                    __audioBridge = new AudioBridge(null);
               }
               return __audioBridge;
          }

          /**
             Returns a reference to the registered BarcodeBridge.

             @return BarcodeBridge reference or null if a bridge of this type is not registered.
          */
          public BarcodeBridge GetBarcodeBridge()
          {
               if(__barcodeBridge == null)
               {
                    __barcodeBridge = new BarcodeBridge(null);
               }
               return __barcodeBridge;
          }

          /**
             Returns a reference to the registered BarometerBridge.

             @return BarometerBridge reference or null if a bridge of this type is not registered.
          */
          public BarometerBridge GetBarometerBridge()
          {
               if(__barometerBridge == null)
               {
                    __barometerBridge = new BarometerBridge(null);
               }
               return __barometerBridge;
          }

          /**
             Returns a reference to the registered BluetoothBridge.

             @return BluetoothBridge reference or null if a bridge of this type is not registered.
          */
          public BluetoothBridge GetBluetoothBridge()
          {
               if(__bluetoothBridge == null)
               {
                    __bluetoothBridge = new BluetoothBridge(null);
               }
               return __bluetoothBridge;
          }

          /**
             Returns a reference to the registered BrowserBridge.

             @return BrowserBridge reference or null if a bridge of this type is not registered.
          */
          public BrowserBridge GetBrowserBridge()
          {
               if(__browserBridge == null)
               {
                    __browserBridge = new BrowserBridge(null);
               }
               return __browserBridge;
          }

          /**
             Returns a reference to the registered CalendarBridge.

             @return CalendarBridge reference or null if a bridge of this type is not registered.
          */
          public CalendarBridge GetCalendarBridge()
          {
               if(__calendarBridge == null)
               {
                    __calendarBridge = new CalendarBridge(null);
               }
               return __calendarBridge;
          }

          /**
             Returns a reference to the registered CameraBridge.

             @return CameraBridge reference or null if a bridge of this type is not registered.
          */
          public CameraBridge GetCameraBridge()
          {
               if(__cameraBridge == null)
               {
                    __cameraBridge = new CameraBridge(null);
               }
               return __cameraBridge;
          }

          /**
             Returns a reference to the registered CapabilitiesBridge.

             @return CapabilitiesBridge reference or null if a bridge of this type is not registered.
          */
          public CapabilitiesBridge GetCapabilitiesBridge()
          {
               if(__capabilitiesBridge == null)
               {
                    __capabilitiesBridge = new CapabilitiesBridge(null);
               }
               return __capabilitiesBridge;
          }

          /**
             Returns a reference to the registered CloudBridge.

             @return CloudBridge reference or null if a bridge of this type is not registered.
          */
          public CloudBridge GetCloudBridge()
          {
               if(__cloudBridge == null)
               {
                    __cloudBridge = new CloudBridge(null);
               }
               return __cloudBridge;
          }

          /**
             Returns a reference to the registered CompressionBridge.

             @return CompressionBridge reference or null if a bridge of this type is not registered.
          */
          public CompressionBridge GetCompressionBridge()
          {
               if(__compressionBridge == null)
               {
                    __compressionBridge = new CompressionBridge(null);
               }
               return __compressionBridge;
          }

          /**
             Returns a reference to the registered ConcurrentBridge.

             @return ConcurrentBridge reference or null if a bridge of this type is not registered.
          */
          public ConcurrentBridge GetConcurrentBridge()
          {
               if(__concurrentBridge == null)
               {
                    __concurrentBridge = new ConcurrentBridge(null);
               }
               return __concurrentBridge;
          }

          /**
             Returns a reference to the registered ContactBridge.

             @return ContactBridge reference or null if a bridge of this type is not registered.
          */
          public ContactBridge GetContactBridge()
          {
               if(__contactBridge == null)
               {
                    __contactBridge = new ContactBridge(null);
               }
               return __contactBridge;
          }

          /**
             Returns a reference to the registered CryptoBridge.

             @return CryptoBridge reference or null if a bridge of this type is not registered.
          */
          public CryptoBridge GetCryptoBridge()
          {
               if(__cryptoBridge == null)
               {
                    __cryptoBridge = new CryptoBridge(null);
               }
               return __cryptoBridge;
          }

          /**
             Returns a reference to the registered DataStreamBridge.

             @return DataStreamBridge reference or null if a bridge of this type is not registered.
          */
          public DataStreamBridge GetDataStreamBridge()
          {
               if(__datastreamBridge == null)
               {
                    __datastreamBridge = new DataStreamBridge(null);
               }
               return __datastreamBridge;
          }

          /**
             Returns a reference to the registered DatabaseBridge.

             @return DatabaseBridge reference or null if a bridge of this type is not registered.
          */
          public DatabaseBridge GetDatabaseBridge()
          {
               if(__databaseBridge == null)
               {
                    __databaseBridge = new DatabaseBridge(null);
               }
               return __databaseBridge;
          }

          /**
             Returns a reference to the registered DesktopBridge.

             @return DesktopBridge reference or null if a bridge of this type is not registered.
          */
          public DesktopBridge GetDesktopBridge()
          {
               if(__desktopBridge == null)
               {
                    __desktopBridge = new DesktopBridge(null);
               }
               return __desktopBridge;
          }

          /**
             Returns a reference to the registered DeviceBridge.

             @return DeviceBridge reference or null if a bridge of this type is not registered.
          */
          public DeviceBridge GetDeviceBridge()
          {
               if(__deviceBridge == null)
               {
                    __deviceBridge = new DeviceBridge(null);
               }
               return __deviceBridge;
          }

          /**
             Returns a reference to the registered DisplayBridge.

             @return DisplayBridge reference or null if a bridge of this type is not registered.
          */
          public DisplayBridge GetDisplayBridge()
          {
               if(__displayBridge == null)
               {
                    __displayBridge = new DisplayBridge(null);
               }
               return __displayBridge;
          }

          /**
             Returns a reference to the registered FacebookBridge.

             @return FacebookBridge reference or null if a bridge of this type is not registered.
          */
          public FacebookBridge GetFacebookBridge()
          {
               if(__facebookBridge == null)
               {
                    __facebookBridge = new FacebookBridge(null);
               }
               return __facebookBridge;
          }

          /**
             Returns a reference to the registered FileBridge.

             @return FileBridge reference or null if a bridge of this type is not registered.
          */
          public FileBridge GetFileBridge()
          {
               if(__fileBridge == null)
               {
                    __fileBridge = new FileBridge(null);
               }
               return __fileBridge;
          }

          /**
             Returns a reference to the registered FileSystemBridge.

             @return FileSystemBridge reference or null if a bridge of this type is not registered.
          */
          public FileSystemBridge GetFileSystemBridge()
          {
               if(__filesystemBridge == null)
               {
                    __filesystemBridge = new FileSystemBridge(null);
               }
               return __filesystemBridge;
          }

          /**
             Returns a reference to the registered GeolocationBridge.

             @return GeolocationBridge reference or null if a bridge of this type is not registered.
          */
          public GeolocationBridge GetGeolocationBridge()
          {
               if(__geolocationBridge == null)
               {
                    __geolocationBridge = new GeolocationBridge(null);
               }
               return __geolocationBridge;
          }

          /**
             Returns a reference to the registered GlobalizationBridge.

             @return GlobalizationBridge reference or null if a bridge of this type is not registered.
          */
          public GlobalizationBridge GetGlobalizationBridge()
          {
               if(__globalizationBridge == null)
               {
                    __globalizationBridge = new GlobalizationBridge(null);
               }
               return __globalizationBridge;
          }

          /**
             Returns a reference to the registered GooglePlusBridge.

             @return GooglePlusBridge reference or null if a bridge of this type is not registered.
          */
          public GooglePlusBridge GetGooglePlusBridge()
          {
               if(__googleplusBridge == null)
               {
                    __googleplusBridge = new GooglePlusBridge(null);
               }
               return __googleplusBridge;
          }

          /**
             Returns a reference to the registered GyroscopeBridge.

             @return GyroscopeBridge reference or null if a bridge of this type is not registered.
          */
          public GyroscopeBridge GetGyroscopeBridge()
          {
               if(__gyroscopeBridge == null)
               {
                    __gyroscopeBridge = new GyroscopeBridge(null);
               }
               return __gyroscopeBridge;
          }

          /**
             Returns a reference to the registered ImagingBridge.

             @return ImagingBridge reference or null if a bridge of this type is not registered.
          */
          public ImagingBridge GetImagingBridge()
          {
               if(__imagingBridge == null)
               {
                    __imagingBridge = new ImagingBridge(null);
               }
               return __imagingBridge;
          }

          /**
             Returns a reference to the registered InternalStorageBridge.

             @return InternalStorageBridge reference or null if a bridge of this type is not registered.
          */
          public InternalStorageBridge GetInternalStorageBridge()
          {
               if(__internalstorageBridge == null)
               {
                    __internalstorageBridge = new InternalStorageBridge(null);
               }
               return __internalstorageBridge;
          }

          /**
             Returns a reference to the registered LifecycleBridge.

             @return LifecycleBridge reference or null if a bridge of this type is not registered.
          */
          public LifecycleBridge GetLifecycleBridge()
          {
               if(__lifecycleBridge == null)
               {
                    __lifecycleBridge = new LifecycleBridge(null);
               }
               return __lifecycleBridge;
          }

          /**
             Returns a reference to the registered LinkedInBridge.

             @return LinkedInBridge reference or null if a bridge of this type is not registered.
          */
          public LinkedInBridge GetLinkedInBridge()
          {
               if(__linkedinBridge == null)
               {
                    __linkedinBridge = new LinkedInBridge(null);
               }
               return __linkedinBridge;
          }

          /**
             Returns a reference to the registered LoggingBridge.

             @return LoggingBridge reference or null if a bridge of this type is not registered.
          */
          public LoggingBridge GetLoggingBridge()
          {
               if(__loggingBridge == null)
               {
                    __loggingBridge = new LoggingBridge(null);
               }
               return __loggingBridge;
          }

          /**
             Returns a reference to the registered MagnetometerBridge.

             @return MagnetometerBridge reference or null if a bridge of this type is not registered.
          */
          public MagnetometerBridge GetMagnetometerBridge()
          {
               if(__magnetometerBridge == null)
               {
                    __magnetometerBridge = new MagnetometerBridge(null);
               }
               return __magnetometerBridge;
          }

          /**
             Returns a reference to the registered MailBridge.

             @return MailBridge reference or null if a bridge of this type is not registered.
          */
          public MailBridge GetMailBridge()
          {
               if(__mailBridge == null)
               {
                    __mailBridge = new MailBridge(null);
               }
               return __mailBridge;
          }

          /**
             Returns a reference to the registered ManagementBridge.

             @return ManagementBridge reference or null if a bridge of this type is not registered.
          */
          public ManagementBridge GetManagementBridge()
          {
               if(__managementBridge == null)
               {
                    __managementBridge = new ManagementBridge(null);
               }
               return __managementBridge;
          }

          /**
             Returns a reference to the registered MapBridge.

             @return MapBridge reference or null if a bridge of this type is not registered.
          */
          public MapBridge GetMapBridge()
          {
               if(__mapBridge == null)
               {
                    __mapBridge = new MapBridge(null);
               }
               return __mapBridge;
          }

          /**
             Returns a reference to the registered MessagingBridge.

             @return MessagingBridge reference or null if a bridge of this type is not registered.
          */
          public MessagingBridge GetMessagingBridge()
          {
               if(__messagingBridge == null)
               {
                    __messagingBridge = new MessagingBridge(null);
               }
               return __messagingBridge;
          }

          /**
             Returns a reference to the registered NFCBridge.

             @return NFCBridge reference or null if a bridge of this type is not registered.
          */
          public NFCBridge GetNFCBridge()
          {
               if(__nfcBridge == null)
               {
                    __nfcBridge = new NFCBridge(null);
               }
               return __nfcBridge;
          }

          /**
             Returns a reference to the registered NetworkInfoBridge.

             @return NetworkInfoBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkInfoBridge GetNetworkInfoBridge()
          {
               if(__networkinfoBridge == null)
               {
                    __networkinfoBridge = new NetworkInfoBridge(null);
               }
               return __networkinfoBridge;
          }

          /**
             Returns a reference to the registered NetworkNamingBridge.

             @return NetworkNamingBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkNamingBridge GetNetworkNamingBridge()
          {
               if(__networknamingBridge == null)
               {
                    __networknamingBridge = new NetworkNamingBridge(null);
               }
               return __networknamingBridge;
          }

          /**
             Returns a reference to the registered NetworkReachabilityBridge.

             @return NetworkReachabilityBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkReachabilityBridge GetNetworkReachabilityBridge()
          {
               if(__networkreachabilityBridge == null)
               {
                    __networkreachabilityBridge = new NetworkReachabilityBridge(null);
               }
               return __networkreachabilityBridge;
          }

          /**
             Returns a reference to the registered NetworkStatusBridge.

             @return NetworkStatusBridge reference or null if a bridge of this type is not registered.
          */
          public NetworkStatusBridge GetNetworkStatusBridge()
          {
               if(__networkstatusBridge == null)
               {
                    __networkstatusBridge = new NetworkStatusBridge(null);
               }
               return __networkstatusBridge;
          }

          /**
             Returns a reference to the registered NotificationBridge.

             @return NotificationBridge reference or null if a bridge of this type is not registered.
          */
          public NotificationBridge GetNotificationBridge()
          {
               if(__notificationBridge == null)
               {
                    __notificationBridge = new NotificationBridge(null);
               }
               return __notificationBridge;
          }

          /**
             Returns a reference to the registered NotificationLocalBridge.

             @return NotificationLocalBridge reference or null if a bridge of this type is not registered.
          */
          public NotificationLocalBridge GetNotificationLocalBridge()
          {
               if(__notificationlocalBridge == null)
               {
                    __notificationlocalBridge = new NotificationLocalBridge(null);
               }
               return __notificationlocalBridge;
          }

          /**
             Returns a reference to the registered OAuthBridge.

             @return OAuthBridge reference or null if a bridge of this type is not registered.
          */
          public OAuthBridge GetOAuthBridge()
          {
               if(__oauthBridge == null)
               {
                    __oauthBridge = new OAuthBridge(null);
               }
               return __oauthBridge;
          }

          /**
             Returns a reference to the registered OCRBridge.

             @return OCRBridge reference or null if a bridge of this type is not registered.
          */
          public OCRBridge GetOCRBridge()
          {
               if(__ocrBridge == null)
               {
                    __ocrBridge = new OCRBridge(null);
               }
               return __ocrBridge;
          }

          /**
             Returns a reference to the registered OSBridge.

             @return OSBridge reference or null if a bridge of this type is not registered.
          */
          public OSBridge GetOSBridge()
          {
               if(__osBridge == null)
               {
                    __osBridge = new OSBridge(null);
               }
               return __osBridge;
          }

          /**
             Returns a reference to the registered OpenIdBridge.

             @return OpenIdBridge reference or null if a bridge of this type is not registered.
          */
          public OpenIdBridge GetOpenIdBridge()
          {
               if(__openidBridge == null)
               {
                    __openidBridge = new OpenIdBridge(null);
               }
               return __openidBridge;
          }

          /**
             Returns a reference to the registered PrintingBridge.

             @return PrintingBridge reference or null if a bridge of this type is not registered.
          */
          public PrintingBridge GetPrintingBridge()
          {
               if(__printingBridge == null)
               {
                    __printingBridge = new PrintingBridge(null);
               }
               return __printingBridge;
          }

          /**
             Returns a reference to the registered ProximityBridge.

             @return ProximityBridge reference or null if a bridge of this type is not registered.
          */
          public ProximityBridge GetProximityBridge()
          {
               if(__proximityBridge == null)
               {
                    __proximityBridge = new ProximityBridge(null);
               }
               return __proximityBridge;
          }

          /**
             Returns a reference to the registered QRCodeBridge.

             @return QRCodeBridge reference or null if a bridge of this type is not registered.
          */
          public QRCodeBridge GetQRCodeBridge()
          {
               if(__qrcodeBridge == null)
               {
                    __qrcodeBridge = new QRCodeBridge(null);
               }
               return __qrcodeBridge;
          }

          /**
             Returns a reference to the registered RSSBridge.

             @return RSSBridge reference or null if a bridge of this type is not registered.
          */
          public RSSBridge GetRSSBridge()
          {
               if(__rssBridge == null)
               {
                    __rssBridge = new RSSBridge(null);
               }
               return __rssBridge;
          }

          /**
             Returns a reference to the registered RuntimeBridge.

             @return RuntimeBridge reference or null if a bridge of this type is not registered.
          */
          public RuntimeBridge GetRuntimeBridge()
          {
               if(__runtimeBridge == null)
               {
                    __runtimeBridge = new RuntimeBridge(null);
               }
               return __runtimeBridge;
          }

          /**
             Returns a reference to the registered SecurityBridge.

             @return SecurityBridge reference or null if a bridge of this type is not registered.
          */
          public SecurityBridge GetSecurityBridge()
          {
               if(__securityBridge == null)
               {
                    __securityBridge = new SecurityBridge(null);
               }
               return __securityBridge;
          }

          /**
             Returns a reference to the registered ServiceBridge.

             @return ServiceBridge reference or null if a bridge of this type is not registered.
          */
          public ServiceBridge GetServiceBridge()
          {
               if(__serviceBridge == null)
               {
                    __serviceBridge = new ServiceBridge(null);
               }
               return __serviceBridge;
          }

          /**
             Returns a reference to the registered SettingsBridge.

             @return SettingsBridge reference or null if a bridge of this type is not registered.
          */
          public SettingsBridge GetSettingsBridge()
          {
               if(__settingsBridge == null)
               {
                    __settingsBridge = new SettingsBridge(null);
               }
               return __settingsBridge;
          }

          /**
             Returns a reference to the registered SocketBridge.

             @return SocketBridge reference or null if a bridge of this type is not registered.
          */
          public SocketBridge GetSocketBridge()
          {
               if(__socketBridge == null)
               {
                    __socketBridge = new SocketBridge(null);
               }
               return __socketBridge;
          }

          /**
             Returns a reference to the registered StoreBridge.

             @return StoreBridge reference or null if a bridge of this type is not registered.
          */
          public StoreBridge GetStoreBridge()
          {
               if(__storeBridge == null)
               {
                    __storeBridge = new StoreBridge(null);
               }
               return __storeBridge;
          }

          /**
             Returns a reference to the registered TelephonyBridge.

             @return TelephonyBridge reference or null if a bridge of this type is not registered.
          */
          public TelephonyBridge GetTelephonyBridge()
          {
               if(__telephonyBridge == null)
               {
                    __telephonyBridge = new TelephonyBridge(null);
               }
               return __telephonyBridge;
          }

          /**
             Returns a reference to the registered TimerBridge.

             @return TimerBridge reference or null if a bridge of this type is not registered.
          */
          public TimerBridge GetTimerBridge()
          {
               if(__timerBridge == null)
               {
                    __timerBridge = new TimerBridge(null);
               }
               return __timerBridge;
          }

          /**
             Returns a reference to the registered TwitterBridge.

             @return TwitterBridge reference or null if a bridge of this type is not registered.
          */
          public TwitterBridge GetTwitterBridge()
          {
               if(__twitterBridge == null)
               {
                    __twitterBridge = new TwitterBridge(null);
               }
               return __twitterBridge;
          }

          /**
             Returns a reference to the registered UIBridge.

             @return UIBridge reference or null if a bridge of this type is not registered.
          */
          public UIBridge GetUIBridge()
          {
               if(__uiBridge == null)
               {
                    __uiBridge = new UIBridge(null);
               }
               return __uiBridge;
          }

          /**
             Returns a reference to the registered UpdateBridge.

             @return UpdateBridge reference or null if a bridge of this type is not registered.
          */
          public UpdateBridge GetUpdateBridge()
          {
               if(__updateBridge == null)
               {
                    __updateBridge = new UpdateBridge(null);
               }
               return __updateBridge;
          }

          /**
             Returns a reference to the registered VibrationBridge.

             @return VibrationBridge reference or null if a bridge of this type is not registered.
          */
          public VibrationBridge GetVibrationBridge()
          {
               if(__vibrationBridge == null)
               {
                    __vibrationBridge = new VibrationBridge(null);
               }
               return __vibrationBridge;
          }

          /**
             Returns a reference to the registered VideoBridge.

             @return VideoBridge reference or null if a bridge of this type is not registered.
          */
          public VideoBridge GetVideoBridge()
          {
               if(__videoBridge == null)
               {
                    __videoBridge = new VideoBridge(null);
               }
               return __videoBridge;
          }

          /**
             Returns a reference to the registered WalletBridge.

             @return WalletBridge reference or null if a bridge of this type is not registered.
          */
          public WalletBridge GetWalletBridge()
          {
               if(__walletBridge == null)
               {
                    __walletBridge = new WalletBridge(null);
               }
               return __walletBridge;
          }

          /**
             Returns a reference to the registered XMLBridge.

             @return XMLBridge reference or null if a bridge of this type is not registered.
          */
          public XMLBridge GetXMLBridge()
          {
               if(__xmlBridge == null)
               {
                    __xmlBridge = new XMLBridge(null);
               }
               return __xmlBridge;
          }

          /**
             Provides a bridge to handle API calls for the given interface name.

             @param bridgeType String with the interface name required.
             @return APIBridge That handles calls for the specified interface or null if the given bridge is not registered.
          */
          public APIBridge GetBridge(String bridgeType) {
               switch(bridgeType) {

                    case "IAcceleration":
                         return GetAccelerationBridge();

                    case "IAds":
                         return GetAdsBridge();

                    case "IAlarm":
                         return GetAlarmBridge();

                    case "IAmbientLight":
                         return GetAmbientLightBridge();

                    case "IAnalytics":
                         return GetAnalyticsBridge();

                    case "IAudio":
                         return GetAudioBridge();

                    case "IBarcode":
                         return GetBarcodeBridge();

                    case "IBarometer":
                         return GetBarometerBridge();

                    case "IBluetooth":
                         return GetBluetoothBridge();

                    case "IBrowser":
                         return GetBrowserBridge();

                    case "ICalendar":
                         return GetCalendarBridge();

                    case "ICamera":
                         return GetCameraBridge();

                    case "ICapabilities":
                         return GetCapabilitiesBridge();

                    case "ICloud":
                         return GetCloudBridge();

                    case "ICompression":
                         return GetCompressionBridge();

                    case "IConcurrent":
                         return GetConcurrentBridge();

                    case "IContact":
                         return GetContactBridge();

                    case "ICrypto":
                         return GetCryptoBridge();

                    case "IDataStream":
                         return GetDataStreamBridge();

                    case "IDatabase":
                         return GetDatabaseBridge();

                    case "IDesktop":
                         return GetDesktopBridge();

                    case "IDevice":
                         return GetDeviceBridge();

                    case "IDisplay":
                         return GetDisplayBridge();

                    case "IFacebook":
                         return GetFacebookBridge();

                    case "IFile":
                         return GetFileBridge();

                    case "IFileSystem":
                         return GetFileSystemBridge();

                    case "IGeolocation":
                         return GetGeolocationBridge();

                    case "IGlobalization":
                         return GetGlobalizationBridge();

                    case "IGooglePlus":
                         return GetGooglePlusBridge();

                    case "IGyroscope":
                         return GetGyroscopeBridge();

                    case "IImaging":
                         return GetImagingBridge();

                    case "IInternalStorage":
                         return GetInternalStorageBridge();

                    case "ILifecycle":
                         return GetLifecycleBridge();

                    case "ILinkedIn":
                         return GetLinkedInBridge();

                    case "ILogging":
                         return GetLoggingBridge();

                    case "IMagnetometer":
                         return GetMagnetometerBridge();

                    case "IMail":
                         return GetMailBridge();

                    case "IManagement":
                         return GetManagementBridge();

                    case "IMap":
                         return GetMapBridge();

                    case "IMessaging":
                         return GetMessagingBridge();

                    case "INFC":
                         return GetNFCBridge();

                    case "INetworkInfo":
                         return GetNetworkInfoBridge();

                    case "INetworkNaming":
                         return GetNetworkNamingBridge();

                    case "INetworkReachability":
                         return GetNetworkReachabilityBridge();

                    case "INetworkStatus":
                         return GetNetworkStatusBridge();

                    case "INotification":
                         return GetNotificationBridge();

                    case "INotificationLocal":
                         return GetNotificationLocalBridge();

                    case "IOAuth":
                         return GetOAuthBridge();

                    case "IOCR":
                         return GetOCRBridge();

                    case "IOS":
                         return GetOSBridge();

                    case "IOpenId":
                         return GetOpenIdBridge();

                    case "IPrinting":
                         return GetPrintingBridge();

                    case "IProximity":
                         return GetProximityBridge();

                    case "IQRCode":
                         return GetQRCodeBridge();

                    case "IRSS":
                         return GetRSSBridge();

                    case "IRuntime":
                         return GetRuntimeBridge();

                    case "ISecurity":
                         return GetSecurityBridge();

                    case "IService":
                         return GetServiceBridge();

                    case "ISettings":
                         return GetSettingsBridge();

                    case "ISocket":
                         return GetSocketBridge();

                    case "IStore":
                         return GetStoreBridge();

                    case "ITelephony":
                         return GetTelephonyBridge();

                    case "ITimer":
                         return GetTimerBridge();

                    case "ITwitter":
                         return GetTwitterBridge();

                    case "IUI":
                         return GetUIBridge();

                    case "IUpdate":
                         return GetUpdateBridge();

                    case "IVibration":
                         return GetVibrationBridge();

                    case "IVideo":
                         return GetVideoBridge();

                    case "IWallet":
                         return GetWalletBridge();

                    case "IXML":
                         return GetXMLBridge();

                    default:
                         return null;
               }
          }

          /**
             Method that returns the API version of the implementation.
          */
          public String GetAPIVersion() {
               return "v2.1.2";
          }

          /**
             Returns a reference to the Platform Context
          */
          private AppContextBridge __appcontextBridge = null;

          /**
             Returns a reference to the Platform Context

             @return Reference to the platform context
             @since v2.0
          */
          public AppContextBridge GetPlatformContext() {
                if(__appcontextBridge == null) {
                    __appcontextBridge = new AppContextBridge(null);
               }
               return __appcontextBridge;
          }

          /**
             Returns a reference to the Webview platform context
          */
          private AppContextWebviewBridge __appcontextwebviewBridge = null;

          /**
             Returns a reference to the Webview platform context

             @return Reference to the Webview Context
             @since v2.0
          */
          public AppContextWebviewBridge GetPlatformContextWeb() {
                if(__appcontextwebviewBridge == null) {
                    __appcontextwebviewBridge = new AppContextWebviewBridge(null);
               }
               return __appcontextwebviewBridge;
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
