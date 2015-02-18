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
        Interface for Managing the Display operations

        @author Carlos Lozano Diez
        @since v2.0
        @version 1.0
     */
     public interface IDisplay : IBaseSystem
     {

          /**
             Add a listener to start receiving display orientation change events.

             @param listener Listener to add to receive orientation change events.
             @since v2.0.5
          */
          void AddDisplayOrientationListener(IDisplayOrientationListener listener);

          /**
             Returns the current orientation of the display. Please note that this may be different from the orientation
of the device. For device orientation, use the IDevice APIs.

             @return The current orientation of the display.
             @since v2.0.5
          */
          ICapabilitiesOrientation GetOrientationCurrent();

          /**
             Remove a listener to stop receiving display orientation change events.

             @param listener Listener to remove from receiving orientation change events.
             @since v2.0.5
          */
          void RemoveDisplayOrientationListener(IDisplayOrientationListener listener);

          /**
             Remove all listeners receiving display orientation events.

             @since v2.0.5
          */
          void RemoveDisplayOrientationListeners();

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
