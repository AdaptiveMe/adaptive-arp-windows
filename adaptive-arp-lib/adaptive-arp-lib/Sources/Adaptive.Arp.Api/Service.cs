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

    * @version v2.1.6

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents an instance of a service.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class Service
     {

          /**
             The service name
          */
          public string Name { get; set; }
          /**
             Endpoint of the service
          */
          public ServiceEndpoint[] ServiceEndpoints { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Service()  {
          }

          /**
             Constructor used by the implementation

             @param ServiceEndpoints Endpoints of the service
             @param Name             Name of the service
             @since V2.0.6
          */
          public Service(ServiceEndpoint[] serviceEndpoints, string name) : base () {
               this.ServiceEndpoints = ServiceEndpoints;
               this.Name = Name;
          }

          /**
             Returns the name

             @return Name
             @since V2.0
          */
          public string GetName() {
               return this.Name;
          }

          /**
             Set the name

             @param Name Name of the service
             @since V2.0
          */
          public void SetName(string Name) {
               this.Name = Name;
          }

          /**
             Returns the serviceEndpoints

             @return ServiceEndpoints
             @since V2.0
          */
          public ServiceEndpoint[] GetServiceEndpoints() {
               return this.ServiceEndpoints;
          }

          /**
             Set the serviceEndpoints

             @param ServiceEndpoints Endpoint of the service
             @since V2.0
          */
          public void SetServiceEndpoints(ServiceEndpoint[] ServiceEndpoints) {
               this.ServiceEndpoints = ServiceEndpoints;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
