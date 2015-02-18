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

    * @version v2.1.7

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the File System operations
        Auto-generated implementation of IFileSystem specification.
     */
public class FileSystemBridge : BaseDataBridge, IFileSystem, APIBridge
{

          /**
             API Delegate.
          */
          private IFileSystem _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public FileSystemBridge(IFileSystem _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IFileSystem delegate that manages platform specific functions..
          */
          public IFileSystem GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IFileSystem _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Creates a new reference to a new or existing location in the filesystem.
This method does not create the actual file in the specified folder.

             @param parent Parent directory.
             @param name   Name of new file or directory.
             @return A reference to a new or existing location in the filesystem.
             @since v2.0
          */
          public FileDescriptor CreateFileDescriptor(FileDescriptor parent, string name)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing createFileDescriptor({"+parent+"},{"+name+"}).");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.CreateFileDescriptor(parent, name);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'createFileDescriptor' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'createFileDescriptor'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the cache folder for the current application.
This path must always be writable by the current application.
This path is volatile and may be cleaned by the OS periodically.

             @return Path to the application's cache folder.
             @since v2.0
          */
          public FileDescriptor GetApplicationCacheFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getApplicationCacheFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetApplicationCacheFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getApplicationCacheFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getApplicationCacheFolder'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the cloud synchronizable folder for the current application.
This path must always be writable by the current application.

             @return Path to the application's cloud storage folder.
             @since v2.0
          */
          public FileDescriptor GetApplicationCloudFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getApplicationCloudFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetApplicationCloudFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getApplicationCloudFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getApplicationCloudFolder'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the documents folder for the current application.
This path must always be writable by the current application.

             @return Path to the application's documents folder.
             @since v2.0
          */
          public FileDescriptor GetApplicationDocumentsFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getApplicationDocumentsFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetApplicationDocumentsFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getApplicationDocumentsFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getApplicationDocumentsFolder'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the application installation folder.
This path may or may not be directly readable or writable - it usually contains the app binary and data.

             @return Path to the application folder.
             @since v2.0
          */
          public FileDescriptor GetApplicationFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getApplicationFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetApplicationFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getApplicationFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getApplicationFolder'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the protected storage folder for the current application.
This path must always be writable by the current application.

             @return Path to the application's protected storage folder.
             @since v2.0
          */
          public FileDescriptor GetApplicationProtectedFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getApplicationProtectedFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetApplicationProtectedFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getApplicationProtectedFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getApplicationProtectedFolder'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns the file system dependent file separator.

             @return char with the directory/file separator.
             @since v2.0
          */
          public char GetSeparator()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getSeparator.");
               }

               char result = ' ';
               if (this._delegate != null)
               {
                    result = this._delegate.GetSeparator();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getSeparator' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getSeparator'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns a reference to the external storage folder provided by the OS. This may
be an external SSD card or similar. This type of storage is removable and by
definition, not secure.
This path may or may not be writable by the current application.

             @return Path to the application's documents folder.
             @since v2.0
          */
          public FileDescriptor GetSystemExternalFolder()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executing getSystemExternalFolder.");
               }

               FileDescriptor result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetSystemExternalFolder();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileSystemBridge executed 'getSystemExternalFolder' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileSystemBridge no delegate for 'getSystemExternalFolder'.");
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
                    case "createFileDescriptor":
                         FileDescriptor parent0 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         string name0 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         FileDescriptor response0 = this.CreateFileDescriptor(parent0, name0);
                         if (response0 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response0);
                         }
                         break;
                    case "getApplicationCacheFolder":
                         FileDescriptor response1 = this.GetApplicationCacheFolder();
                         if (response1 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response1);
                         }
                         break;
                    case "getApplicationCloudFolder":
                         FileDescriptor response2 = this.GetApplicationCloudFolder();
                         if (response2 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response2);
                         }
                         break;
                    case "getApplicationDocumentsFolder":
                         FileDescriptor response3 = this.GetApplicationDocumentsFolder();
                         if (response3 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response3);
                         }
                         break;
                    case "getApplicationFolder":
                         FileDescriptor response4 = this.GetApplicationFolder();
                         if (response4 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response4);
                         }
                         break;
                    case "getApplicationProtectedFolder":
                         FileDescriptor response5 = this.GetApplicationProtectedFolder();
                         if (response5 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response5);
                         }
                         break;
                    case "getSeparator":
                         char response6 = this.GetSeparator();
                         responseJSON = GetJSONProcessor().SerializeObject(response6);
                         break;
                    case "getSystemExternalFolder":
                         FileDescriptor response7 = this.GetSystemExternalFolder();
                         if (response7 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response7);
                         }
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "FileSystemBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.7.";
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
