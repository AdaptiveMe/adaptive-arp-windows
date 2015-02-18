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

    * @version v2.1.8

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{

     /**
        The IAppResourceManager is the interface that must be followed for the implementation of secure resource
reading from the application data container. Implementations of this class should provide the logic
to read data from the application container (that may be compressed and encrypted in different formats)
and return the uncompressed data in each case. Implementation specifics may vary between platforms but
the ResourceData and formats returned must be coherent between platforms.

        @author Carlos Lozano Diez
        @since v2.1.3
        @version 1.0
     */
     public interface IAppResourceManager
     {

          /**
             Retrieve a configuration resource from the secure application data container.

             @param id The id or relative path of the configuration resource to be retrieved.
             @return ResourceData with the configuration resource payload.
             @since v2.1.3
          */
          AppResourceData RetrieveConfigResource(string id);

          /**
             Retrieve a web resource from the secure application data container.

             @param id The id or relative path of the web resource to be retrieved.
             @return ResourceData with the web resource payload.
             @since v2.1.3
          */
          AppResourceData RetrieveWebResource(string id);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
