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

namespace Adaptive.Arp.Api
{
     /**
        Represents a specific application life-cycle stage.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class Lifecycle : APIBean
     {

          /**
             Represent the state of the app
<p>
Possible lifecycle States:
<p>
1. Starting    - Before starting.
2. Started     - Start concluded.
3. Running     - Accepts user interaction - running in foreground.
4. Pausing     - Before going to background.
4.1 PausedIdle - In background, no scheduled background activity (passive).
4.2 PausedRun  - In background, scheduled background activity (periodic network access, gps access, etc.)
5. Resuming    - Before going to foreground, followed by Running state.
6. Stopping    - Before stopping.
          */
          public LifecycleState State { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Lifecycle()  {
          }

          /**
             Constructor used by the implementation

             @param State of the app
             @since V2.0
          */
          public Lifecycle(LifecycleState state) : base () {
               this.State = State;
          }

          /**
             Returns the state of the application

             @return State of the app
             @since V2.0
          */
          public LifecycleState GetState() {
               return this.State;
          }

          /**
             Set the State of the application

             @param State of the app
             @since V2.0
          */
          public void SetState(LifecycleState State) {
               this.State = State;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
