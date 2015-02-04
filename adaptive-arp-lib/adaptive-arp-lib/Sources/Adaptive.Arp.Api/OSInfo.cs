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

    * @version v2.1.4

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents the basic information about the operating system.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class OSInfo : APIBean
     {

          /**
             The name of the operating system.
          */
          public IOSType Name { get; set; }
          /**
             The vendor of the operating system.
          */
          public string Vendor { get; set; }
          /**
             The version/identifier of the operating system.
          */
          public string Version { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public OSInfo()  {
          }

          /**
             Constructor used by implementation to set the OS information.

             @param Name    of the OS.
             @param Version of the OS.
             @param Vendor  of the OS.
             @since V2.0
          */
          public OSInfo(IOSType name, string version, string vendor) : base () {
               this.Name = Name;
               this.Version = Version;
               this.Vendor = Vendor;
          }

          /**
             Returns the name of the operating system.

             @return OS name.
             @since V2.0
          */
          public IOSType GetName() {
               return this.Name;
          }

          /**
             Sets The name of the operating system.

             @param name The name of the operating system.
          */
          public void SetName(IOSType Name) {
               this.Name = Name;
          }

          /**
             Returns the vendor of the operating system.

             @return OS vendor.
             @since V2.0
          */
          public string GetVendor() {
               return this.Vendor;
          }

          /**
             Sets The vendor of the operating system.

             @param vendor The vendor of the operating system.
          */
          public void SetVendor(string Vendor) {
               this.Vendor = Vendor;
          }

          /**
             Returns the version of the operating system.

             @return OS version.
             @since V2.0
          */
          public string GetVersion() {
               return this.Version;
          }

          /**
             Sets The version/identifier of the operating system.

             @param version The version/identifier of the operating system.
          */
          public void SetVersion(string Version) {
               this.Version = Version;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
