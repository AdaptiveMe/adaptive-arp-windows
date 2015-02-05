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
        Structure representing the cookieValue of a http cookie.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class ServiceSessionCookie : APIBean
     {

          /**
             Name ot the cookie.
          */
          public string CookieName { get; set; }
          /**
             Value of the ServiceCookie.
          */
          public string CookieValue { get; set; }
          /**
             ServiceCookie creation timestamp in milliseconds.
          */
          public long Creation { get; set; }
          /**
             Domain for which the cookie is valid.
          */
          public string Domain { get; set; }
          /**
             ServiceCookie expiry in milliseconds or -1 for session only.
          */
          public long Expiry { get; set; }
          /**
             URI path for which the cookie is valid.
          */
          public string Path { get; set; }
          /**
             Scheme of the domain - http/https - for which the cookie is valid.
          */
          public string Scheme { get; set; }
          /**
             ServiceCookie is secure (https only).
          */
          public bool Secure { get; set; }

          /**
             Default constructor.

             @since V2.0
          */
          public ServiceSessionCookie()  {
          }

          /**
             Constructor used by the implementation

             @param CookieName  Name of the cookie
             @param CookieValue Value of the cookie
             @since V2.0
          */
          public ServiceSessionCookie(string cookieName, string cookieValue) : base () {
               this.CookieName = CookieName;
               this.CookieValue = CookieValue;
          }

          /**
             Contructor with fields

             @param CookieName  Name of the cookie
             @param CookieValue Value of the cookie
             @param Domain      Domain of the cookie
             @param Path        Path of the cookie
             @param Scheme      Scheme of the cookie
             @param Secure      Privacy of the cookie
             @param Expiry      Expiration date of the cookie
             @param Creation    Creation date of the cookie
             @since V2.0
          */
          public ServiceSessionCookie(string cookieName, string cookieValue, string domain, string path, string scheme, bool secure, long expiry, long creation) : base () {
               this.CookieName = CookieName;
               this.CookieValue = CookieValue;
               this.Domain = Domain;
               this.Path = Path;
               this.Scheme = Scheme;
               this.Secure = Secure;
               this.Expiry = Expiry;
               this.Creation = Creation;
          }

          /**
             Returns the cookie cookieName

             @return CookieName Name of the cookie
             @since V2.0
          */
          public string GetCookieName() {
               return this.CookieName;
          }

          /**
             Set the cookie cookieName

             @param CookieName Name of the cookie
             @since V2.0
          */
          public void SetCookieName(string CookieName) {
               this.CookieName = CookieName;
          }

          /**
             Returns the cookie cookieValue

             @return Value of the cookie
             @since V2.0
          */
          public string GetCookieValue() {
               return this.CookieValue;
          }

          /**
             Set the cookie cookieValue

             @param CookieValue Value of the cookie
             @since V2.0
          */
          public void SetCookieValue(string CookieValue) {
               this.CookieValue = CookieValue;
          }

          /**
             Returns the creation date

             @return Creation date of the cookie
             @since V2.0
          */
          public long GetCreation() {
               return this.Creation;
          }

          /**
             Sets the creation date

             @param Creation Creation date of the cookie
             @since V2.0
          */
          public void SetCreation(long Creation) {
               this.Creation = Creation;
          }

          /**
             Returns the domain

             @return Domain
             @since V2.0
          */
          public string GetDomain() {
               return this.Domain;
          }

          /**
             Set the domain

             @param Domain Domain of the cookie
             @since V2.0
          */
          public void SetDomain(string Domain) {
               this.Domain = Domain;
          }

          /**
             Returns the expiration date in milis

             @return Expiry
             @since V2.0
          */
          public long GetExpiry() {
               return this.Expiry;
          }

          /**
             Set the expiration date in milis

             @param Expiry Expiration date of the cookie
             @since V2.0
          */
          public void SetExpiry(long Expiry) {
               this.Expiry = Expiry;
          }

          /**
             Returns the path

             @return Path
             @since V2.0
          */
          public string GetPath() {
               return this.Path;
          }

          /**
             Set the path

             @param Path Path of the cookie
             @since V2.0
          */
          public void SetPath(string Path) {
               this.Path = Path;
          }

          /**
             Returns the scheme

             @return Scheme
             @since V2.0
          */
          public string GetScheme() {
               return this.Scheme;
          }

          /**
             Set the scheme

             @param Scheme Scheme of the cookie
             @since V2.0
          */
          public void SetScheme(string Scheme) {
               this.Scheme = Scheme;
          }

          /**
             Returns whether the cookie is secure or not

             @return True if the cookie is secure; false otherwise
             @since V2.0
          */
          public bool GetSecure() {
               return this.Secure;
          }

          /**
             Set whether the cookie is secure or not

             @param Secure Privacy of the cookie
             @since V2.0
          */
          public void SetSecure(bool Secure) {
               this.Secure = Secure;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
