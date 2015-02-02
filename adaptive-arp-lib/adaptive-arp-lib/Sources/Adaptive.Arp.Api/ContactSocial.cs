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
        Structure representing the social data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactSocial : APIBean
     {

          /**
             The social network
          */
          public ContactSocialNetwork SocialNetwork { get; set; }
          /**
             The profileUrl
          */
          public string ProfileUrl { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactSocial()  {
          }

          /**
             Constructor used by the implementation

             @param SocialNetwork of the profile
             @param ProfileUrl    of the user
             @since V2.0
          */
          public ContactSocial(ContactSocialNetwork socialNetwork, string profileUrl) : base () {
               this.SocialNetwork = SocialNetwork;
               this.ProfileUrl = ProfileUrl;
          }

          /**
             Returns the social network

             @return SocialNetwork
             @since V2.0
          */
          public ContactSocialNetwork GetSocialNetwork() {
               return this.SocialNetwork;
          }

          /**
             Set the social network

             @param SocialNetwork of the profile
             @since V2.0
          */
          public void SetSocialNetwork(ContactSocialNetwork SocialNetwork) {
               this.SocialNetwork = SocialNetwork;
          }

          /**
             Returns the profile url of the user

             @return ProfileUrl
             @since V2.0
          */
          public string GetProfileUrl() {
               return this.ProfileUrl;
          }

          /**
             Set the profile url of the iser

             @param ProfileUrl of the user
             @since V2.0
          */
          public void SetProfileUrl(string ProfileUrl) {
               this.ProfileUrl = ProfileUrl;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
