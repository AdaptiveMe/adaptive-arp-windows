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

    * @version v2.1.3

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a specific user or system locate.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class Locale : APIBean
     {

          /**
             A valid ISO Country Code.
          */
          public string Country { get; set; }
          /**
             A valid ISO Language Code.
          */
          public string Language { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Locale()  {
          }

          /**
             Constructor used by the implementation

             @param Country  Country of the Locale
             @param Language Language of the Locale
             @since V2.0
          */
          public Locale(string language, string country) : base () {
               this.Language = Language;
               this.Country = Country;
          }

          /**
             Returns the country code

             @return Country code
             @since V2.0
          */
          public string GetCountry() {
               return this.Country;
          }

          /**
             Set the country code

             @param Country code
             @since V2.0
          */
          public void SetCountry(string Country) {
               this.Country = Country;
          }

          /**
             Returns the language code

             @return Language code
             @since V2.0
          */
          public string GetLanguage() {
               return this.Language;
          }

          /**
             Set the language code

             @param Language code
             @since V2.0
          */
          public void SetLanguage(string Language) {
               this.Language = Language;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
