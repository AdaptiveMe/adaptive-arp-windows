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

    * @version v2.2.0

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Structure representing the personal info data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactPersonalInfo : APIBean
     {

          /**
             The title of the Contact
          */
          public ContactPersonalInfoTitle Title { get; set; }
          /**
             The last name of the Contact
          */
          public string LastName { get; set; }
          /**
             The middle name of the Contact if it proceeds
          */
          public string MiddleName { get; set; }
          /**
             The name of the Contact
          */
          public string Name { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactPersonalInfo()  {
          }

          /**
             The Constructor used by the implementation

             @param Name       of the Contact
             @param MiddleName of the Contact
             @param LastName   of the Contact
             @param Title      of the Contact
             @since V2.0
          */
          public ContactPersonalInfo(string name, string middleName, string lastName, ContactPersonalInfoTitle title) : base () {
               this.Name = Name;
               this.MiddleName = MiddleName;
               this.LastName = LastName;
               this.Title = Title;
          }

          /**
             Returns the title of the Contact

             @return Title
             @since V2.0
          */
          public ContactPersonalInfoTitle GetTitle() {
               return this.Title;
          }

          /**
             Set the Title of the Contact

             @param Title of the Contact
             @since V2.0
          */
          public void SetTitle(ContactPersonalInfoTitle Title) {
               this.Title = Title;
          }

          /**
             Returns the last name of the Contact

             @return LastName
             @since V2.0
          */
          public string GetLastName() {
               return this.LastName;
          }

          /**
             Set the last name of the Contact

             @param LastName of the Contact
             @since V2.0
          */
          public void SetLastName(string LastName) {
               this.LastName = LastName;
          }

          /**
             Returns the middle name of the Contact

             @return MiddelName
             @since V2.0
          */
          public string GetMiddleName() {
               return this.MiddleName;
          }

          /**
             Set the middle name of the Contact

             @param MiddleName of the Contact
             @since V2.0
          */
          public void SetMiddleName(string MiddleName) {
               this.MiddleName = MiddleName;
          }

          /**
             Returns the name of the Contact

             @return Name
             @since V2.0
          */
          public string GetName() {
               return this.Name;
          }

          /**
             Set the name of the Contact

             @param Name of the Contact
             @since V2.0
          */
          public void SetName(string Name) {
               this.Name = Name;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
