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

    * @version v2.1.5

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Structure representing the data of a single acceleration reading.

        @author Carlos Lozano Diez
        @since v2.0
        @version 1.0
     */
     public class Acceleration : APIBean
     {

          /**
             Timestamp of the acceleration reading.
          */
          public long Timestamp { get; set; }
          /**
             X-axis component of the acceleration.
          */
          public double X { get; set; }
          /**
             Y-axis component of the acceleration.
          */
          public double Y { get; set; }
          /**
             Z-axis component of the acceleration.
          */
          public double Z { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Acceleration()  {
          }

          /**
             Constructor with fields

             @param X         X Coordinate
             @param Y         Y Coordinate
             @param Z         Z Coordinate
             @param Timestamp Timestamp
             @since V2.0
          */
          public Acceleration(double x, double y, double z, long timestamp) : base () {
               this.X = X;
               this.Y = Y;
               this.Z = Z;
               this.Timestamp = Timestamp;
          }

          /**
             Timestamp Getter

             @return Timestamp
             @since V2.0
          */
          public long GetTimestamp() {
               return this.Timestamp;
          }

          /**
             Timestamp Setter

             @param Timestamp Timestamp
             @since V2.0
          */
          public void SetTimestamp(long Timestamp) {
               this.Timestamp = Timestamp;
          }

          /**
             X Coordinate Getter

             @return X-axis component of the acceleration.
             @since V2.0
          */
          public double GetX() {
               return this.X;
          }

          /**
             X Coordinate Setter

             @param X X-axis component of the acceleration.
             @since V2.0
          */
          public void SetX(double X) {
               this.X = X;
          }

          /**
             Y Coordinate Getter

             @return Y-axis component of the acceleration.
             @since V2.0
          */
          public double GetY() {
               return this.Y;
          }

          /**
             Y Coordinate Setter

             @param Y Y-axis component of the acceleration.
             @since V2.0
          */
          public void SetY(double Y) {
               this.Y = Y;
          }

          /**
             Z Coordinate Getter

             @return Z-axis component of the acceleration.
             @since V2.0
          */
          public double GetZ() {
               return this.Z;
          }

          /**
             Z Coordinate Setter

             @param Z Z Coordinate
             @since V2.0
          */
          public void SetZ(double Z) {
               this.Z = Z;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
