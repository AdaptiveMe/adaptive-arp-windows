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
        Structure representing the internal unique identifier data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactUid : APIBean
     {

          /**
             The id of the Contact
          */
          public string ContactId { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactUid()  {
          }

          /**
             Constructor used by implementation to set the Contact id.

             @param ContactId Internal unique contact id.
             @since V2.0
          */
          public ContactUid(string contactId) : base () {
               this.ContactId = ContactId;
          }

          /**
             Returns the contact id

             @return Contactid Internal unique contact id.
             @since V2.0
          */
          public string GetContactId() {
               return this.ContactId;
          }

          /**
             Set the id of the Contact

             @param ContactId Internal unique contact id.
             @since V2.0
          */
          public void SetContactId(string ContactId) {
               this.ContactId = ContactId;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
