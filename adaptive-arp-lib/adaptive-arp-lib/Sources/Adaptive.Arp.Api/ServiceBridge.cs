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

    * @version v2.1.1

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Services operations
        Auto-generated implementation of IService specification.
     */
public class ServiceBridge : BaseCommunicationBridge, IService, APIBridge
{

          /**
             API Delegate.
          */
          private IService _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public ServiceBridge(IService _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IService delegate that manages platform specific functions..
          */
          public IService GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IService _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Create a service request for the given ServiceToken. This method creates the request, populating
existing headers and cookies for the same service. The request is populated with all the defaults
for the service being invoked and requires only the request body to be set. Headers and cookies may be
manipulated as needed by the application before submitting the ServiceRequest via invokeService.

             @param serviceToken ServiceToken to be used for the creation of the request.
             @return ServiceRequest with pre-populated headers, cookies and defaults for the service.
             @since v2.0.6
          */
          public ServiceRequest GetServiceRequest(ServiceToken serviceToken)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executing getServiceRequest({"+serviceToken+"}).");
               }

               ServiceRequest result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetServiceRequest(serviceToken);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executed 'getServiceRequest' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"ServiceBridge no delegate for 'getServiceRequest'.");
                    }
                    }
                    return result;                    
               }

          /**
             Obtains a ServiceToken for the given parameters to be used for the creation of requests.

             @param serviceName  Service name.
             @param endpointName Endpoint name.
             @param functionName Function name.
             @param method       Method type.
             @return ServiceToken to create a service request or null if the given parameter combination is not
configured in the platform's XML service definition file.
             @since v2.0.6
          */
          public ServiceToken GetServiceToken(string serviceName, string endpointName, string functionName, IServiceMethod method)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executing getServiceToken({"+serviceName+"},{"+endpointName+"},{"+functionName+"},{"+method+"}).");
               }

               ServiceToken result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetServiceToken(serviceName, endpointName, functionName, method);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executed 'getServiceToken' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"ServiceBridge no delegate for 'getServiceToken'.");
                    }
                    }
                    return result;                    
               }

          /**
             Returns all the possible service tokens configured in the platform's XML service definition file.

             @return Array of service tokens configured.
             @since v2.0.6
          */
          public ServiceToken[] GetServicesRegistered()
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executing getServicesRegistered.");
               }

               ServiceToken[] result = null;
               if (this._delegate != null)
               {
                    result = this._delegate.GetServicesRegistered();
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executed 'getServicesRegistered' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"ServiceBridge no delegate for 'getServicesRegistered'.");
                    }
                    }
                    return result;                    
               }

          /**
             Executes the given ServiceRequest and provides responses to the given callback handler.

             @param serviceRequest ServiceRequest with the request body.
             @param callback       IServiceResultCallback to handle the ServiceResponse.
             @since v2.0.6
          */
          public void InvokeService(ServiceRequest serviceRequest, IServiceResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executing invokeService({"+serviceRequest+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.InvokeService(serviceRequest, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executed 'invokeService' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"ServiceBridge no delegate for 'invokeService'.");
                    }
                    }
                    
               }

          /**
             Checks whether a specific service, endpoint, function and method type is configured in the platform's
XML service definition file.

             @param serviceName  Service name.
             @param endpointName Endpoint name.
             @param functionName Function name.
             @param method       Method type.
             @return Returns true if the service is configured, false otherwise.
             @since v2.0.6
          */
          public bool IsServiceRegistered(string serviceName, string endpointName, string functionName, IServiceMethod method)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executing isServiceRegistered({"+serviceName+"},{"+endpointName+"},{"+functionName+"},{"+method+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.IsServiceRegistered(serviceName, endpointName, functionName, method);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"ServiceBridge executed 'isServiceRegistered' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"ServiceBridge no delegate for 'isServiceRegistered'.");
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
                    case "getServiceRequest":
                         ServiceToken serviceToken0 = GetJSONProcessor().DeserializeObject<ServiceToken>(request.GetParameters()[0]);
                         ServiceRequest response0 = this.GetServiceRequest(serviceToken0);
                         if (response0 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response0);
                         }
                         break;
                    case "getServiceToken":
                         string serviceName1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         string endpointName1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         string functionName1 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[2]);
                         IServiceMethod method1 = GetJSONProcessor().DeserializeObject<IServiceMethod>(request.GetParameters()[3]);
                         ServiceToken response1 = this.GetServiceToken(serviceName1, endpointName1, functionName1, method1);
                         if (response1 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response1);
                         }
                         break;
                    case "getServicesRegistered":
                         ServiceToken[] response2 = this.GetServicesRegistered();
                         if (response2 != null)
                         {
                              responseJSON = GetJSONProcessor().SerializeObject(response2);
                         }
                         break;
                    case "invokeService":
                         ServiceRequest serviceRequest3 = GetJSONProcessor().DeserializeObject<ServiceRequest>(request.GetParameters()[0]);
                         IServiceResultCallback callback3 = new ServiceResultCallbackImpl(request.GetAsyncId());
                         this.InvokeService(serviceRequest3, callback3);
                         break;
                    case "isServiceRegistered":
                         string serviceName4 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         string endpointName4 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         string functionName4 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[2]);
                         IServiceMethod method4 = GetJSONProcessor().DeserializeObject<IServiceMethod>(request.GetParameters()[3]);
                         bool response4 = this.IsServiceRegistered(serviceName4, endpointName4, functionName4, method4);
                         responseJSON = GetJSONProcessor().SerializeObject(response4);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "ServiceBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.1.";
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
