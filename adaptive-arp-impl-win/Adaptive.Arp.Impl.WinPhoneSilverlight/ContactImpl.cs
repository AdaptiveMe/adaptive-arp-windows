/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * David Barranco Bonilla
 *             <https://github.com/aryslan>
 *             <mailto:ddbc@gft.com>
 *
 * =====================================================================================================================
 */

using Adaptive.Arp.Api;
using Microsoft.Phone.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    //SL ONLY
    public class ContactImpl : IContact
    {
        private const string CALLBACK_ID = "CALLBACK";
        private const string FIELD_ID = "FIELD";
        private const string FILTER_ID = "FILTER";
        private LoggingImpl _Log = new LoggingImpl();
        private string _logCategory = "Contact";

        /// <summary>
        /// Retrieves a given contact from Agenda and returns result to specified callback object
        /// </summary>
        /// <param name="contact">Contact information used to perform the search</param>
        /// <param name="callback">Callback object to use to notify results</param>
        public override void GetContact(ContactUid contact, IContactResultCallback callback)
        {
            try
            {
                if (contact != null && !String.IsNullOrWhiteSpace(contact.GetContactId()))
                {
                    _Log.Log(ILogging.LogLevel.Info, "Method GetContact -- Looking for contact id: " + contact.GetContactId());
                    Contacts agenda = new Contacts();
                    agenda.SearchCompleted += GetContact_SearchCompleted;
                    agenda.SearchAsync(contact.GetContactId(), FilterKind.Identifier, callback);
                }
                else
                {
                    _Log.Log(ILogging.LogLevel.Error, "Method GetContact -- Wrong parameters supplied");
                    callback.OnError(IContactResultCallback.Error.Wrong_Params);
                }
            }
            catch (Exception)
            {
                _Log.Log(ILogging.LogLevel.Error, "Method GetContact -- No permission to access Contacts");
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        /// <summary>
        /// Gets the picture assigned to a Contact and sends result to callback
        /// </summary>
        /// <param name="contact">Contact information used to perform the search</param>
        /// <param name="callback">Callback object to use to notify results</param>
        public override void GetContactPhoto(ContactUid contact, IContactPhotoResultCallback callback)
        {
            try
            {
                if (contact != null && !String.IsNullOrWhiteSpace(contact.GetContactId()))
                {
                    _Log.Log(ILogging.LogLevel.Info, "Method GetContactPhoto -- Looking for contact id: " + contact.GetContactId());
                    Contacts agenda = new Contacts();
                    agenda.SearchCompleted += GetContactPhoto_SearchCompleted;
                    agenda.SearchAsync(contact.GetContactId(), FilterKind.Identifier, callback);
                }
                else
                {
                    _Log.Log(ILogging.LogLevel.Error, "Method GetContactPhoto -- Wrong parameters supplied");
                    callback.OnError(IContactPhotoResultCallback.Error.Wrong_Params);
                }
            }
            catch (Exception ex)
            {
                callback.OnError(IContactPhotoResultCallback.Error.NoPermission);
            }
        }

        /// <summary>
        /// Gets all the contacts from Agenda (only returning specified fields) that matches with specfied filters
        /// </summary>
        /// <param name="callback">Callback object to use to notify results</param>
        /// <param name="fields">Fields to return for each Contact</param>
        /// <param name="filter">Requirements a Contact has to meet</param>
        public override void GetContacts(IContactResultCallback callback, IContact.FieldGroup[] fields, params IContact.Filter[] filter)
        {
            SearchContacts(String.Empty, callback, filter, fields);
        }

        /// <summary>
        /// Gets all the contacts from Agenda (only returning specified fields)
        /// </summary>
        /// <param name="callback">Callback object to use to notify results</param>
        /// <param name="fields">Fields to return for each Contact</param>
        public override void GetContacts(IContactResultCallback callback, params IContact.FieldGroup[] fields)
        {
            SearchContacts(String.Empty, callback, null, fields);
        }

        /// <summary>
        /// Gets all the contacts from Agenda
        /// </summary>
        /// <param name="callback">Callback object to use to notify results</param>
        public override void GetContacts(IContactResultCallback callback)
        {
            SearchContacts(String.Empty, callback, null, null);
        }

        /// <summary>
        /// Returns contacts from Agenda that match a certain search term and comply with specified filters
        /// </summary>
        /// <param name="term">Term to use to perform a Search. Like contact Name, email or phone number</param>
        /// <param name="callback">Callback object to use to notify results</param>
        /// <param name="filter">Requirements a Contact has to meet</param>
        public override void SearchContacts(string term, IContactResultCallback callback, params IContact.Filter[] filter)
        {
            SearchContacts(term, callback, filter, null);
        }

        /// <summary>
        /// Returns contacts from Agenda that match a certain search term
        /// </summary>
        /// <param name="term">Term to use to perform a Search. Like contact Name, email or phone number</param>
        /// <param name="callback">Callback object to use to notify results</param>
        public override void SearchContacts(string term, IContactResultCallback callback)
        {
            SearchContacts(term, callback, null, null);
        }

        /// <summary>
        /// Sets a photo as contact picture in agenda
        /// </summary>
        /// <param name="contact">The contact info to identify the Agenda contact</param>
        /// <param name="pngImage">The image to use in Byte[]</param>
        /// <returns>True if image could be set for the contact, otherwise False</returns>
        public override bool SetContactPhoto(ContactUid contact, byte[] pngImage)
        {
            //Not possible using Silverlight Contacts API
            _Log.Log(ILogging.LogLevel.Warn, "Method SetContactPhoto -- Operation not supported in SilverLight");
            return false;
        }

        #region Private_Methods

        /// <summary>
        /// Converts the native Address list object to Adaptive ContactAddress object
        /// </summary>
        /// <param name="addressList">List of Contact Addresses List</param>
        /// <returns>ContactAddress[] containing the addresses</returns>
        private Adaptive.Arp.Api.ContactAddress[] GetContactAddresses(IEnumerable<Microsoft.Phone.UserData.ContactAddress> addressList)
        {
            if (addressList != null && addressList.Count() > 0)
            {
                List<Adaptive.Arp.Api.ContactAddress> returnContactAddressList = new List<Adaptive.Arp.Api.ContactAddress>();
                foreach (Microsoft.Phone.UserData.ContactAddress contactAddress in addressList)
                {
                    Adaptive.Arp.Api.ContactAddress returnContactAddress = new Adaptive.Arp.Api.ContactAddress();

                    //Create a String with Full Address
                    StringBuilder sb = new StringBuilder();
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.AddressLine1)) { sb.AppendLine("Street:" + contactAddress.PhysicalAddress.AddressLine1); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.AddressLine2)) { sb.AppendLine(contactAddress.PhysicalAddress.AddressLine2); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.Building)) { sb.AppendLine("Building:" + contactAddress.PhysicalAddress.Building); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.City)) { sb.AppendLine("City: " + contactAddress.PhysicalAddress.City); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.PostalCode)) { sb.AppendLine("Postal Code: " + contactAddress.PhysicalAddress.PostalCode); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.CountryRegion)) { sb.AppendLine("Country Region: " + contactAddress.PhysicalAddress.CountryRegion); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.FloorLevel)) { sb.AppendLine("Floor Level: " + contactAddress.PhysicalAddress.FloorLevel); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PhysicalAddress.StateProvince)) { sb.AppendLine("StateProvince: " + contactAddress.PhysicalAddress.StateProvince); }

                    returnContactAddress.SetAddress(sb.ToString());

                    //Set the Kind of address
                    switch (contactAddress.Kind)
                    {
                        case AddressKind.Home:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Home);
                            break;

                        case AddressKind.Other:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Other);
                            break;

                        case AddressKind.Work:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Work);
                            break;
                    }
                    returnContactAddressList.Add(returnContactAddress);
                }
                return returnContactAddressList.ToArray();
            }
            else return null;
        }

        /// <summary>
        /// Converts the native Email Address list object to Adaptive ContactEmail object
        /// </summary>
        /// <param name="emailList">List of Contact Email Addresses List</param>
        /// <returns>ContactEmail[] containing the email addresses</returns>
        private ContactEmail[] GetContactEmailAddresses(IEnumerable<ContactEmailAddress> emailList)
        {
            if (emailList != null && emailList.Count() > 0)
            {
                List<Adaptive.Arp.Api.ContactEmail> returnContactEmailList = new List<Adaptive.Arp.Api.ContactEmail>();
                foreach (ContactEmailAddress emailAccount in emailList)
                {
                    ContactEmail returnContactEmail = new ContactEmail();
                    returnContactEmail.SetEmail(emailAccount.EmailAddress);
                    switch (emailAccount.Kind)
                    {
                        case EmailAddressKind.Personal:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Personal);
                            break;

                        case EmailAddressKind.Work:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Work);
                            break;

                        case EmailAddressKind.Other:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Other);
                            break;

                        default:
                            break;
                    }
                    //NO WAY TO IDENTIFY IF PRIMARY
                    returnContactEmailList.Add(returnContactEmail);
                }
                return returnContactEmailList.ToArray();
            }
            else return null;
        }

        /// <summary>
        /// Returns the Contact native Personal Info object to Adaptive ContactPersonalInfo object
        /// </summary>
        /// <param name="contact">Native Contact object to get the Personal Info</param>
        /// <returns>ContactPersonalInfo object containing personal information</returns>
        private ContactPersonalInfo GetContactPersonalInfo(Microsoft.Phone.UserData.Contact contact)
        {
            ContactPersonalInfo returnInfo = new ContactPersonalInfo();
            if (!String.IsNullOrWhiteSpace(contact.CompleteName.FirstName)) returnInfo.SetName(contact.CompleteName.FirstName);
            if (!String.IsNullOrWhiteSpace(contact.CompleteName.MiddleName)) returnInfo.SetMiddleName(contact.CompleteName.MiddleName);
            if (!String.IsNullOrWhiteSpace(contact.CompleteName.LastName)) returnInfo.SetLastName(contact.CompleteName.LastName);
            return returnInfo;
        }

        /// <summary>
        /// Returns the Contact native Phone number List to Adaptive ContactPhone object
        /// </summary>
        /// <param name="phoneList">List of Native Phone Numbers objects</param>
        /// <returns>ContactPhone[] containing the different contact's phone numbers</returns>
        private ContactPhone[] GetContactPhones(IEnumerable<ContactPhoneNumber> phoneList)
        {
            List<Adaptive.Arp.Api.ContactPhone> returnContactPhoneList = new List<Adaptive.Arp.Api.ContactPhone>();
            if (phoneList != null && phoneList.Count() > 0)
            {
                foreach (ContactPhoneNumber contactPhone in phoneList)
                {
                    ContactPhone returnContactPhone = new ContactPhone();
                    switch (contactPhone.Kind)
                    {
                        case PhoneNumberKind.Home:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Home);
                            break;

                        case PhoneNumberKind.Mobile:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Mobile);
                            break;

                        case PhoneNumberKind.Work:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Work);
                            break;

                        case PhoneNumberKind.HomeFax:
                        case PhoneNumberKind.Pager:
                        case PhoneNumberKind.WorkFax:
                        default:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Other);
                            break;
                    }
                    returnContactPhone.SetPhone(contactPhone.PhoneNumber);
                    returnContactPhoneList.Add(returnContactPhone);
                }
                return returnContactPhoneList.ToArray();
            }
            else return null;
        }

        /// <summary>
        /// Returns a contact professional information in Adaptive Object
        /// </summary>
        /// <param name="contact">Contact to get the professional info</param>
        /// <returns>A contacts professional info in a ContactProfessionalInfo object</returns>
        private ContactProfessionalInfo GetContactProfessionalInfo(Microsoft.Phone.UserData.Contact contact)
        {
            ContactProfessionalInfo returnInfo = new ContactProfessionalInfo();
            if (contact.Companies != null && contact.Companies.Count() > 0)
            {
                var jobInfo = contact.Companies.First();
                if (String.IsNullOrWhiteSpace(jobInfo.CompanyName)) returnInfo.SetCompany(jobInfo.CompanyName);
                if (String.IsNullOrWhiteSpace(jobInfo.JobTitle)) returnInfo.SetJobTitle(jobInfo.JobTitle);
            }
            return returnInfo;
        }

        /// <summary>
        /// Creates a ContactWebsite[] from a contact's website List
        /// </summary>
        /// <param name="websiteList"></param>
        /// <returns>ContactWebsite[] </returns>
        private ContactWebsite[] GetContactWebsites(IEnumerable<string> websiteList)
        {
            if (websiteList != null && websiteList.Count() > 0)
            {
                ContactWebsite[] contactWebsites = (from x in websiteList
                                                    select new ContactWebsite(x)).ToArray();

                return contactWebsites;
            }
            else return null;
        }

        /// <summary>
        /// Filters the specified list of Contacts using the specified filters
        /// </summary>
        /// <param name="contacts">Native Contact object list to parse</param>
        /// <param name="filterList">Requirements a Contact has to meet</param>
        /// <returns></returns>
        private List<Microsoft.Phone.UserData.Contact> GetFilteredContactList(List<Microsoft.Phone.UserData.Contact> contacts, List<Filter> filterList)
        {
            if (filterList != null && filterList.Count > 0)
            {
                if (filterList.Contains(IContact.Filter.HasAddress))
                {
                    contacts = contacts.Where(x => x.Addresses != null).ToList();
                }
                if (filterList.Contains(IContact.Filter.HasEmail))
                {
                    contacts = contacts.Where(x => x.EmailAddresses != null).ToList();
                }
                if (filterList.Contains(IContact.Filter.HasPhone))
                {
                    contacts = contacts.Where(x => x.PhoneNumbers != null).ToList();
                }
            }
            return contacts;
        }

        /// <summary>
        /// Converts a Native list of Contacts to Adaptive Contact
        /// </summary>
        /// <param name="contactList">Native Contact list to parse</param>
        /// <param name="fieldList">Fields to return for each Contact</param>
        /// <returns>Adaptive Contact[]</returns>
        private Adaptive.Arp.Api.Contact[] GetReturnContactArrayFromContactList(List<Microsoft.Phone.UserData.Contact> contactList, List<FieldGroup> fieldList)
        {
            List<Adaptive.Arp.Api.Contact> returnContactList = new List<Adaptive.Arp.Api.Contact>();
            foreach (Microsoft.Phone.UserData.Contact contact in contactList)
            {
                Adaptive.Arp.Api.Contact returnContact = new Adaptive.Arp.Api.Contact(contact.GetHashCode().ToString());
                if (fieldList != null && fieldList.Count > 0)
                {
                    returnContact.SetContactWebsites(GetContactWebsites(contact.Websites));
                    returnContact.SetPersonalInfo(GetContactPersonalInfo(contact));
                    returnContact.SetProfessionalInfo(GetContactProfessionalInfo(contact));
                    returnContact.SetContactEmails(GetContactEmailAddresses(contact.EmailAddresses));
                    returnContact.SetContactAddresses(GetContactAddresses(contact.Addresses));
                    returnContact.SetContactPhones(GetContactPhones(contact.PhoneNumbers));
                }
                else
                {
                    if (fieldList.Contains(IContact.FieldGroup.Websites))
                        returnContact.SetContactWebsites(GetContactWebsites(contact.Websites));
                    if (fieldList.Contains(IContact.FieldGroup.PersonalInfo))
                        returnContact.SetPersonalInfo(GetContactPersonalInfo(contact));
                    if (fieldList.Contains(IContact.FieldGroup.ProfessionalInfo))
                        returnContact.SetProfessionalInfo(GetContactProfessionalInfo(contact));
                    if (fieldList.Contains(IContact.FieldGroup.Emails))
                        returnContact.SetContactEmails(GetContactEmailAddresses(contact.EmailAddresses));
                    if (fieldList.Contains(IContact.FieldGroup.Addresses))
                        returnContact.SetContactAddresses(GetContactAddresses(contact.Addresses));
                    if (fieldList.Contains(IContact.FieldGroup.Phones))
                        returnContact.SetContactPhones(GetContactPhones(contact.PhoneNumbers));
                    //SOCIAL TAGS?
                }
                returnContactList.Add(returnContact);
            }
            return returnContactList.ToArray();
        }

        /// <summary>
        /// Performs a seach to the Agenda to get contacts that match specified search term(if any), meet filter requirements(if any) and return only the specified fields(if any)
        /// </summary>
        /// <param name="term">Term to use to perform a Search. Like contact Name, email or phone number</param>
        /// <param name="callback">Callback object to use to notify results</param>
        /// <param name="filter">Requirements a Contact has to meet</param>
        /// <param name="fields">Fields to return for each Contact</param>
        private void SearchContacts(string term, IContactResultCallback callback, IContact.Filter[] filter, IContact.FieldGroup[] fields)
        {
            try
            {
                _Log.Log(ILogging.LogLevel.Info, "Method SearchContact -- Starting execution");
                Contacts agenda = new Contacts();
                Dictionary<string, object> stateObject = new Dictionary<string, object>();
                if (callback != null) stateObject.Add(CALLBACK_ID, callback);
                if (filter != null && filter.Length > 0) stateObject.Add(FILTER_ID, filter);
                if (fields != null && fields.Length > 0) stateObject.Add(FIELD_ID, fields);
                agenda.SearchCompleted += SearchContacts_SearchCompleted;
                if (String.IsNullOrWhiteSpace(term))
                {
                    _Log.Log(ILogging.LogLevel.Debug, "Method SearchContact -- looking for all the contacts, no search term was passed");
                    agenda.SearchAsync(String.Empty, FilterKind.None, stateObject);
                }
                else
                {
                    _Log.Log(ILogging.LogLevel.Debug, "Method SearchContact -- looking for contacts with search term: " + term);
                    agenda.SearchAsync(term, FilterKind.DisplayName | FilterKind.EmailAddress | FilterKind.PhoneNumber, stateObject);
                }
            }
            catch (Exception)
            {
                _Log.Log(ILogging.LogLevel.Error, "Method SearchContact/s-- No permission to access Contacts");
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        #endregion Private_Methods

        #region Native Callbacks

        private void GetContact_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            IContactResultCallback callback = (IContactResultCallback)e.State;
            try
            {
                List<Microsoft.Phone.UserData.Contact> contactsFoundList = e.Results.ToList();
                List<Adaptive.Arp.Api.Contact> returnContactList = new List<Adaptive.Arp.Api.Contact>();
                if (contactsFoundList != null && contactsFoundList.Count > 0)
                {
                    _Log.Log(ILogging.LogLevel.Info, "Method GetContact -- Contact Found");
                    foreach (Microsoft.Phone.UserData.Contact contactFound in contactsFoundList)
                    {
                        Adaptive.Arp.Api.Contact returnContact = new Adaptive.Arp.Api.Contact();
                        returnContact.SetContactAddresses(GetContactAddresses(contactFound.Addresses));
                        returnContact.SetContactEmails(GetContactEmailAddresses(contactFound.EmailAddresses));
                        returnContact.SetContactPhones(GetContactPhones(contactFound.PhoneNumbers));
                        returnContact.SetPersonalInfo(GetContactPersonalInfo(contactFound));
                        returnContact.SetProfessionalInfo(GetContactProfessionalInfo(contactFound));
                        returnContact.SetContactWebsites(GetContactWebsites(contactFound.Websites));
                        returnContactList.Add(returnContact);
                    }
                    callback.OnResult(returnContactList.ToArray());
                }
                else
                {
                    _Log.Log(ILogging.LogLevel.Warn, "Method GetContact -- No matches found");
                    callback.OnWarning(null, IContactResultCallback.Warning.No_Matches);
                }
            }
            catch (Exception)
            {
                _Log.Log(ILogging.LogLevel.Error, "Method GetContact -- No permission to access Contacts");
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        private void GetContactPhoto_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            IContactPhotoResultCallback callback = (IContactPhotoResultCallback)e.State;
            try
            {
                Microsoft.Phone.UserData.Contact contactFound = e.Results.First();
                using (var photoStream = contactFound.GetPicture())
                {
                    if (photoStream != null && photoStream.Length > 0)
                    {
                        byte[] photoBytes = new byte[photoStream.Length];
                        _Log.Log(ILogging.LogLevel.Info, "Method GetContactPhoto -- Contact Photo found");
                        photoStream.ReadAsync(photoBytes, 0, (int)photoStream.Length).ContinueWith((x) =>
                        {
                            callback.OnResult(photoBytes);
                        });
                    }
                }
            }
            catch (Exception)
            {
                _Log.Log(ILogging.LogLevel.Error, "Method GetContactPhoto -- No permission to access Contacts");
                callback.OnError(IContactPhotoResultCallback.Error.NoPermission);
            }
        }

        private void SearchContacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            Dictionary<string, object> stateObject = (Dictionary<string, object>)e.State;
            IContactResultCallback callback = (stateObject.ContainsKey(CALLBACK_ID)) ? (IContactResultCallback)stateObject[CALLBACK_ID] : null;
            try
            {
                List<IContact.Filter> filter = (stateObject.ContainsKey(FILTER_ID)) ? ((IContact.Filter[])stateObject[FILTER_ID]).ToList() : null;
                List<IContact.FieldGroup> fields = (stateObject.ContainsKey(FIELD_ID)) ? ((IContact.FieldGroup[])stateObject[FIELD_ID]).ToList() : null;
                List<Microsoft.Phone.UserData.Contact> contacts = e.Results.ToList();
                List<Microsoft.Phone.UserData.Contact> filteredContacts = GetFilteredContactList(contacts, filter);
                Adaptive.Arp.Api.Contact[] returnContactArray = GetReturnContactArrayFromContactList(filteredContacts, fields);
                callback.OnResult(returnContactArray);
            }
            catch (Exception ex)
            {
                _Log.Log(ILogging.LogLevel.Error, "Method SearchContact/s-- No permission to access Contacts");
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        #endregion Native Callbacks
    }
}