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
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Contact operations
        Auto-generated implementation of IContact specification.
     */
public class ContactBridge : BasePIMBridge, IContact, APIBridge
{

          /**
             API Delegate.
          */
          private IContact _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public ContactBridge(IContact _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IContact delegate that manages platform specific functions..
          */
          public IContact GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IContact _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Get all the details of a contact according to its id

             @param contact  id to search for
             @param callback called for return
             @since v2.0
          */
          public void GetContact(ContactUid contact, IContactResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing getContact...");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContact(contact, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'getContact' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'getContact'.");
                    }
                    }
                    
               }

          /**
             Get the contact photo

             @param contact  id to search for
             @param callback called for return
             @since v2.0
          */
          public void GetContactPhoto(ContactUid contact, IContactPhotoResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing getContactPhoto...");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContactPhoto(contact, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'getContactPhoto' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'getContactPhoto'.");
                    }
                    }
                    
               }

          /**
             Get all contacts

             @param callback called for return
             @since v2.0
          */
          public void GetContacts(IContactResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing getContacts...");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContacts(callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'getContacts' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'getContacts'.");
                    }
                    }
                    
               }

          /**
             Get marked fields of all contacts

             @param callback called for return
             @param fields   to get for each Contact
             @since v2.0
          */
          public void GetContactsForFields(IContactResultCallback callback, IContactFieldGroup[] fields)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing getContactsForFields...");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContactsForFields(callback, fields);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'getContactsForFields' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'getContactsForFields'.");
                    }
                    }
                    
               }

          /**
             Get marked fields of all contacts according to a filter

             @param callback called for return
             @param fields   to get for each Contact
             @param filter   to search for
             @since v2.0
          */
          public void GetContactsWithFilter(IContactResultCallback callback, IContactFieldGroup[] fields, IContactFilter[] filter)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing getContactsWithFilter...");
               }

               if (this._delegate != null)
               {
                    this._delegate.GetContactsWithFilter(callback, fields, filter);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'getContactsWithFilter' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'getContactsWithFilter'.");
                    }
                    }
                    
               }

          /**
             Search contacts according to a term and send it to the callback

             @param term     string to search
             @param callback called for return
             @since v2.0
          */
          public void SearchContacts(string term, IContactResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing searchContacts...");
               }

               if (this._delegate != null)
               {
                    this._delegate.SearchContacts(term, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'searchContacts' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'searchContacts'.");
                    }
                    }
                    
               }

          /**
             Search contacts according to a term with a filter and send it to the callback

             @param term     string to search
             @param callback called for return
             @param filter   to search for
             @since v2.0
          */
          public void SearchContactsWithFilter(string term, IContactResultCallback callback, IContactFilter[] filter)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing searchContactsWithFilter...");
               }

               if (this._delegate != null)
               {
                    this._delegate.SearchContactsWithFilter(term, callback, filter);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'searchContactsWithFilter' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'searchContactsWithFilter'.");
                    }
                    }
                    
               }

          /**
             Set the contact photo

             @param contact  id to assign the photo
             @param pngImage photo as byte array
             @return true if set is successful;false otherwise
             @since v2.0
          */
          public bool SetContactPhoto(ContactUid contact, byte[] pngImage)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executing setContactPhoto...");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.SetContactPhoto(contact, pngImage);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Debug, this.apiGroup.ToString(),"ContactBridge executed 'setContactPhoto' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.Error, this.apiGroup.ToString(),"ContactBridge no delegate for 'setContactPhoto'.");
                    }
                    }
                    return result;                    
               }

          /**
             Invokes the given method specified in the API request object.

             @param request APIRequest object containing method name and parameters.
             @return APIResponse with status code, message and JSON response or a JSON null string for void functions. Status code 200 is OK, all others are HTTP standard error conditions.
          */
          public new APIResponse Invoke(APIRequest request)
          {
               APIResponse response = new APIResponse();
               int responseCode = 200;
               String responseMessage = "OK";
               String responseJSON = "null";
               switch (request.GetMethodName())
               {
                    case "getContact":
                         ContactUid contact0 = GetJSONProcessor().DeserializeObject<ContactUid>(request.GetParameters()[0]);
                         IContactResultCallback callback0 = new ContactResultCallbackImpl(request.GetAsyncId());
                         this.GetContact(contact0, callback0);
                         break;
                    case "getContactPhoto":
                         ContactUid contact1 = GetJSONProcessor().DeserializeObject<ContactUid>(request.GetParameters()[0]);
                         IContactPhotoResultCallback callback1 = new ContactPhotoResultCallbackImpl(request.GetAsyncId());
                         this.GetContactPhoto(contact1, callback1);
                         break;
                    case "getContacts":
                         IContactResultCallback callback2 = new ContactResultCallbackImpl(request.GetAsyncId());
                         this.GetContacts(callback2);
                         break;
                    case "getContactsForFields":
                         IContactResultCallback callback3 = new ContactResultCallbackImpl(request.GetAsyncId());
                         IContactFieldGroup[] fields3 = GetJSONProcessor().DeserializeObject<IContactFieldGroup[]>(request.GetParameters()[1]);
                         this.GetContactsForFields(callback3, fields3);
                         break;
                    case "getContactsWithFilter":
                         IContactResultCallback callback4 = new ContactResultCallbackImpl(request.GetAsyncId());
                         IContactFieldGroup[] fields4 = GetJSONProcessor().DeserializeObject<IContactFieldGroup[]>(request.GetParameters()[1]);
                         IContactFilter[] filter4 = GetJSONProcessor().DeserializeObject<IContactFilter[]>(request.GetParameters()[2]);
                         this.GetContactsWithFilter(callback4, fields4, filter4);
                         break;
                    case "searchContacts":
                         string term5 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         IContactResultCallback callback5 = new ContactResultCallbackImpl(request.GetAsyncId());
                         this.SearchContacts(term5, callback5);
                         break;
                    case "searchContactsWithFilter":
                         string term6 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[0]);
                         IContactResultCallback callback6 = new ContactResultCallbackImpl(request.GetAsyncId());
                         IContactFilter[] filter6 = GetJSONProcessor().DeserializeObject<IContactFilter[]>(request.GetParameters()[2]);
                         this.SearchContactsWithFilter(term6, callback6, filter6);
                         break;
                    case "setContactPhoto":
                         ContactUid contact7 = GetJSONProcessor().DeserializeObject<ContactUid>(request.GetParameters()[0]);
                         byte[] pngImage7 = GetJSONProcessor().DeserializeObject<byte[]>(request.GetParameters()[1]);
                         bool response7 = this.SetContactPhoto(contact7, pngImage7);
                         responseJSON = GetJSONProcessor().SerializeObject(response7);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "ContactBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.2.0.";
                         break;
               }
               response.SetResponse(responseJSON);
               response.SetStatusCode(responseCode);
               response.SetStatusMessage(responseMessage);
               return response;
          }
     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
