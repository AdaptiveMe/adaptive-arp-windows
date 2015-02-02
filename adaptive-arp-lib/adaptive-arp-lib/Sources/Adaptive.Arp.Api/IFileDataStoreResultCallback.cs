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

    * @version v2.1.3

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the File store operations callback

        @author Carlos Lozano Diez
        @since v2.0
        @version 1.0
     */
     public interface IFileDataStoreResultCallback : IBaseCallback
     {

          /**
             Error processing data retrieval/storage operation.

             @param error Error condition encountered.
             @since v2.0
          */
          void OnError(IFileDataStoreResultCallbackError error);

          /**
             Result of data storage operation.

             @param file File reference to stored data.
             @since v2.0
          */
          void OnResult(FileDescriptor file);

          /**
             Result with warning of data retrieval/storage operation.

             @param file    File being loaded/stored.
             @param warning Warning condition encountered.
             @since v2.0
          */
          void OnWarning(FileDescriptor file, IFileDataStoreResultCallbackWarning warning);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
