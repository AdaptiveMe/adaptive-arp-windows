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
        Interface for Managing the Cloud operations
        Auto-generated implementation of IDatabaseResultCallback specification.
     */
     public class DatabaseResultCallbackImpl : BaseCallbackImpl, IDatabaseResultCallback
     {

          /**
             Constructor with callback id.

          @param id  The id of the callback.
          */
          public DatabaseResultCallbackImpl(long id) : base(id)
          {
          }

          /**
             Result callback for error responses

             @param error Returned error
             @since v2.0
          */
          public void OnError(IDatabaseResultCallbackError error)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleDatabaseResultCallbackError( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(error) +") )");
          }

          /**
             Result callback for correct responses

             @param database Returns the database
             @since v2.0
          */
          public void OnResult(Database database)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleDatabaseResultCallbackResult( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(database) +") )");
          }

          /**
             Result callback for warning responses

             @param database Returns the database
             @param warning  Returned Warning
             @since v2.0
          */
          public void OnWarning(Database database, IDatabaseResultCallbackWarning warning)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleDatabaseResultCallbackWarning( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(database) +"), JSON.parse(" + GetJSONProcessor().SerializeObject(warning) +") )");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
