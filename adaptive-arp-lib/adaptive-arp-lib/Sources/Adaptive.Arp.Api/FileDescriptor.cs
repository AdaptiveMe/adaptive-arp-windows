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
        Implementation of FileDescriptor bean.

        @author Carlos Lozano Diez
        @since 1.0
        @version 1.0
     */
     public class FileDescriptor : APIBean
     {

          public long DateCreated { get; set; }
          public long DateModified { get; set; }
          public string Name { get; set; }
          public string Path { get; set; }
          public string PathAbsolute { get; set; }
          public long Size { get; set; }

          /**
             Default constructor.
          */
          public FileDescriptor()  {
          }

          /**
             Returns the milliseconds passed since 1/1/1970 since the file was created.

             @return Timestamp in milliseconds.
             @since V2.0
          */
          public long GetDateCreated() {
               return this.DateCreated;
          }

          /**
             Sets the creation timestamp in milliseconds. Used internally.

             @param DateCreated Timestamp of file creation or -1 if the file or folder doesn't exist.
          */
          public void SetDateCreated(long DateCreated) {
               this.DateCreated = DateCreated;
          }

          /**
             Returns the milliseconds passed since 1/1/1970 since the file was modified.

             @return Timestamp in milliseconds.
             @since V2.0
          */
          public long GetDateModified() {
               return this.DateModified;
          }

          /**
             Sets the file or folder modification timestamp in milliseconds. Used internally.

             @param DateModified Timestamp of file modification or -1 if the file or folder doesn't exist.
          */
          public void SetDateModified(long DateModified) {
               this.DateModified = DateModified;
          }

          /**
             Returns the name of the file if the reference is a file or the last path element of the folder.

             @return The name of the file.
             @since V2.0
          */
          public string GetName() {
               return this.Name;
          }

          /**
             Sets the name of the file. Used internally.

             @param Name Name of the file or last folder path element.
          */
          public void SetName(string Name) {
               this.Name = Name;
          }

          /**
             Returns the path element of the file or folder (excluding the last path element if it's a directory).

             @return The path to the file.
             @since V2.0
          */
          public string GetPath() {
               return this.Path;
          }

          /**
             Sets the path of the file or folder. Used internally.

             @param Path The path element of the file or folder.
          */
          public void SetPath(string Path) {
               this.Path = Path;
          }

          /**
             Returns the resolved absolute path elements of the file and/or folders (including the last path element).

             @return The absolute path to the file.
             @since V2.0
          */
          public string GetPathAbsolute() {
               return this.PathAbsolute;
          }

          /**
             Sets the absolute path of the file or folder. Used internally.

             @param PathAbsolute String with the absolute path of file or folder.
          */
          public void SetPathAbsolute(string PathAbsolute) {
               this.PathAbsolute = PathAbsolute;
          }

          /**
             Returns the size in bytes of the file or -1 if the reference is a folder.

             @return Size in bytes of file.
             @since V2.0
          */
          public long GetSize() {
               return this.Size;
          }

          /**
             Sets the file size in bytes of the file. If the file is a folder, this will be 0. If the file
doesn't exist, this will be -1. Used internally.

             @param Size The size in bytes of the file.
          */
          public void SetSize(long Size) {
               this.Size = Size;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
