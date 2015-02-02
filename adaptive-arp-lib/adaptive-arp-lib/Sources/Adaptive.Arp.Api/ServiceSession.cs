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

    * @version v2.1.3

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a session object for HTTP request and responses

        @author Ferran Vila Conesa
        @since v2.0
        @version 1.0
     */
     public class ServiceSession : APIBean
     {

          /**
             The attributes of the request or response.
          */
          public ServiceSessionAttribute[] Attributes { get; set; }
          /**
             The cookies of the request or response.
          */
          public ServiceSessionCookie[] Cookies { get; set; }

          /**
             Default constructor.

             @since V2.0
          */
          public ServiceSession()  {
          }

          /**
             Constructor with fields.

             @param Cookies    The cookies of the request or response.
             @param Attributes Attributes of the request or response.
             @since V2.0
          */
          public ServiceSession(ServiceSessionCookie[] cookies, ServiceSessionAttribute[] attributes) : base () {
               this.Cookies = Cookies;
               this.Attributes = Attributes;
          }

          /**
             Gets the attributes of the request or response.

             @return Attributes of the request or response.
             @since V2.0
          */
          public ServiceSessionAttribute[] GetAttributes() {
               return this.Attributes;
          }

          /**
             Sets the attributes for the request or response.

             @param Attributes Attributes of the request or response.
             @since V2.0
          */
          public void SetAttributes(ServiceSessionAttribute[] Attributes) {
               this.Attributes = Attributes;
          }

          /**
             Returns the cookies of the request or response.

             @return The cookies of the request or response.
             @since V2.0
          */
          public ServiceSessionCookie[] GetCookies() {
               return this.Cookies;
          }

          /**
             Sets the cookies of the request or response.

             @param Cookies The cookies of the request or response.
             @since V2.0
          */
          public void SetCookies(ServiceSessionCookie[] Cookies) {
               this.Cookies = Cookies;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
