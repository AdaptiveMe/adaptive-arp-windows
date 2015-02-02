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
        Interface for Managing the Globalization results

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public interface IGlobalization : IBaseApplication
     {

          /**
             Returns the default locale of the application defined in the configuration file

             @return Default Locale of the application
             @since v2.0
          */
          Locale GetDefaultLocale();

          /**
             List of supported locales for the application defined in the configuration file

             @return List of locales
             @since v2.0
          */
          Locale[] GetLocaleSupportedDescriptors();

          /**
             Gets the text/message corresponding to the given key and locale.

             @param key    to match text
             @param locale The locale object to get localized message, or the locale desciptor ("language" or "language-country" two-letters ISO codes.
             @return Localized text.
             @since v2.0
          */
          string GetResourceLiteral(string key, Locale locale);

          /**
             Gets the full application configured literals (key/message pairs) corresponding to the given locale.

             @param locale The locale object to get localized message, or the locale desciptor ("language" or "language-country" two-letters ISO codes.
             @return Localized texts in the form of an object.
             @since v2.0
          */
          KeyPair[] GetResourceLiterals(Locale locale);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
