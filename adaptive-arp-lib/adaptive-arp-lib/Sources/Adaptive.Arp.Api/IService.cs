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

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Services operations

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public interface IService : IBaseCommunication
     {

          /**
             Create a service request for the given ServiceToken. This method creates the request, populating
existing headers and cookies for the same service. The request is populated with all the defaults
for the service being invoked and requires only the request body to be set. Headers and cookies may be
manipulated as needed by the application before submitting the ServiceRequest via invokeService.

             @param serviceToken ServiceToken to be used for the creation of the request.
             @return ServiceRequest with pre-populated headers, cookies and defaults for the service.
             @since v2.0.6
          */
          ServiceRequest GetServiceRequest(ServiceToken serviceToken);

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
          ServiceToken GetServiceToken(string serviceName, string endpointName, string functionName, IServiceMethod method);

          /**
             Returns all the possible service tokens configured in the platform's XML service definition file.

             @return Array of service tokens configured.
             @since v2.0.6
          */
          ServiceToken[] GetServicesRegistered();

          /**
             Executes the given ServiceRequest and provides responses to the given callback handler.

             @param serviceRequest ServiceRequest with the request body.
             @param callback       IServiceResultCallback to handle the ServiceResponse.
             @since v2.0.6
          */
          void InvokeService(ServiceRequest serviceRequest, IServiceResultCallback callback);

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
          bool IsServiceRegistered(string serviceName, string endpointName, string functionName, IServiceMethod method);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
