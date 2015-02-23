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

namespace Adaptive.Arp.Api
{
     /**
        Structure representing the data a single geolocation reading.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class Geolocation : APIBean
     {

          /**
             The current device altitude (or Z coordinate). Measured in meters.
          */
          public double Altitude { get; set; }
          /**
             The Y coordinate (or latitude). Measured in degrees.
          */
          public double Latitude { get; set; }
          /**
             The X coordinate (or longitude). Measured in degrees.
          */
          public double Longitude { get; set; }
          /**
             Timestamp of the geolocation reading.
          */
          public long Timestamp { get; set; }
          /**
             Dilution of precision on the X measurement. Measured in meters.
          */
          public float XDoP { get; set; }
          /**
             Dilution of precision on the Y measurement. Measured in meters.
          */
          public float YDoP { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Geolocation()  {
          }

          /**
             Constructor with parameters

             @param Latitude  Latitude of the measurement
             @param Longitude Longitude of the measurement
             @param Altitude  Altitude of the measurement
             @param XDoP      Dilution of precision on the X measurement
             @param YDoP      Dilution of precision on the Y measurement
             @param Timestamp Timestamp of the measurement
             @since V2.0
          */
          public Geolocation(double latitude, double longitude, double altitude, float xDoP, float yDoP, long timestamp) : base () {
               this.Latitude = Latitude;
               this.Longitude = Longitude;
               this.Altitude = Altitude;
               this.XDoP = XDoP;
               this.YDoP = YDoP;
               this.Timestamp = Timestamp;
          }

          /**
             Constructor with parameters

             @param Latitude  Latitude of the measurement
             @param Longitude Longitude of the measurement
             @param Timestamp Timestamp of the measurement
             @since V2.0
          */
          public Geolocation(double latitude, double longitude, long timestamp) : base () {
               this.Latitude = Latitude;
               this.Longitude = Longitude;
               this.Timestamp = Timestamp;
          }

          /**
             Returns altitude in meters

             @return Altitude of the measurement
             @since V2.0
          */
          public double GetAltitude() {
               return this.Altitude;
          }

          /**
             Set altitude in meters

             @param Altitude Altitude of the measurement
             @since V2.0
          */
          public void SetAltitude(double Altitude) {
               this.Altitude = Altitude;
          }

          /**
             Returns the latitude in degrees

             @return Latitude of the measurement
             @since V2.0
          */
          public double GetLatitude() {
               return this.Latitude;
          }

          /**
             Set the latitude in degrees

             @param Latitude Latitude of the measurement
             @since V2.0
          */
          public void SetLatitude(double Latitude) {
               this.Latitude = Latitude;
          }

          /**
             Returns the longitude in degrees

             @return Longitude of the measurement
             @since V2.0
          */
          public double GetLongitude() {
               return this.Longitude;
          }

          /**
             Returns the latitude in degrees

             @param Longitude Longitude of the measurement
             @since V2.0
          */
          public void SetLongitude(double Longitude) {
               this.Longitude = Longitude;
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
             Gets Dilution of precision on the X measurement. Measured in meters.

             @return xDoP Dilution of precision on the X measurement. Measured in meters.
          */
          public float GetXDoP() {
               return this.XDoP;
          }

          /**
             Sets Dilution of precision on the X measurement. Measured in meters.

             @param xDoP Dilution of precision on the X measurement. Measured in meters.
          */
          public void SetXDoP(float XDoP) {
               this.XDoP = XDoP;
          }

          /**
             Gets Dilution of precision on the Y measurement. Measured in meters.

             @return yDoP Dilution of precision on the Y measurement. Measured in meters.
          */
          public float GetYDoP() {
               return this.YDoP;
          }

          /**
             Sets Dilution of precision on the Y measurement. Measured in meters.

             @param yDoP Dilution of precision on the Y measurement. Measured in meters.
          */
          public void SetYDoP(float YDoP) {
               this.YDoP = YDoP;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
