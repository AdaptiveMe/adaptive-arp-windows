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

    * @version v2.2.0

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the browser operations
        Auto-generated implementation of IBrowser specification.
     */
public class BrowserBridge : BaseUIBridge, IBrowser, APIBridge
{

          /**
             API Delegate.
          */
          private IBrowser _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public BrowserBridge(IBrowser _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IBrowser delegate that manages platform specific functions..
          */
          public IBrowser GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IBrowser _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Method for opening a URL like a link in the native default browser

             @param url Url to open
             @return The result of the operation
             @since v2.0
          */
          public bool OpenExtenalBrowser(string url)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executing openExtenalBrowser({"+url+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.OpenExtenalBrowser(url);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executed 'openExtenalBrowser' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"BrowserBridge no delegate for 'openExtenalBrowser'.");
                    }
                    }
                    return result;                    
               }

          /**
             Method for opening a browser embedded into the application

             @param url            Url to open
             @param title          Title of the Navigation bar
             @param backButtonText Title of the Back button bar
             @return The result of the operation
             @since v2.0
          */
          public bool OpenInternalBrowser(string url, string title, string backButtonText)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executing openInternalBrowser({"+url+"},{"+title+"},{"+backButtonText+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.OpenInternalBrowser(url, title, backButtonText);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executed 'openInternalBrowser' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"BrowserBridge no delegate for 'openInternalBrowser'.");
                    }
                    }
                    return result;                    
               }

          /**
             Method for opening a browser embedded into the application in a modal window

             @param url            Url to open
             @param title          Title of the Navigation bar
             @param backButtonText Title of the Back button bar
             @return The result of the operation
             @since v2.0
          */
          public bool OpenInternalBrowserModal(string url, string title, string backButtonText)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executing openInternalBrowserModal({"+url+"},{"+title+"},{"+backButtonText+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.OpenInternalBrowserModal(url, title, backButtonText);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"BrowserBridge executed 'openInternalBrowserModal' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"BrowserBridge no delegate for 'openInternalBrowserModal'.");
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
                    case "openExtenalBrowser":
                         string url0 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         bool response0 = this.OpenExtenalBrowser(url0);
                         responseJSON = GetJSONProcessor().SerializeObject(response0);
                         break;
                    case "openInternalBrowser":
                         string url1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         string title1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         string backButtonText1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[2]);
                         bool response1 = this.OpenInternalBrowser(url1, title1, backButtonText1);
                         responseJSON = GetJSONProcessor().SerializeObject(response1);
                         break;
                    case "openInternalBrowserModal":
                         string url2 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         string title2 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         string backButtonText2 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[2]);
                         bool response2 = this.OpenInternalBrowserModal(url2, title2, backButtonText2);
                         responseJSON = GetJSONProcessor().SerializeObject(response2);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "BrowserBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.2.0.";
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
