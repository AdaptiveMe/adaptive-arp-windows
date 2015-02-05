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
        Interface for Managing the Security operations
        Auto-generated implementation of ISecurity specification.
     */
public class SecurityBridge : BaseSecurityBridge, ISecurity, APIBridge
{

          /**
             API Delegate.
          */
          private ISecurity _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public SecurityBridge(ISecurity _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return ISecurity delegate that manages platform specific functions..
          */
          public ISecurity GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(ISecurity _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Deletes from the device internal storage the entry/entries containing the specified key names.

             @param keys             Array with the key names to delete.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void DeleteSecureKeyValuePairs(string[] keys, string publicAccessName, ISecurityResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executing deleteSecureKeyValuePairs({"+keys+"},{"+publicAccessName+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.DeleteSecureKeyValuePairs(keys, publicAccessName, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executed 'deleteSecureKeyValuePairs' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"SecurityBridge no delegate for 'deleteSecureKeyValuePairs'.");
                    }
                    }
                    
               }

          /**
             Retrieves from the device internal storage the entry/entries containing the specified key names.

             @param keys             Array with the key names to retrieve.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void GetSecureKeyValuePairs(string[] keys, string publicAccessName, ISecurityResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executing getSecureKeyValuePairs({"+keys+"},{"+publicAccessName+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetSecureKeyValuePairs(keys, publicAccessName, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executed 'getSecureKeyValuePairs' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"SecurityBridge no delegate for 'getSecureKeyValuePairs'.");
                    }
                    }
                    
               }

          /**
             Returns if the device has been modified in anyhow

             @return true if the device has been modified; false otherwise
             @since v2.0
          */
          public bool IsDeviceModified()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executing isDeviceModified.");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.IsDeviceModified();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executed 'isDeviceModified' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"SecurityBridge no delegate for 'isDeviceModified'.");
                    }
                    }
                    return result;                    
               }

          /**
             Stores in the device internal storage the specified item/s.

             @param keyValues        Array containing the items to store on the device internal memory.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void SetSecureKeyValuePairs(SecureKeyPair[] keyValues, string publicAccessName, ISecurityResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executing setSecureKeyValuePairs({"+keyValues+"},{"+publicAccessName+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.SetSecureKeyValuePairs(keyValues, publicAccessName, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"SecurityBridge executed 'setSecureKeyValuePairs' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"SecurityBridge no delegate for 'setSecureKeyValuePairs'.");
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
                    case "deleteSecureKeyValuePairs":
                         string[] keys0 = GetJSONProcessor().DeserializeObject<string[]>(request.GetParameters()[0]);
                         string publicAccessName0 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         ISecurityResultCallback callback0 = new SecurityResultCallbackImpl(request.GetAsyncId());
                         this.DeleteSecureKeyValuePairs(keys0, publicAccessName0, callback0);
                         break;
                    case "getSecureKeyValuePairs":
                         string[] keys1 = GetJSONProcessor().DeserializeObject<string[]>(request.GetParameters()[0]);
                         string publicAccessName1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         ISecurityResultCallback callback1 = new SecurityResultCallbackImpl(request.GetAsyncId());
                         this.GetSecureKeyValuePairs(keys1, publicAccessName1, callback1);
                         break;
                    case "isDeviceModified":
                         bool response2 = this.IsDeviceModified();
                         responseJSON = GetJSONProcessor().SerializeObject(response2);
                         break;
                    case "setSecureKeyValuePairs":
                         SecureKeyPair[] keyValues3 = GetJSONProcessor().DeserializeObject<SecureKeyPair[]>(request.GetParameters()[0]);
                         string publicAccessName3 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         ISecurityResultCallback callback3 = new SecurityResultCallbackImpl(request.GetAsyncId());
                         this.SetSecureKeyValuePairs(keyValues3, publicAccessName3, callback3);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "SecurityBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.5.";
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
