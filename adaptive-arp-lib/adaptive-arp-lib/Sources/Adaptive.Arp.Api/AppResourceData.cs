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
        This class represents a resource provided by the platform from the application's secure payload.

        @author Carlos Lozano Diez
        @since v2.1.3
        @version 1.0
     */
     public class AppResourceData
     {

          /**
             Marker to indicate whether the resource is cooked in some way (compressed, encrypted, etc.) If true, the
implementation must uncompress/unencrypt following the cookedType recipe specified by the payload.
          */
          public bool Cooked { get; set; }
          /**
             This is the length of the payload after cooking. In general, this length indicates the amount
of space saved with regard to the rawLength of the payload.
          */
          public long CookedLength { get; set; }
          /**
             If the data is cooked, this field should contain the recipe to return the cooked data to its original
uncompressed/unencrypted/etc format.
          */
          public string CookedType { get; set; }
          /**
             The payload data of the resource in ready to consume format.
          */
          public byte[] Data { get; set; }
          /**
             The id or path identifier of the resource.
          */
          public string Id { get; set; }
          /**
             The raw length of the payload before any cooking occurred. This is equivalent to the size of the resource
after uncompressing and unencrypting.
          */
          public long RawLength { get; set; }
          /**
             The raw type of the payload - this is equivalent to the mimetype of the content.
          */
          public string RawType { get; set; }

          /**
             Default constructor.

             @since V2.1.3
          */
          public AppResourceData()  {
          }

          /**
             Convenience constructor.

             @param Id           The id or path of the resource retrieved.
             @param Data         The payload data of the resource (uncooked).
             @param RawType      The raw type/mimetype of the resource.
             @param RawLength    The raw length/original length in bytes of the resource.
             @param Cooked       True if the resource is cooked.
             @param CookedType   Type of recipe used for cooking.
             @param CookedLength The cooked length in bytes of the resource.
             @since V2.1.3
          */
          public AppResourceData(string id, byte[] data, string rawType, long rawLength, bool cooked, string cookedType, long cookedLength) : base () {
               this.Id = Id;
               this.Data = Data;
               this.RawType = RawType;
               this.RawLength = RawLength;
               this.Cooked = Cooked;
               this.CookedType = CookedType;
               this.CookedLength = CookedLength;
          }

          /**
             Attribute to denote whether the payload of the resource is cooked.

             @return True if the resource is cooked, false otherwise.
             @since V2.1.3
          */
          public bool GetCooked() {
               return this.Cooked;
          }

          /**
             Attribute to denote whether the payload of the resource is cooked.

             @param Cooked True if the resource is cooked, false otherwise.
             @since V2.1.3
          */
          public void SetCooked(bool Cooked) {
               this.Cooked = Cooked;
          }

          /**
             The length in bytes of the payload after cooking.

             @return Length in bytes of cooked payload.
             @since V2.1.3
          */
          public long GetCookedLength() {
               return this.CookedLength;
          }

          /**
             The length in bytes of the payload after cooking.

             @param CookedLength Length in bytes of cooked payload.
             @since V2.1.3
          */
          public void SetCookedLength(long CookedLength) {
               this.CookedLength = CookedLength;
          }

          /**
             If the resource is cooked, this will return the recipe used during cooking.

             @return The cooking recipe to reverse the cooking process.
             @since V2.1.3
          */
          public string GetCookedType() {
               return this.CookedType;
          }

          /**
             If the resource is cooked, the type of recipe used during cooking.

             @param CookedType The cooking recipe used during cooking.
             @since V2.1.3
          */
          public void SetCookedType(string CookedType) {
               this.CookedType = CookedType;
          }

          /**
             Returns the payload of the resource.

             @return Binary payload of the resource.
             @since V2.1.3
          */
          public byte[] GetData() {
               return this.Data;
          }

          /**
             Sets the payload of the resource.

             @param Data Binary payload of the resource.
             @since V2.1.3
          */
          public void SetData(byte[] Data) {
               this.Data = Data;
          }

          /**
             Gets The id or path identifier of the resource.

             @return id The id or path identifier of the resource.
          */
          public string GetId() {
               return this.Id;
          }

          /**
             Sets the id or path of the resource.

             @param Id The id or path of the resource.
             @since V2.1.3
          */
          public void SetId(string Id) {
               this.Id = Id;
          }

          /**
             Gets the resource payload's original length.

             @return Original length of the resource in bytes before cooking.
             @since V2.1.3
          */
          public long GetRawLength() {
               return this.RawLength;
          }

          /**
             Sets the resource payload's original length.

             @param RawLength Original length of the resource in bytes before cooking.
             @since V2.1.3
          */
          public void SetRawLength(long RawLength) {
               this.RawLength = RawLength;
          }

          /**
             Gets the resource's raw type or mimetype.

             @return Resource's type or mimetype.
             @since V2.1.3
          */
          public string GetRawType() {
               return this.RawType;
          }

          /**
             Sets the resource's raw type or mimetype.

             @param RawType Resource's type or mimetype.
             @since V2.1.3
          */
          public void SetRawType(string RawType) {
               this.RawType = RawType;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
