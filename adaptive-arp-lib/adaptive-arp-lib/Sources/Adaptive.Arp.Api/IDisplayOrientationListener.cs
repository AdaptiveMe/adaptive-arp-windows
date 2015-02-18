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

    * @version v2.1.6

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{

     /**
        Interface for handling display orientation change events.

        @author Carlos Lozano Diez
        @since v2.0.5
        @version 1.0
     */
     public interface IDisplayOrientationListener : IBaseListener
     {

          /**
             Although extremely unlikely, this event will be fired if something beyond the control of the
platform impedes the rotation of the display.

             @param error The error condition... generally unknown as it is unexpected!
             @since v2.0.5
          */
          void OnError(IDisplayOrientationListenerError error);

          /**
             Event fired with the successful start and finish of a rotation.

             @param rotationEvent RotationEvent containing origin, destination and state of the event.
             @since v2.0.5
          */
          void OnResult(RotationEvent rotationEvent);

          /**
             Event fired with a warning when the rotation is aborted. In specific, this
event may be fired if the application vetoes display rotation before rotation is completed.

             @param rotationEvent   RotationEvent containing origin, destination and state of the event.
             @param warning Type of condition that aborted rotation execution.
             @since v2.0.5
          */
          void OnWarning(RotationEvent rotationEvent, IDisplayOrientationListenerWarning warning);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
