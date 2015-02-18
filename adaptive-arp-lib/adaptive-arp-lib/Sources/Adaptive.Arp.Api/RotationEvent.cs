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

namespace Adaptive.Arp.Api
{
     /**
        Object for reporting orientation change events for device and display.

        @author Carlos Lozano Diez
        @since v2.0.5
        @version 1.0
     */
     public class RotationEvent : APIBean
     {

          /**
             The orientation we're rotating to. This is the future orientation when the state of the event is
WillStartRotation. This will be the current orientation when the rotation is finished with the state
DidFinishRotation.
          */
          public ICapabilitiesOrientation Destination { get; set; }
          /**
             The orientation we're rotating from. This is the current orientation when the state of the event is
WillStartRotation. This will be the previous orientation when the rotation is finished with the state
DidFinishRotation.
          */
          public ICapabilitiesOrientation Origin { get; set; }
          /**
             The state of the event to indicate the start of the rotation and the end of the rotation event. This allows
for functions to be pre-emptively performed (veto change, re-layout, etc.) before rotation is effected and
concluded.
          */
          public RotationEventState State { get; set; }
          /**
             The timestamps in milliseconds when the event was fired.
          */
          public long Timestamp { get; set; }

          /**
             Default constructor.

             @since V2.0.5
          */
          public RotationEvent()  {
          }

          /**
             Convenience constructor.

             @param Origin      Source orientation when the event was fired.
             @param Destination Destination orientation when the event was fired.
             @param State       State of the event (WillBegin, DidFinish).
             @param Timestamp   Timestamp in milliseconds when the event was fired.
             @since V2.0.5
          */
          public RotationEvent(ICapabilitiesOrientation origin, ICapabilitiesOrientation destination, RotationEventState state, long timestamp) : base () {
               this.Origin = Origin;
               this.Destination = Destination;
               this.State = State;
               this.Timestamp = Timestamp;
          }

          /**
             Gets the destination orientation of the event.

             @return Destination orientation.
             @since V2.0.5
          */
          public ICapabilitiesOrientation GetDestination() {
               return this.Destination;
          }

          /**
             Sets the destination orientation of the event.

             @param Destination Destination orientation.
             @since V2.0.5
          */
          public void SetDestination(ICapabilitiesOrientation Destination) {
               this.Destination = Destination;
          }

          /**
             Get the origin orientation of the event.

             @return Origin orientation.
             @since V2.0.5
          */
          public ICapabilitiesOrientation GetOrigin() {
               return this.Origin;
          }

          /**
             Set the origin orientation of the event.

             @param Origin Origin orientation
             @since V2.0.5
          */
          public void SetOrigin(ICapabilitiesOrientation Origin) {
               this.Origin = Origin;
          }

          /**
             Gets the current state of the event.

             @return State of the event.
             @since V2.0.5
          */
          public RotationEventState GetState() {
               return this.State;
          }

          /**
             Sets the current state of the event.

             @param State The state of the event.
             @since V2.0.5
          */
          public void SetState(RotationEventState State) {
               this.State = State;
          }

          /**
             Gets the timestamp in milliseconds of the event.

             @return Timestamp of the event.
             @since V2.0.5
          */
          public long GetTimestamp() {
               return this.Timestamp;
          }

          /**
             Sets the timestamp in milliseconds of the event.

             @param Timestamp Timestamp of the event.
             @since V2.0.5
          */
          public void SetTimestamp(long Timestamp) {
               this.Timestamp = Timestamp;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
