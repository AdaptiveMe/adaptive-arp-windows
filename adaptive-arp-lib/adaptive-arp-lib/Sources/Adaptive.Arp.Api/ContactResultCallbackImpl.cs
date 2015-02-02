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

    * @version v2.1.2

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Contact operations
        Auto-generated implementation of IContactResultCallback specification.
     */
     public class ContactResultCallbackImpl : BaseCallbackImpl, IContactResultCallback
     {

          /**
             Constructor with callback id.

          @param id  The id of the callback.
          */
          public ContactResultCallbackImpl(long id) : base(id)
          {
          }

          /**
             This method is called on Error

             @param error returned by the platform
             @since v2.0
          */
          public void OnError(IContactResultCallbackError error)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleContactResultCallbackError( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(error) +") )");
          }

          /**
             This method is called on Result

             @param contacts returned by the platform
             @since v2.0
          */
          public void OnResult(Contact[] contacts)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleContactResultCallbackResult( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(contacts) +") )");
          }

          /**
             This method is called on Warning

             @param contacts returned by the platform
             @param warning  returned by the platform
             @since v2.0
          */
          public void OnWarning(Contact[] contacts, IContactResultCallbackWarning warning)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleContactResultCallbackWarning( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(contacts) +"), JSON.parse(" + GetJSONProcessor().SerializeObject(warning) +") )");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
