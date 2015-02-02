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
        Structure representing the assigned tags data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactTag : APIBean
     {

          /**
             The tagName of the Tag
          */
          public string TagName { get; set; }
          /**
             The tagValue of the Tag
          */
          public string TagValue { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactTag()  {
          }

          /**
             Constructor used by the implementation

             @param TagValue Value of the tag
             @param TagName  Name of the tag
             @since V2.0
          */
          public ContactTag(string tagName, string tagValue) : base () {
               this.TagName = TagName;
               this.TagValue = TagValue;
          }

          /**
             Returns the tagName of the Tag

             @return TagName
             @since V2.0
          */
          public string GetTagName() {
               return this.TagName;
          }

          /**
             Set the tagName of the Tag

             @param TagName Name of the tag
             @since V2.0
          */
          public void SetTagName(string TagName) {
               this.TagName = TagName;
          }

          /**
             Returns the tagValue of the Tag

             @return TagValue
             @since V2.0
          */
          public string GetTagValue() {
               return this.TagValue;
          }

          /**
             Set the tagValue of the Tag

             @param TagValue Value of the tag
             @since V2.0
          */
          public void SetTagValue(string TagValue) {
               this.TagValue = TagValue;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
