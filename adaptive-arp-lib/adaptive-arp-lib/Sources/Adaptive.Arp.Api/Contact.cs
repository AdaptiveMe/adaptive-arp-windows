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

    * @version v2.1.8

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Structure representing the data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class Contact : ContactUid
     {

          /**
             The adresses from the contact
          */
          public ContactAddress[] ContactAddresses { get; set; }
          /**
             The emails from the contact
          */
          public ContactEmail[] ContactEmails { get; set; }
          /**
             The phones from the contact
          */
          public ContactPhone[] ContactPhones { get; set; }
          /**
             The social network info from the contact
          */
          public ContactSocial[] ContactSocials { get; set; }
          /**
             The aditional tags from the contact
          */
          public ContactTag[] ContactTags { get; set; }
          /**
             The websites from the contact
          */
          public ContactWebsite[] ContactWebsites { get; set; }
          /**
             The personal info from the contact
          */
          public ContactPersonalInfo PersonalInfo { get; set; }
          /**
             The professional info from the contact
          */
          public ContactProfessionalInfo ProfessionalInfo { get; set; }

          /**
             Default Constructor

             @since V2.0
          */
          public Contact()  {
          }

          /**
             Constructor used by implementation to set the Contact.

             @param ContactId of the Contact
             @since V2.0
          */
          public Contact(string contactId) : base(contactId) {
          }

          /**
             Returns all the addresses of the Contact

             @return ContactAddress[]
             @since V2.0
          */
          public ContactAddress[] GetContactAddresses() {
               return this.ContactAddresses;
          }

          /**
             Set the addresses of the Contact

             @param ContactAddresses Addresses of the contact
             @since V2.0
          */
          public void SetContactAddresses(ContactAddress[] ContactAddresses) {
               this.ContactAddresses = ContactAddresses;
          }

          /**
             Returns all the emails of the Contact

             @return ContactEmail[]
             @since V2.0
          */
          public ContactEmail[] GetContactEmails() {
               return this.ContactEmails;
          }

          /**
             Set the emails of the Contact

             @param ContactEmails Emails of the contact
             @since V2.0
          */
          public void SetContactEmails(ContactEmail[] ContactEmails) {
               this.ContactEmails = ContactEmails;
          }

          /**
             Returns all the phones of the Contact

             @return ContactPhone[]
             @since V2.0
          */
          public ContactPhone[] GetContactPhones() {
               return this.ContactPhones;
          }

          /**
             Set the phones of the Contact

             @param ContactPhones Phones of the contact
             @since V2.0
          */
          public void SetContactPhones(ContactPhone[] ContactPhones) {
               this.ContactPhones = ContactPhones;
          }

          /**
             Returns all the social network info of the Contact

             @return ContactSocial[]
             @since V2.0
          */
          public ContactSocial[] GetContactSocials() {
               return this.ContactSocials;
          }

          /**
             Set the social network info of the Contact

             @param ContactSocials Social Networks of the contact
             @since V2.0
          */
          public void SetContactSocials(ContactSocial[] ContactSocials) {
               this.ContactSocials = ContactSocials;
          }

          /**
             Returns the additional tags of the Contact

             @return ContactTag[]
             @since V2.0
          */
          public ContactTag[] GetContactTags() {
               return this.ContactTags;
          }

          /**
             Set the additional tags of the Contact

             @param ContactTags Tags of the contact
             @since V2.0
          */
          public void SetContactTags(ContactTag[] ContactTags) {
               this.ContactTags = ContactTags;
          }

          /**
             Returns all the websites of the Contact

             @return ContactWebsite[]
             @since V2.0
          */
          public ContactWebsite[] GetContactWebsites() {
               return this.ContactWebsites;
          }

          /**
             Set the websites of the Contact

             @param ContactWebsites Websites of the contact
             @since V2.0
          */
          public void SetContactWebsites(ContactWebsite[] ContactWebsites) {
               this.ContactWebsites = ContactWebsites;
          }

          /**
             Returns the personal info of the Contact

             @return ContactPersonalInfo of the Contact
             @since V2.0
          */
          public ContactPersonalInfo GetPersonalInfo() {
               return this.PersonalInfo;
          }

          /**
             Set the personal info of the Contact

             @param PersonalInfo Personal Information
             @since V2.0
          */
          public void SetPersonalInfo(ContactPersonalInfo PersonalInfo) {
               this.PersonalInfo = PersonalInfo;
          }

          /**
             Returns the professional info of the Contact

             @return Array of personal info
             @since V2.0
          */
          public ContactProfessionalInfo GetProfessionalInfo() {
               return this.ProfessionalInfo;
          }

          /**
             Set the professional info of the Contact

             @param ProfessionalInfo Professional Information
             @since V2.0
          */
          public void SetProfessionalInfo(ContactProfessionalInfo ProfessionalInfo) {
               this.ProfessionalInfo = ProfessionalInfo;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
