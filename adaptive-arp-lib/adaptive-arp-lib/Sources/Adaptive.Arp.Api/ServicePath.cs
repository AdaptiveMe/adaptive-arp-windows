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
        Structure representing a service path for one endpoint

        @author fnva
        @since v2.0.4
        @version 1.0
     */
     public class ServicePath
     {

          /**
             Service endpoint type.
          */
          public IServiceType Type { get; set; }
          /**
             The methods for calling a path.
          */
          public IServiceMethod[] Methods { get; set; }
          /**
             The path for the endpoint.
          */
          public string Path { get; set; }

          /**
             Default Constructor.

             @since V2.0.4
          */
          public ServicePath()  {
          }

          /**
             Constructor with parameters.

             @param Path    The path for the endpoint
             @param Methods The methods for calling a path
             @param Type    Protocol type.
             @since V2.0.6
          */
          public ServicePath(string path, IServiceMethod[] methods, IServiceType type) : base () {
               this.Path = Path;
               this.Methods = Methods;
               this.Type = Type;
          }

          /**
             Gets the protocol for the path.

             @return Type of protocol.
             @since V2.0.6
          */
          public new IServiceType GetType() {
               return this.Type;
          }

          /**
             Sets the protocol for the path.

             @param Type Type of protocol.
             @since V2.0.6
          */
          public void SetType(IServiceType Type) {
               this.Type = Type;
          }

          /**
             Endpoint's path methods setter

             @return Endpoint's path methods
             @since V2.0.4
          */
          public IServiceMethod[] GetMethods() {
               return this.Methods;
          }

          /**
             Endpoint's path methods setter

             @param Methods Endpoint's path methods
             @since V2.0.4
          */
          public void SetMethods(IServiceMethod[] Methods) {
               this.Methods = Methods;
          }

          /**
             Endpoint's Path Getter

             @return Endpoint's Path
             @since V2.0.4
          */
          public string GetPath() {
               return this.Path;
          }

          /**
             Endpoint's path setter

             @param Path Endpoint's path
             @since V2.0.4
          */
          public void SetPath(string Path) {
               this.Path = Path;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
