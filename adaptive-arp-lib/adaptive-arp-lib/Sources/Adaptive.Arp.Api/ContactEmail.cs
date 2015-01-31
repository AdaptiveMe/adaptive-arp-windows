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
        Structure representing the email data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactEmail : APIBean
     {

          /**
             The type of the email
          */
          public ContactEmailType Type { get; set; }
          /**
             Email of the Contact
          */
          public string Email { get; set; }
          /**
             Whether the email is the primary one or not
          */
          public bool Primary { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactEmail()  {
          }

          /**
             Constructor used by the implementation

             @param Type    Type of the email
             @param Primary Is email primary
             @param Email   Email of the contact
             @since V2.0
          */
          public ContactEmail(ContactEmailType type, bool primary, string email) : base () {
               this.Type = Type;
               this.Primary = Primary;
               this.Email = Email;
          }

          /**
             Returns the type of the email

             @return EmailType
             @since V2.0
          */
          public new ContactEmailType GetType() {
               return this.Type;
          }

          /**
             Set the type of the email

             @param Type Type of the email
             @since V2.0
          */
          public void SetType(ContactEmailType Type) {
               this.Type = Type;
          }

          /**
             Returns the email of the Contact

             @return Email
             @since V2.0
          */
          public string GetEmail() {
               return this.Email;
          }

          /**
             Set the email of the Contact

             @param Email Email of the contact
             @since V2.0
          */
          public void SetEmail(string Email) {
               this.Email = Email;
          }

          /**
             Returns if the email is primary

             @return True if the email is primary; false otherwise
             @since V2.0
          */
          public bool GetPrimary() {
               return this.Primary;
          }

          /**
             Set if the email

             @param Primary true if the email is primary; false otherwise
             @since V2.0
          */
          public void SetPrimary(bool Primary) {
               this.Primary = Primary;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
