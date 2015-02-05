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
        Represents a basic bean to store keyName pair values

        @author Ferran Vila Conesa
        @since v2.0
        @version 1.0
     */
     public class KeyPair : APIBean
     {

          /**
             Key of the element
          */
          public string KeyName { get; set; }
          /**
             Value of the element
          */
          public string KeyValue { get; set; }

          /**
             Default Constructor

             @since V2.0
          */
          public KeyPair()  {
          }

          /**
             Constructor using fields

             @param KeyName  Key of the element
             @param KeyValue Value of the element
             @since V2.0
          */
          public KeyPair(string keyName, string keyValue) : base () {
               this.KeyName = KeyName;
               this.KeyValue = KeyValue;
          }

          /**
             Returns the keyName of the element

             @return Key of the element
             @since V2.0
          */
          public string GetKeyName() {
               return this.KeyName;
          }

          /**
             Sets the keyName of the element

             @param KeyName Key of the element
             @since V2.0
          */
          public void SetKeyName(string KeyName) {
               this.KeyName = KeyName;
          }

          /**
             Returns the keyValue of the element

             @return Value of the element
             @since V2.0
          */
          public string GetKeyValue() {
               return this.KeyValue;
          }

          /**
             Sets the keyValue of the element

             @param KeyValue Value of the element
             @since V2.0
          */
          public void SetKeyValue(string KeyValue) {
               this.KeyValue = KeyValue;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
