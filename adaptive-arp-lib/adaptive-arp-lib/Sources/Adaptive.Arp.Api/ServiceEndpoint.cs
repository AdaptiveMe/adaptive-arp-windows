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

namespace Adaptive.Arp.Api
{
     /**
        Structure representing a remote or local service access end-point.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class ServiceEndpoint
     {

          /**
             Type of validation to be performed for SSL hosts.
          */
          public IServiceCertificateValidation ValidationType { get; set; }
          /**
             The remote service hostURI URI (alias or IP) composed of scheme://hostURI:port (http://hostURI:8080).
          */
          public string HostURI { get; set; }
          /**
             The remote service paths (to be added to the hostURI and port url).
          */
          public ServicePath[] Paths { get; set; }

          /**
             Default Constructor

             @since V2.0
          */
          public ServiceEndpoint()  {
          }

          /**
             Constructor with parameters

             @param HostURI Remote service hostURI
             @param Paths   Remote service Paths
             @since V2.0.6
          */
          public ServiceEndpoint(string hostURI, ServicePath[] paths) : base () {
               this.HostURI = HostURI;
               this.Paths = Paths;
          }

          /**
             Gets the validation type for the certificate of a SSL host.

             @return Type of validation.
             @since V2.0.6
          */
          public IServiceCertificateValidation GetValidationType() {
               return this.ValidationType;
          }

          /**
             Sets the validation type for the certificate of a SSL host.

             @param ValidationType Type of validation.
             @since V2.0.6
          */
          public void SetValidationType(IServiceCertificateValidation ValidationType) {
               this.ValidationType = ValidationType;
          }

          /**
             Returns the Remote service hostURI

             @return Remote service hostURI
             @since V2.0
          */
          public string GetHostURI() {
               return this.HostURI;
          }

          /**
             Set the Remote service hostURI

             @param HostURI Remote service hostURI
             @since V2.0
          */
          public void SetHostURI(string HostURI) {
               this.HostURI = HostURI;
          }

          /**
             Returns the Remote service Paths

             @return Remote service Paths
             @since V2.0
          */
          public ServicePath[] GetPaths() {
               return this.Paths;
          }

          /**
             Set the Remote service Paths

             @param Paths Remote service Paths
             @since V2.0
          */
          public void SetPaths(ServicePath[] Paths) {
               this.Paths = Paths;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
