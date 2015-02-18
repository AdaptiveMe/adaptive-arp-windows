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

    * @version v2.1.9

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a single secureKey-value pair.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class SecureKeyPair : APIBean
     {

          /**
             Value of the secured element
          */
          public string SecureData { get; set; }
          /**
             Key of the secured element
          */
          public string SecureKey { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public SecureKeyPair()  {
          }

          /**
             Constructor with parameters

             @param SecureKey  name of the keypair
             @param SecureData value of the keypair
             @since V2.0
          */
          public SecureKeyPair(string secureKey, string secureData) : base () {
               this.SecureKey = SecureKey;
               this.SecureData = SecureData;
          }

          /**
             Returns the object value

             @return Value.
             @since V2.0
          */
          public string GetSecureData() {
               return this.SecureData;
          }

          /**
             Sets the value for this object

             @param SecureData value to set.
             @since V2.0
          */
          public void SetSecureData(string SecureData) {
               this.SecureData = SecureData;
          }

          /**
             Returns the object secureKey name.

             @return Key name.
             @since V2.0
          */
          public string GetSecureKey() {
               return this.SecureKey;
          }

          /**
             Sets the secureKey name for this object.

             @param SecureKey Key name.
             @since V2.0
          */
          public void SetSecureKey(string SecureKey) {
               this.SecureKey = SecureKey;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
