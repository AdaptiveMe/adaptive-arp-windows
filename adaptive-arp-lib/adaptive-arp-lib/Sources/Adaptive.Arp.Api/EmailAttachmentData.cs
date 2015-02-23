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
        Structure representing the binary attachment data.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class EmailAttachmentData : APIBean
     {

          /**
             The raw data for the current file attachment (byte array)
          */
          public byte[] Data { get; set; }
          /**
             The name of the current file attachment
          */
          public string FileName { get; set; }
          /**
             The mime type of the current attachment
          */
          public string MimeType { get; set; }
          /**
             The relative path where the contents for the attachment file could be located.
          */
          public string ReferenceUrl { get; set; }
          /**
             The data size (in bytes) of the current file attachment
          */
          public long Size { get; set; }

          /**
             Default Constructor

             @since V2.0
          */
          public EmailAttachmentData()  {
          }

          /**
             Constructor with fields

             @param Data         raw data of the file attachment
             @param Size         size of the file attachment
             @param FileName     name of the file attachment
             @param MimeType     mime type of the file attachment
             @param ReferenceUrl relative url of the file attachment
             @since V2.0
          */
          public EmailAttachmentData(byte[] data, long size, string fileName, string mimeType, string referenceUrl) : base () {
               this.Data = Data;
               this.Size = Size;
               this.FileName = FileName;
               this.MimeType = MimeType;
               this.ReferenceUrl = ReferenceUrl;
          }

          /**
             Returns the raw data in byte[]

             @return Data Octet-binary content of the attachment payload.
             @since V2.0
          */
          public byte[] GetData() {
               return this.Data;
          }

          /**
             Set the data of the attachment as a byte[]

             @param Data Sets the octet-binary content of the attachment.
             @since V2.0
          */
          public void SetData(byte[] Data) {
               this.Data = Data;
          }

          /**
             Returns the filename of the attachment

             @return FileName Name of the attachment.
             @since V2.0
          */
          public string GetFileName() {
               return this.FileName;
          }

          /**
             Set the name of the file attachment

             @param FileName Name of the attachment.
             @since V2.0
          */
          public void SetFileName(string FileName) {
               this.FileName = FileName;
          }

          /**
             Returns the mime type of the attachment

             @return MimeType
             @since V2.0
          */
          public string GetMimeType() {
               return this.MimeType;
          }

          /**
             Set the mime type of the attachment

             @param MimeType Mime-type of the attachment.
             @since V2.0
          */
          public void SetMimeType(string MimeType) {
               this.MimeType = MimeType;
          }

          /**
             Returns the absolute url of the file attachment

             @return ReferenceUrl Absolute URL of the file attachment for either file:// or http:// access.
             @since V2.0
          */
          public string GetReferenceUrl() {
               return this.ReferenceUrl;
          }

          /**
             Set the absolute url of the attachment

             @param ReferenceUrl Absolute URL of the file attachment for either file:// or http:// access.
             @since V2.0
          */
          public void SetReferenceUrl(string ReferenceUrl) {
               this.ReferenceUrl = ReferenceUrl;
          }

          /**
             Returns the size of the attachment as a long

             @return Size Length in bytes of the octet-binary content.
             @since V2.0
          */
          public long GetSize() {
               return this.Size;
          }

          /**
             Set the size of the attachment as a long

             @param Size Length in bytes of the octet-binary content ( should be same as data array length.)
             @since V2.0
          */
          public void SetSize(long Size) {
               this.Size = Size;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
