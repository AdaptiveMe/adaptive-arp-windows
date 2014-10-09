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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Contacts;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class ContactImpl : IContact
    {
        public override async void GetContact(ContactUid contact, IContactResultCallback callback)
        {
            try
            {
                if (contact != null && !String.IsNullOrWhiteSpace(contact.GetContactId()))
                {
                    ContactStore agenda = await ContactManager.RequestStoreAsync();
                    Windows.ApplicationModel.Contacts.Contact foundContact = await agenda.GetContactAsync(contact.GetContactId());
                    if (foundContact != null)
                    {
                        FilterAndSendContactList(new List<Windows.ApplicationModel.Contacts.Contact>() { foundContact }, null, null, callback);
                    }
                    else
                    {
                        callback.OnWarning(null, IContactResultCallback.Warning.No_Matches);
                    }
                }
                else
                {
                    callback.OnError(IContactResultCallback.Error.Wrong_Params);
                }
            }
            catch (Exception)
            {
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        public override async void GetContactPhoto(ContactUid contact, IContactPhotoResultCallback callback)
        {
            try
            {
                string sContactID = contact.GetContactId();
                if (!String.IsNullOrWhiteSpace(sContactID))
                {
                    ContactStore agenda = await ContactManager.RequestStoreAsync();
                    Windows.ApplicationModel.Contacts.Contact foundContact = await agenda.GetContactAsync(sContactID);
                    if (foundContact != null)
                    {
                        if (foundContact.Thumbnail != null)
                        {
                            byte[] picBytes = null;
                            using (var picStream = await foundContact.Thumbnail.OpenReadAsync())
                            {
                                Windows.Storage.Streams.Buffer buff = new Windows.Storage.Streams.Buffer((uint)picStream.Size);
                                await picStream.ReadAsync(buff, (uint)picStream.Size, Windows.Storage.Streams.InputStreamOptions.None);
                                picBytes = buff.ToArray();
                            }
                            if (picBytes != null && picBytes.Length > 0)
                            {
                                callback.OnResult(picBytes);
                            }
                            else
                            {
                                callback.OnError(IContactPhotoResultCallback.Error.NoPermission);
                            }
                        }
                        else
                        {
                            callback.OnError(IContactPhotoResultCallback.Error.No_Photo);
                        }
                    }
                    else
                    {
                        callback.OnWarning(null, IContactPhotoResultCallback.Warning.No_Matches);
                    }
                }
                else
                {
                    callback.OnError(IContactPhotoResultCallback.Error.Wrong_Params);
                }
            }
            catch (Exception)
            {
                callback.OnError(IContactPhotoResultCallback.Error.NoPermission);
            }
        }

        public override void GetContacts(IContactResultCallback callback, IContact.FieldGroup[] fields, params IContact.Filter[] filter)
        {
            SearchContacts(String.Empty, callback, filter, fields);
        }

        public override void GetContacts(IContactResultCallback callback, params IContact.FieldGroup[] fields)
        {
            SearchContacts(String.Empty, callback, null, fields);
        }

        public override void GetContacts(IContactResultCallback callback)
        {
            SearchContacts(String.Empty, callback, null, null);
        }

        public override void SearchContacts(string term, IContactResultCallback callback, params IContact.Filter[] filter)
        {
            SearchContacts(term, callback, filter, null);
        }

        public override void SearchContacts(string term, IContactResultCallback callback)
        {
            SearchContacts(term, callback, null, null);
        }

        public override bool SetContactPhoto(ContactUid contact, byte[] pngImage)
        {
            //Not possible to modify the picture of an existing contact, only works for new contacts
            return false;
        }

        #region Private_Methods

        private void ApplyFiltersToContactList(ref List<Windows.ApplicationModel.Contacts.Contact> contactList, Filter[] filter)
        {
            if (filter != null && filter.Length > 0)
            {
                List<Filter> filterList = filter.ToList();
                if (filterList.Contains(Filter.HasAddress))
                {
                    contactList = contactList.Where(singleContact => singleContact.Addresses != null && singleContact.Addresses.Count > 0).ToList();
                }
                if (filterList.Contains(Filter.HasEmail))
                {
                    contactList = contactList.Where(singleContact => singleContact.Emails != null && singleContact.Emails.Count > 0).ToList();
                }
                if (filterList.Contains(Filter.HasPhone))
                {
                    contactList = contactList.Where(singleContact => singleContact.Phones != null && singleContact.Phones.Count > 0).ToList();
                }
            }
        }

        private void FilterAndSendContactList(List<Windows.ApplicationModel.Contacts.Contact> contactList, Filter[] filter, FieldGroup[] fields, IContactResultCallback callback)
        {
            try
            {
                if (contactList != null && contactList.Count > 0)
                {
                    ApplyFiltersToContactList(ref contactList, filter);
                    Adaptive.Arp.Api.Contact[] returnContactArray = GetReturnContactArrayFromContactList(contactList, fields);
                    callback.OnResult(returnContactArray);
                }
                else
                {
                    callback.OnWarning(null, IContactResultCallback.Warning.No_Matches);
                }
            }
            catch (Exception Ex)
            {
                //_Log.Log(ILogging.LogLevel.Error, "Method SearchContact/s-- No permission to access Contacts");
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        /// <summary>
        /// Converts the native Address list object to Adaptive ContactAddress object
        /// </summary>
        /// <param name="addressList">List of Contact Addresses List</param>
        /// <returns>ContactAddress[] containing the addresses</returns>
        private Api.ContactAddress[] GetContactAddresses(IList<Windows.ApplicationModel.Contacts.ContactAddress> addressList)
        {
            if (addressList != null && addressList.Count > 0)
            {
                List<Adaptive.Arp.Api.ContactAddress> returnContactAddressList = new List<Api.ContactAddress>();
                foreach (Windows.ApplicationModel.Contacts.ContactAddress contactAddress in addressList)
                {
                    Adaptive.Arp.Api.ContactAddress returnContactAddress = new Api.ContactAddress();

                    //Create a String with Full Address
                    StringBuilder sb = new StringBuilder();
                    if (!String.IsNullOrWhiteSpace(contactAddress.StreetAddress)) { sb.AppendLine("StreetAddress: " + contactAddress.StreetAddress); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.Locality)) { sb.AppendLine("Locality: " + contactAddress.Locality); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.Region)) { sb.AppendLine("Region: " + contactAddress.Region); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.PostalCode)) { sb.AppendLine("Postal Code: " + contactAddress.PostalCode); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.Country)) { sb.AppendLine("Country: " + contactAddress.Country); }
                    if (!String.IsNullOrWhiteSpace(contactAddress.Description)) { sb.AppendLine("Description: " + contactAddress.Description); }

                    returnContactAddress.SetAddress(sb.ToString());

                    //Set the Kind of address
                    switch (contactAddress.Kind)
                    {
                        case ContactAddressKind.Home:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Home);
                            break;

                        case ContactAddressKind.Work:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Work);
                            break;

                        case ContactAddressKind.Other:
                        default:
                            returnContactAddress.SetType(Adaptive.Arp.Api.ContactAddress.AddressType.Other);
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
        private Api.ContactEmail[] GetContactEmailAddresses(IList<Windows.ApplicationModel.Contacts.ContactEmail> emailList)
        {
            if (emailList != null && emailList.Count > 0)
            {
                List<Adaptive.Arp.Api.ContactEmail> returnContactEmailList = new List<Api.ContactEmail>();
                foreach (Windows.ApplicationModel.Contacts.ContactEmail emailAccount in emailList)
                {
                    Adaptive.Arp.Api.ContactEmail returnContactEmail = new Api.ContactEmail();
                    returnContactEmail.SetEmail(emailAccount.Address);
                    switch (emailAccount.Kind)
                    {
                        case ContactEmailKind.Personal:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Personal);
                            break;

                        case ContactEmailKind.Work:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Work);
                            break;

                        case ContactEmailKind.Other:
                        default:
                            returnContactEmail.SetType(Adaptive.Arp.Api.ContactEmail.EmailType.Other);
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
        private ContactPersonalInfo GetContactPersonalInfo(Windows.ApplicationModel.Contacts.Contact contact)
        {
            ContactPersonalInfo returnInfo = new ContactPersonalInfo();
            if (!String.IsNullOrWhiteSpace(contact.FirstName)) returnInfo.SetName(contact.FirstName);
            if (!String.IsNullOrWhiteSpace(contact.MiddleName)) returnInfo.SetMiddleName(contact.MiddleName);
            if (!String.IsNullOrWhiteSpace(contact.LastName)) returnInfo.SetLastName(contact.LastName);
            return returnInfo;
        }

        /// <summary>
        /// Returns the Contact native Phone number List to Adaptive ContactPhone object
        /// </summary>
        /// <param name="phoneList">List of Native Phone Numbers objects</param>
        /// <returns>ContactPhone[] containing the different contact's phone numbers</returns>
        private Api.ContactPhone[] GetContactPhones(IList<Windows.ApplicationModel.Contacts.ContactPhone> phoneList)
        {
            List<Adaptive.Arp.Api.ContactPhone> returnContactPhoneList = new List<Api.ContactPhone>();
            if (phoneList != null && phoneList.Count > 0)
            {
                foreach (Windows.ApplicationModel.Contacts.ContactPhone contactPhone in phoneList)
                {
                    Adaptive.Arp.Api.ContactPhone returnContactPhone = new Api.ContactPhone();
                    switch (contactPhone.Kind)
                    {
                        case ContactPhoneKind.Home:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Home);
                            break;

                        case ContactPhoneKind.Mobile:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Mobile);
                            break;

                        case ContactPhoneKind.Work:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Work);
                            break;

                        case ContactPhoneKind.Other:
                        default:
                            returnContactPhone.SetPhoneType(Adaptive.Arp.Api.ContactPhone.PhoneType.Other);
                            break;
                    }
                    returnContactPhone.SetPhone(contactPhone.Number);
                    returnContactPhoneList.Add(returnContactPhone);
                }
                return returnContactPhoneList.ToArray();
            }
            else return null;
        }

        /// <summary>
        /// Returns a contact professional information in Adaptive Object
        /// </summary>
        /// <param name="jobInfoList">List of contact's job information</param>
        /// <returns>A contacts professional info in a ContactProfessionalInfo object</returns>
        private ContactProfessionalInfo GetContactProfessionalInfo(IList<ContactJobInfo> jobInfoList)
        {
            ContactProfessionalInfo returnInfo = new ContactProfessionalInfo();
            if (jobInfoList != null && jobInfoList.Count > 0)
            {
                var jobInfo = jobInfoList[0];
                if (!String.IsNullOrWhiteSpace(jobInfo.CompanyName)) returnInfo.SetCompany(jobInfo.CompanyName);
                if (!String.IsNullOrWhiteSpace(jobInfo.Description)) returnInfo.SetJobDescription(jobInfo.Description);
                if (!String.IsNullOrWhiteSpace(jobInfo.Title)) returnInfo.SetJobTitle(jobInfo.Title);
            }
            return returnInfo;
        }

        /// <summary>
        /// Creates a ContactWebsite[] from a contact's website List
        /// </summary>
        /// <param name="websiteList">List of conctact's websites</param>
        /// <returns>ContactWebsite[] </returns>
        private Api.ContactWebsite[] GetContactWebSites(IList<Windows.ApplicationModel.Contacts.ContactWebsite> websiteList)
        {
            if (websiteList != null && websiteList.Count > 0)
            {
                Adaptive.Arp.Api.ContactWebsite[] contactWebsites = (from x in websiteList
                                                                     select new Adaptive.Arp.Api.ContactWebsite(x.Uri.ToString())).ToArray();
                return contactWebsites;
            }
            else return null;
        }

        /// <summary>
        /// Converts a Native list of Contacts to Adaptive Contact
        /// </summary>
        /// <param name="contactList">Native Contact list to parse</param>
        /// <param name="fieldList">Fields to return for each Contact</param>
        /// <returns>Adaptive Contact[]</returns>
        private Api.Contact[] GetReturnContactArrayFromContactList(List<Windows.ApplicationModel.Contacts.Contact> contactList, FieldGroup[] fields)
        {
            List<Adaptive.Arp.Api.Contact> returnContactList = new List<Api.Contact>();
            List<FieldGroup> fieldList = (fields != null && fields.Length > 0) ? new List<FieldGroup>() : null;
            foreach (Windows.ApplicationModel.Contacts.Contact contact in contactList)
            {
                Adaptive.Arp.Api.Contact returnContact = new Api.Contact(contact.Id);
                if (fieldList != null && fieldList.Count > 0)
                {
                    if (fieldList.Contains(IContact.FieldGroup.Websites))
                        returnContact.SetContactWebsites(GetContactWebSites(contact.Websites));
                    if (fieldList.Contains(IContact.FieldGroup.PersonalInfo))
                        returnContact.SetPersonalInfo(GetContactPersonalInfo(contact));
                    if (fieldList.Contains(IContact.FieldGroup.ProfessionalInfo))
                        returnContact.SetProfessionalInfo(GetContactProfessionalInfo(contact.JobInfo));
                    if (fieldList.Contains(IContact.FieldGroup.Emails))
                        returnContact.SetContactEmails(GetContactEmailAddresses(contact.Emails));
                    if (fieldList.Contains(IContact.FieldGroup.Addresses))
                        returnContact.SetContactAddresses(GetContactAddresses(contact.Addresses));
                    if (fieldList.Contains(IContact.FieldGroup.Phones))
                        returnContact.SetContactPhones(GetContactPhones(contact.Phones));
                }
                else
                {
                    returnContact.SetContactWebsites(GetContactWebSites(contact.Websites));
                    returnContact.SetPersonalInfo(GetContactPersonalInfo(contact));
                    returnContact.SetProfessionalInfo(GetContactProfessionalInfo(contact.JobInfo));
                    returnContact.SetContactEmails(GetContactEmailAddresses(contact.Emails));
                    returnContact.SetContactAddresses(GetContactAddresses(contact.Addresses));
                    returnContact.SetContactPhones(GetContactPhones(contact.Phones));
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
        private async void SearchContacts(string term, IContactResultCallback callback, IContact.Filter[] filter, IContact.FieldGroup[] fields)
        {
            try
            {
                ContactStore agenda = await ContactManager.RequestStoreAsync();
                List<Windows.ApplicationModel.Contacts.Contact> foundContactList = null;
                if (!String.IsNullOrWhiteSpace(term))
                {
                    foundContactList = (await agenda.FindContactsAsync(term)).ToList();
                }
                else
                {
                    foundContactList = (await agenda.FindContactsAsync()).ToList();
                }
                if (foundContactList != null && foundContactList.Count > 0)
                {
                    FilterAndSendContactList(foundContactList, filter, fields, callback);
                }
                else { callback.OnWarning(null, IContactResultCallback.Warning.No_Matches); }
            }
            catch (Exception ex)
            {
                callback.OnError(IContactResultCallback.Error.NoPermission);
            }
        }

        #endregion Private_Methods
    }
}