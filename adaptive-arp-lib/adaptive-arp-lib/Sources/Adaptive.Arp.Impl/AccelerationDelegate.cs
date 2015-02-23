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
        Interface defining methods about the acceleration sensor
        Auto-generated implementation of IAcceleration specification.
     */
     public class AccelerationDelegate : BaseSensorDelegate, IAcceleration
     {

          /**
             Default Constructor.
          */
          public AccelerationDelegate() : base()
          {
          }

          /**
             Register a new listener that will receive acceleration events.

             @param listener to be registered.
             @since v2.0
          */
          public void AddAccelerationListener(IAccelerationListener listener) {
               // TODO: Not implemented.
               throw new NotSupportedException("AccelerationDelegate:addAccelerationListener");
          }

          /**
             De-registers an existing listener from receiving acceleration events.

             @param listener to be registered.
             @since v2.0
          */
          public void RemoveAccelerationListener(IAccelerationListener listener) {
               // TODO: Not implemented.
               throw new NotSupportedException("AccelerationDelegate:removeAccelerationListener");
          }

          /**
             Removed all existing listeners from receiving acceleration events.

             @since v2.0
          */
          public void RemoveAccelerationListeners() {
               // TODO: Not implemented.
               throw new NotSupportedException("AccelerationDelegate:removeAccelerationListeners");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
