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
        Structure representing the professional info data elements of a contact.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class ContactProfessionalInfo : APIBean
     {

          /**
             The company of the job
          */
          public string Company { get; set; }
          /**
             The job description
          */
          public string JobDescription { get; set; }
          /**
             The job title
          */
          public string JobTitle { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public ContactProfessionalInfo()  {
          }

          /**
             Constructor used by implementation to set the ContactProfessionalInfo.

             @param JobTitle       The job title
             @param JobDescription The job description
             @param Company        The company of the job
             @since V2.0
          */
          public ContactProfessionalInfo(string jobTitle, string jobDescription, string company) : base () {
               this.JobTitle = JobTitle;
               this.JobDescription = JobDescription;
               this.Company = Company;
          }

          /**
             Returns the company of the job

             @return Company
             @since V2.0
          */
          public string GetCompany() {
               return this.Company;
          }

          /**
             Set the company of the job

             @param Company The company of the job
             @since V2.0
          */
          public void SetCompany(string Company) {
               this.Company = Company;
          }

          /**
             Returns the description of the job

             @return Description
             @since V2.0
          */
          public string GetJobDescription() {
               return this.JobDescription;
          }

          /**
             Set the description of the job

             @param JobDescription The job description
             @since V2.0
          */
          public void SetJobDescription(string JobDescription) {
               this.JobDescription = JobDescription;
          }

          /**
             Returns the title of the job

             @return Title
             @since V2.0
          */
          public string GetJobTitle() {
               return this.JobTitle;
          }

          /**
             Set the title of the job

             @param JobTitle The job title
             @since V2.0
          */
          public void SetJobTitle(string JobTitle) {
               this.JobTitle = JobTitle;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
