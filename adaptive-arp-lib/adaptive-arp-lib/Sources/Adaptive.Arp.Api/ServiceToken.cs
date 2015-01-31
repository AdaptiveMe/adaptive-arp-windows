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
        Object representing a specific service, path, function and invocation method for accessing external services.

        @author Carlos Lozano Diez
        @since v2.0.6
        @version 1.0
     */
     public class ServiceToken : APIBean
     {

          /**
             Http method to be used by the invocation - this is typically GET or POST albeit the platform may support
other invocation methods. This is also defined per function of each endpoint in the platform's XML file.
          */
          public IServiceMethod InvocationMethod { get; set; }
          /**
             Name of the endpoint configured in the platform's services XML file. This is a reference to a specific schema,
host and port combination for a given service.
          */
          public string EndpointName { get; set; }
          /**
             Name of the function configured in the platform's services XML file for a specific endpoint. This is a reference
to a relative path of a function published on a remote service.
          */
          public string FunctionName { get; set; }
          /**
             Name of the service configured in the platform's services XML file.
          */
          public string ServiceName { get; set; }

          /**
             Default constructor.

             @since V2.0.6
          */
          public ServiceToken()  {
          }

          /**
             Convenience constructor.

             @param ServiceName      Name of the configured service.
             @param EndpointName     Name of the endpoint configured for the service.
             @param FunctionName     Name of the function configured for the endpoint.
             @param InvocationMethod Method type configured for the function.
             @since V2.0.6
          */
          public ServiceToken(string serviceName, string endpointName, string functionName, IServiceMethod invocationMethod) : base () {
               this.ServiceName = ServiceName;
               this.EndpointName = EndpointName;
               this.FunctionName = FunctionName;
               this.InvocationMethod = InvocationMethod;
          }

          /**
             Get token's invocation method type.

             @return Invocation method type.
             @since V2.0.6
          */
          public IServiceMethod GetInvocationMethod() {
               return this.InvocationMethod;
          }

          /**
             Sets the invocation method type.

             @param InvocationMethod Method type.
             @since V2.0.6
          */
          public void SetInvocationMethod(IServiceMethod InvocationMethod) {
               this.InvocationMethod = InvocationMethod;
          }

          /**
             Get token's endpoint name.

             @return Endpoint name.
             @since V2.0.6
          */
          public string GetEndpointName() {
               return this.EndpointName;
          }

          /**
             Set the endpoint name.

             @param EndpointName Endpoint name.
             @since V2.0.6
          */
          public void SetEndpointName(string EndpointName) {
               this.EndpointName = EndpointName;
          }

          /**
             Get token's function name.

             @return Function name.
             @since V2.0.6
          */
          public string GetFunctionName() {
               return this.FunctionName;
          }

          /**
             Sets the function name.

             @param FunctionName Function name.
             @since V2.0.6
          */
          public void SetFunctionName(string FunctionName) {
               this.FunctionName = FunctionName;
          }

          /**
             Get token's service name.

             @return Service name.
             @since V2.0.6
          */
          public string GetServiceName() {
               return this.ServiceName;
          }

          /**
             Sets token's service name.

             @param ServiceName Service name.
             @since V2.0.6
          */
          public void SetServiceName(string ServiceName) {
               this.ServiceName = ServiceName;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
