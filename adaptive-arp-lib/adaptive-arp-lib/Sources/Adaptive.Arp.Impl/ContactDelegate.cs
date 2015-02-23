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
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Api.Impl
{

     /**
        Interface for Managing the Contact operations
        Auto-generated implementation of IContact specification.
     */
     public class ContactDelegate : BasePIMDelegate, IContact
     {

          /**
             Default Constructor.
          */
          public ContactDelegate() : base()
          {
          }

          /**
             Get all the details of a contact according to its id

             @param contact  id to search for
             @param callback called for return
             @since v2.0
          */
          public void GetContact(ContactUid contact, IContactResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:getContact");
          }

          /**
             Get the contact photo

             @param contact  id to search for
             @param callback called for return
             @since v2.0
          */
          public void GetContactPhoto(ContactUid contact, IContactPhotoResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:getContactPhoto");
          }

          /**
             Get all contacts

             @param callback called for return
             @since v2.0
          */
          public void GetContacts(IContactResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:getContacts");
          }

          /**
             Get marked fields of all contacts

             @param callback called for return
             @param fields   to get for each Contact
             @since v2.0
          */
          public void GetContactsForFields(IContactResultCallback callback, IContactFieldGroup[] fields) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:getContactsForFields");
          }

          /**
             Get marked fields of all contacts according to a filter

             @param callback called for return
             @param fields   to get for each Contact
             @param filter   to search for
             @since v2.0
          */
          public void GetContactsWithFilter(IContactResultCallback callback, IContactFieldGroup[] fields, IContactFilter[] filter) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:getContactsWithFilter");
          }

          /**
             Search contacts according to a term and send it to the callback

             @param term     string to search
             @param callback called for return
             @since v2.0
          */
          public void SearchContacts(string term, IContactResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:searchContacts");
          }

          /**
             Search contacts according to a term with a filter and send it to the callback

             @param term     string to search
             @param callback called for return
             @param filter   to search for
             @since v2.0
          */
          public void SearchContactsWithFilter(string term, IContactResultCallback callback, IContactFilter[] filter) {
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:searchContactsWithFilter");
          }

          /**
             Set the contact photo

             @param contact  id to assign the photo
             @param pngImage photo as byte array
             @return true if set is successful;false otherwise
             @since v2.0
          */
          public bool SetContactPhoto(ContactUid contact, byte[] pngImage) {
               // bool response;
               // TODO: Not implemented.
               throw new NotSupportedException("ContactDelegate:setContactPhoto");
               // return response;
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
