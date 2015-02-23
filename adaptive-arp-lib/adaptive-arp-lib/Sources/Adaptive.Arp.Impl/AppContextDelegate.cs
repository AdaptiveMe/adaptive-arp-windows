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
        Interface for context management purposes
        Auto-generated implementation of IAppContext specification.
     */
     public class AppContextDelegate : IAppContext
     {

          /**
             Default Constructor.
          */
          public AppContextDelegate() : base()
          {
          }

          /**
             The main application context. This should be cast to the platform specific implementation.

             @return Object representing the specific singleton application context provided by the OS.
             @since v2.0
          */
          public Object GetContext() {
               // Object response;
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextDelegate:getContext");
               // return response;
          }

          /**
             The type of context provided by the getContext method.

             @return Type of platform context.
             @since v2.0
          */
          public IOSType GetContextType() {
               // IOSType response;
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextDelegate:getContextType");
               // return response;
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
