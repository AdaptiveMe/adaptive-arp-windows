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
        Structure representing the basic device information.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class DeviceInfo : APIBean
     {

          /**
             Model of device - equivalent to device release or version.
          */
          public string Model { get; set; }
          /**
             Name of device - equivalent to brand.
          */
          public string Name { get; set; }
          /**
             Device identifier - this may not be unique for a device. It may depend on the platform implementation and may
be unique for a specific instance of an application on a specific device.
          */
          public string Uuid { get; set; }
          /**
             Vendor of the device hardware.
          */
          public string Vendor { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public DeviceInfo()  {
          }

          /**
             Constructor for the implementation of the platform.

             @param Name   or brand of the device.
             @param Model  of the device.
             @param Vendor of the device.
             @param Uuid   unique* identifier (* platform dependent).
             @since V2.0
          */
          public DeviceInfo(string name, string model, string vendor, string uuid) : base () {
               this.Name = Name;
               this.Model = Model;
               this.Vendor = Vendor;
               this.Uuid = Uuid;
          }

          /**
             Returns the model of the device.

             @return String with the model of the device.
             @since V2.0
          */
          public string GetModel() {
               return this.Model;
          }

          /**
             Sets Model of device - equivalent to device release or version.

             @param model Model of device - equivalent to device release or version.
          */
          public void SetModel(string Model) {
               this.Model = Model;
          }

          /**
             Returns the name of the device.

             @return String with device name.
             @since V2.0
          */
          public string GetName() {
               return this.Name;
          }

          /**
             Sets Name of device - equivalent to brand.

             @param name Name of device - equivalent to brand.
          */
          public void SetName(string Name) {
               this.Name = Name;
          }

          /**
             Returns the platform dependent UUID of the device.

             @return String with the 128-bit device identifier.
             @since V2.0
          */
          public string GetUuid() {
               return this.Uuid;
          }

          /**
             Sets Device identifier - this may not be unique for a device. It may depend on the platform implementation and may
be unique for a specific instance of an application on a specific device.

             @param uuid Device identifier - this may not be unique for a device. It may depend on the platform implementation and may
be unique for a specific instance of an application on a specific device.
          */
          public void SetUuid(string Uuid) {
               this.Uuid = Uuid;
          }

          /**
             Returns the vendor of the device.

             @return String with the vendor name.
             @since V2.0
          */
          public string GetVendor() {
               return this.Vendor;
          }

          /**
             Sets Vendor of the device hardware.

             @param vendor Vendor of the device hardware.
          */
          public void SetVendor(string Vendor) {
               this.Vendor = Vendor;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
