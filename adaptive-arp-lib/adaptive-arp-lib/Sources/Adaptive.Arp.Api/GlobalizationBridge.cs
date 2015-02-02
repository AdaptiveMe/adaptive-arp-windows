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
        Interface for Managing the Globalization results
        Auto-generated implementation of IGlobalization specification.
     */
public class GlobalizationBridge : BaseApplicationBridge, IGlobalization, APIBridge
{

          /**
             API Delegate.
          */
          private IGlobalization _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public GlobalizationBridge(IGlobalization _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IGlobalization delegate that manages platform specific functions..
          */
          public IGlobalization GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IGlobalization _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Returns the default locale of the application defined in the configuration file

             @return Default Locale of the application
             @since v2.0
          */
          public Locale GetDefaultLocale()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executing getDefaultLocale.");
               }

               Locale result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetDefaultLocale();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executed 'getDefaultLocale' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GlobalizationBridge no delegate for 'getDefaultLocale'.");
                    }
                    }
                    return result;                    
               }

          /**
             List of supported locales for the application defined in the configuration file

             @return List of locales
             @since v2.0
          */
          public Locale[] GetLocaleSupportedDescriptors()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executing getLocaleSupportedDescriptors.");
               }

               Locale[] result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetLocaleSupportedDescriptors();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executed 'getLocaleSupportedDescriptors' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GlobalizationBridge no delegate for 'getLocaleSupportedDescriptors'.");
                    }
                    }
                    return result;                    
               }

          /**
             Gets the text/message corresponding to the given key and locale.

             @param key    to match text
             @param locale The locale object to get localized message, or the locale desciptor ("language" or "language-country" two-letters ISO codes.
             @return Localized text.
             @since v2.0
          */
          public string GetResourceLiteral(string key, Locale locale)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executing getResourceLiteral({"+key+"},{"+locale+"}).");
               }

               string result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetResourceLiteral(key, locale);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executed 'getResourceLiteral' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GlobalizationBridge no delegate for 'getResourceLiteral'.");
                    }
                    }
                    return result;                    
               }

          /**
             Gets the full application configured literals (key/message pairs) corresponding to the given locale.

             @param locale The locale object to get localized message, or the locale desciptor ("language" or "language-country" two-letters ISO codes.
             @return Localized texts in the form of an object.
             @since v2.0
          */
          public KeyPair[] GetResourceLiterals(Locale locale)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executing getResourceLiterals({"+locale+"}).");
               }

               KeyPair[] result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetResourceLiterals(locale);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"GlobalizationBridge executed 'getResourceLiterals' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"GlobalizationBridge no delegate for 'getResourceLiterals'.");
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
                    case "getDefaultLocale":
                         Locale response0 = this.GetDefaultLocale();
                         if (response0 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response0);
                         }
                         break;
                    case "getLocaleSupportedDescriptors":
                         Locale[] response1 = this.GetLocaleSupportedDescriptors();
                         if (response1 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response1);
                         }
                         break;
                    case "getResourceLiteral":
                         string key2 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         Locale locale2 = GetJSONProcessor().DeserializeObject<Locale>(request.GetParameters()[1]);
                         string response2 = this.GetResourceLiteral(key2, locale2);
                         if (response2 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response2);
                         }
                         break;
                    case "getResourceLiterals":
                         Locale locale3 = GetJSONProcessor().DeserializeObject<Locale>(request.GetParameters()[0]);
                         KeyPair[] response3 = this.GetResourceLiterals(locale3);
                         if (response3 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response3);
                         }
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "GlobalizationBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.2.";
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
