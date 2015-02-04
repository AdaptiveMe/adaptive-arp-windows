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

    * @version v2.1.4

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Structure representing the phone data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactPhone : APIBean
     {

          /**
             The phone number phoneType
          */
          public ContactPhoneType PhoneType { get; set; }
          /**
             The phone number
          */
          public string Phone { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactPhone()  {
          }

          /**
             Constructor used by implementation to set the contact Phone

             @param Phone     Phone number
             @param PhoneType Type of Phone number
             @since V2.0
          */
          public ContactPhone(string phone, ContactPhoneType phoneType) : base () {
               this.Phone = Phone;
               this.PhoneType = PhoneType;
          }

          /**
             Returns the phone phoneType

             @return PhoneType
             @since V2.0
          */
          public ContactPhoneType GetPhoneType() {
               return this.PhoneType;
          }

          /**
             Set the phoneType of the phone number

             @param PhoneType Type of Phone number
             @since V2.0
          */
          public void SetPhoneType(ContactPhoneType PhoneType) {
               this.PhoneType = PhoneType;
          }

          /**
             Returns the phone number

             @return Phone number
             @since V2.0
          */
          public string GetPhone() {
               return this.Phone;
          }

          /**
             Set the phone number

             @param Phone number
             @since V2.0
          */
          public void SetPhone(string Phone) {
               this.Phone = Phone;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
