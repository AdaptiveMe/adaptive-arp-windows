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

    * @version v2.1.8

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the File operations
        Auto-generated implementation of IFile specification.
     */
public class FileBridge : BaseDataBridge, IFile, APIBridge
{

          /**
             API Delegate.
          */
          private IFile _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public FileBridge(IFile _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IFile delegate that manages platform specific functions..
          */
          public IFile GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IFile _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Determine whether the current file/folder can be read from.

             @param descriptor File descriptor of file or folder used for operation.
             @return True if the folder/file is readable, false otherwise.
             @since v2.0
          */
          public bool CanRead(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing canRead({"+descriptor+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.CanRead(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'canRead' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'canRead'.");
                    }
                    }
                    return result;                    
               }

          /**
             Determine whether the current file/folder can be written to.

             @param descriptor File descriptor of file or folder used for operation.
             @return True if the folder/file is writable, false otherwise.
             @since v2.0
          */
          public bool CanWrite(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing canWrite({"+descriptor+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.CanWrite(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'canWrite' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'canWrite'.");
                    }
                    }
                    return result;                    
               }

          /**
             Creates a file with the specified name.

             @param descriptor File descriptor of file or folder used for operation.
             @param callback   Result of the operation.
             @since v2.0
          */
          public void Create(FileDescriptor descriptor, IFileResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing create({"+descriptor+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.Create(descriptor, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'create' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'create'.");
                    }
                    }
                    
               }

          /**
             Deletes the given file or path. If the file is a directory and contains files and or subdirectories, these will be
deleted if the cascade parameter is set to true.

             @param descriptor File descriptor of file or folder used for operation.
             @param cascade    Whether to delete sub-files and sub-folders.
             @return True if files (and sub-files and folders) whether deleted.
             @since v2.0
          */
          public bool Delete(FileDescriptor descriptor, bool cascade)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing delete({"+descriptor+"},{"+cascade+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.Delete(descriptor, cascade);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'delete' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'delete'.");
                    }
                    }
                    return result;                    
               }

          /**
             Check whether the file/path exists.

             @param descriptor File descriptor of file or folder used for operation.
             @return True if the file exists in the filesystem, false otherwise.
             @since v2.0
          */
          public bool Exists(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing exists({"+descriptor+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.Exists(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'exists' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'exists'.");
                    }
                    }
                    return result;                    
               }

          /**
             Loads the content of the file.

             @param descriptor File descriptor of file or folder used for operation.
             @param callback   Result of the operation.
             @since v2.0
          */
          public void GetContent(FileDescriptor descriptor, IFileDataLoadResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing getContent({"+descriptor+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContent(descriptor, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'getContent' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'getContent'.");
                    }
                    }
                    
               }

          /**
             Returns the file storage type of the file

             @param descriptor File descriptor of file or folder used for operation.
             @return Storage Type file
             @since v2.0
          */
          public IFileSystemStorageType GetFileStorageType(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing getFileStorageType({"+descriptor+"}).");
               }

               IFileSystemStorageType result = IFileSystemStorageType.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetFileStorageType(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'getFileStorageType' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'getFileStorageType'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns the file type

             @param descriptor File descriptor of file or folder used for operation.
             @return Returns the file type of the file
             @since v2.0
          */
          public IFileSystemType GetFileType(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing getFileType({"+descriptor+"}).");
               }

               IFileSystemType result = IFileSystemType.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetFileType(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'getFileType' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'getFileType'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns the security type of the file

             @param descriptor File descriptor of file or folder used for operation.
             @return Security Level of the file
             @since v2.0
          */
          public IFileSystemSecurity GetSecurityType(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing getSecurityType({"+descriptor+"}).");
               }

               IFileSystemSecurity result = IFileSystemSecurity.Unknown;
               if (this._delegate != null)
               {
                    result = this._delegate.GetSecurityType(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'getSecurityType' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'getSecurityType'.");
                    }
                    }
                    return result;                    
               }

          /**
             Check whether this is a path of a file.

             @param descriptor File descriptor of file or folder used for operation.
             @return true if this is a path to a folder/directory, false if this is a path to a file.
             @since v2.0
          */
          public bool IsDirectory(FileDescriptor descriptor)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing isDirectory({"+descriptor+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.IsDirectory(descriptor);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'isDirectory' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'isDirectory'.");
                    }
                    }
                    return result;                    
               }

          /**
             List all the files contained within this file/path reference. If the reference is a file, it will not yield
any results.

             @param descriptor File descriptor of file or folder used for operation.
             @param callback   Result of operation.
             @since v2.0
          */
          public void ListFiles(FileDescriptor descriptor, IFileListResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing listFiles({"+descriptor+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.ListFiles(descriptor, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'listFiles' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'listFiles'.");
                    }
                    }
                    
               }

          /**
             List all the files matching the speficied regex filter within this file/path reference. If the reference
is a file, it will not yield any results.

             @param descriptor File descriptor of file or folder used for operation.
             @param regex      Filter (eg. *.jpg, *.png, Fil*) name string.
             @param callback   Result of operation.
             @since v2.0
          */
          public void ListFilesForRegex(FileDescriptor descriptor, string regex, IFileListResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing listFilesForRegex({"+descriptor+"},{"+regex+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.ListFilesForRegex(descriptor, regex, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'listFilesForRegex' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'listFilesForRegex'.");
                    }
                    }
                    
               }

          /**
             Creates the parent path (or paths, if recursive) to the given file/path if it doesn't already exist.

             @param descriptor File descriptor of file or folder used for operation.
             @param recursive  Whether to create all parent path elements.
             @return True if the path was created, false otherwise (or it exists already).
             @since v2.0
          */
          public bool MkDir(FileDescriptor descriptor, bool recursive)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing mkDir({"+descriptor+"},{"+recursive+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.MkDir(descriptor, recursive);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'mkDir' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'mkDir'.");
                    }
                    }
                    return result;                    
               }

          /**
             Moves the current file to the given file destination, optionally overwriting and creating the path to the
new destination file.

             @param source      File descriptor of file or folder used for operation as source.
             @param destination File descriptor of file or folder used for operation as destination.
             @param createPath  True to create the path if it does not already exist.
             @param callback    Result of the operation.
             @param overwrite   True to create the path if it does not already exist.
             @since v2.0
          */
          public void Move(FileDescriptor source, FileDescriptor destination, bool createPath, bool overwrite, IFileResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing move({"+source+"},{"+destination+"},{"+createPath+"},{"+overwrite+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.Move(source, destination, createPath, overwrite, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'move' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'move'.");
                    }
                    }
                    
               }

          /**
             Sets the content of the file.

             @param descriptor File descriptor of file or folder used for operation.
             @param content    Binary content to store in the file.
             @param callback   Result of the operation.
             @since v2.0
          */
          public void SetContent(FileDescriptor descriptor, byte[] content, IFileDataStoreResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executing setContent({"+descriptor+"},{"+content+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.SetContent(descriptor, content, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"FileBridge executed 'setContent' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"FileBridge no delegate for 'setContent'.");
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
                    case "canRead":
                         FileDescriptor descriptor0 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool response0 = this.CanRead(descriptor0);
                         responseJSON = GetJSONProcessor().SerializeObject(response0);
                         break;
                    case "canWrite":
                         FileDescriptor descriptor1 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool response1 = this.CanWrite(descriptor1);
                         responseJSON = GetJSONProcessor().SerializeObject(response1);
                         break;
                    case "create":
                         FileDescriptor descriptor2 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileResultCallback callback2 = new FileResultCallbackImpl(request.GetAsyncId());
                         this.Create(descriptor2, callback2);
                         break;
                    case "delete":
                         FileDescriptor descriptor3 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool cascade3 = GetJSONProcessor().DeserializeObject<bool>(request.GetParameters()[1]);
                         bool response3 = this.Delete(descriptor3, cascade3);
                         responseJSON = GetJSONProcessor().SerializeObject(response3);
                         break;
                    case "exists":
                         FileDescriptor descriptor4 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool response4 = this.Exists(descriptor4);
                         responseJSON = GetJSONProcessor().SerializeObject(response4);
                         break;
                    case "getContent":
                         FileDescriptor descriptor5 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileDataLoadResultCallback callback5 = new FileDataLoadResultCallbackImpl(request.GetAsyncId());
                         this.GetContent(descriptor5, callback5);
                         break;
                    case "getFileStorageType":
                         FileDescriptor descriptor6 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileSystemStorageType response6 = this.GetFileStorageType(descriptor6);
                         responseJSON = GetJSONProcessor().SerializeObject(response6);
                         break;
                    case "getFileType":
                         FileDescriptor descriptor7 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileSystemType response7 = this.GetFileType(descriptor7);
                         responseJSON = GetJSONProcessor().SerializeObject(response7);
                         break;
                    case "getSecurityType":
                         FileDescriptor descriptor8 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileSystemSecurity response8 = this.GetSecurityType(descriptor8);
                         responseJSON = GetJSONProcessor().SerializeObject(response8);
                         break;
                    case "isDirectory":
                         FileDescriptor descriptor9 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool response9 = this.IsDirectory(descriptor9);
                         responseJSON = GetJSONProcessor().SerializeObject(response9);
                         break;
                    case "listFiles":
                         FileDescriptor descriptor10 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         IFileListResultCallback callback10 = new FileListResultCallbackImpl(request.GetAsyncId());
                         this.ListFiles(descriptor10, callback10);
                         break;
                    case "listFilesForRegex":
                         FileDescriptor descriptor11 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         string regex11 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         IFileListResultCallback callback11 = new FileListResultCallbackImpl(request.GetAsyncId());
                         this.ListFilesForRegex(descriptor11, regex11, callback11);
                         break;
                    case "mkDir":
                         FileDescriptor descriptor12 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         bool recursive12 = GetJSONProcessor().DeserializeObject<bool>(request.GetParameters()[1]);
                         bool response12 = this.MkDir(descriptor12, recursive12);
                         responseJSON = GetJSONProcessor().SerializeObject(response12);
                         break;
                    case "move":
                         FileDescriptor source13 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         FileDescriptor destination13 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[1]);
                         bool createPath13 = GetJSONProcessor().DeserializeObject<bool>(request.GetParameters()[2]);
                         bool overwrite13 = GetJSONProcessor().DeserializeObject<bool>(request.GetParameters()[3]);
                         IFileResultCallback callback13 = new FileResultCallbackImpl(request.GetAsyncId());
                         this.Move(source13, destination13, createPath13, overwrite13, callback13);
                         break;
                    case "setContent":
                         FileDescriptor descriptor14 = GetJSONProcessor().DeserializeObject<FileDescriptor>(request.GetParameters()[0]);
                         byte[] content14 = GetJSONProcessor().DeserializeObject<byte[]>(request.GetParameters()[1]);
                         IFileDataStoreResultCallback callback14 = new FileDataStoreResultCallbackImpl(request.GetAsyncId());
                         this.SetContent(descriptor14, content14, callback14);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "FileBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.8.";
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
