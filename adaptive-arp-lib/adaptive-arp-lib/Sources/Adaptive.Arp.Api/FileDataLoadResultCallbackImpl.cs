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

    * @version v2.1.7

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the File loading callback responses
        Auto-generated implementation of IFileDataLoadResultCallback specification.
     */
     public class FileDataLoadResultCallbackImpl : BaseCallbackImpl, IFileDataLoadResultCallback
     {

          /**
             Constructor with callback id.

          @param id  The id of the callback.
          */
          public FileDataLoadResultCallbackImpl(long id) : base(id)
          {
          }

          /**
             Error processing data retrieval/storage operation.

             @param error Error condition encountered.
             @since v2.0
          */
          public void OnError(IFileDataLoadResultCallbackError error)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleFileDataLoadResultCallbackError( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(error) +") )");
          }

          /**
             Result of data retrieval operation.

             @param data Data loaded.
             @since v2.0
          */
          public void OnResult(byte[] data)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleFileDataLoadResultCallbackResult( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(data) +") )");
          }

          /**
             Result with warning of data retrieval/storage operation.

             @param data    File being loaded.
             @param warning Warning condition encountered.
             @since v2.0
          */
          public void OnWarning(byte[] data, IFileDataLoadResultCallbackWarning warning)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleFileDataLoadResultCallbackWarning( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(data) +"), JSON.parse(" + GetJSONProcessor().SerializeObject(warning) +") )");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
