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
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Api.Impl
{

     /**
        Interface for Managing the Security operations
        Auto-generated implementation of ISecurity specification.
     */
     public class SecurityDelegate : BaseSecurityDelegate, ISecurity
     {

          /**
             Default Constructor.
          */
          public SecurityDelegate() : base()
          {
          }

          /**
             Deletes from the device internal storage the entry/entries containing the specified key names.

             @param keys             Array with the key names to delete.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void DeleteSecureKeyValuePairs(string[] keys, string publicAccessName, ISecurityResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("SecurityDelegate:deleteSecureKeyValuePairs");
          }

          /**
             Retrieves from the device internal storage the entry/entries containing the specified key names.

             @param keys             Array with the key names to retrieve.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void GetSecureKeyValuePairs(string[] keys, string publicAccessName, ISecurityResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("SecurityDelegate:getSecureKeyValuePairs");
          }

          /**
             Returns if the device has been modified in anyhow

             @return true if the device has been modified; false otherwise
             @since v2.0
          */
          public bool IsDeviceModified() {
               // bool response;
               // TODO: Not implemented.
               throw new NotSupportedException("SecurityDelegate:isDeviceModified");
               // return response;
          }

          /**
             Stores in the device internal storage the specified item/s.

             @param keyValues        Array containing the items to store on the device internal memory.
             @param publicAccessName The name of the shared internal storage object (if needed).
             @param callback         callback to be executed upon function result.
             @since v2.0
          */
          public void SetSecureKeyValuePairs(SecureKeyPair[] keyValues, string publicAccessName, ISecurityResultCallback callback) {
               // TODO: Not implemented.
               throw new NotSupportedException("SecurityDelegate:setSecureKeyValuePairs");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
