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
        Interface for Managing the Lifecycle listeners
        Auto-generated implementation of ILifecycle specification.
     */
public class LifecycleBridge : BaseApplicationBridge, ILifecycle, APIBridge
{

          /**
             API Delegate.
          */
          private ILifecycle _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public LifecycleBridge(ILifecycle _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return ILifecycle delegate that manages platform specific functions..
          */
          public ILifecycle GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(ILifecycle _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Add the listener for the lifecycle of the app

             @param listener Lifecycle listener
             @since v2.0
          */
          public void AddLifecycleListener(ILifecycleListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executing addLifecycleListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.AddLifecycleListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executed 'addLifecycleListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"LifecycleBridge no delegate for 'addLifecycleListener'.");
                    }
                    }
                    
               }

          /**
             Whether the application is in background or not

             @return true if the application is in background;false otherwise
             @since v2.0
          */
          public bool IsBackground()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executing isBackground.");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.IsBackground();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executed 'isBackground' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"LifecycleBridge no delegate for 'isBackground'.");
                    }
                    }
                    return result;                    
               }

          /**
             Un-registers an existing listener from receiving lifecycle events.

             @param listener Lifecycle listener
             @since v2.0
          */
          public void RemoveLifecycleListener(ILifecycleListener listener)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executing removeLifecycleListener({"+listener+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveLifecycleListener(listener);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executed 'removeLifecycleListener' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"LifecycleBridge no delegate for 'removeLifecycleListener'.");
                    }
                    }
                    
               }

          /**
             Removes all existing listeners from receiving lifecycle events.

             @since v2.0
          */
          public void RemoveLifecycleListeners()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executing removeLifecycleListeners.");
               }

               if (this._delegate != null)
               {
                    this._delegate.RemoveLifecycleListeners();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"LifecycleBridge executed 'removeLifecycleListeners' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"LifecycleBridge no delegate for 'removeLifecycleListeners'.");
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
                    case "addLifecycleListener":
                         ILifecycleListener listener0 = new LifecycleListenerImpl(request.GetAsyncId());
                         this.AddLifecycleListener(listener0);
                         break;
                    case "isBackground":
                         bool response1 = this.IsBackground();
                         responseJSON = GetJSONProcessor().SerializeObject(response1);
                         break;
                    case "removeLifecycleListener":
                         ILifecycleListener listener2 = new LifecycleListenerImpl(request.GetAsyncId());
                         this.RemoveLifecycleListener(listener2);
                         break;
                    case "removeLifecycleListeners":
                         this.RemoveLifecycleListeners();
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "LifecycleBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.2.";
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
