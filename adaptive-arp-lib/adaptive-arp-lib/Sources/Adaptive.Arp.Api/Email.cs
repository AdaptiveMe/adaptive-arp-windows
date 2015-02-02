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
        Structure representing the data elements of an email.

        @author Francisco Javier Martin Bueno
        @since v2.0
        @version 1.0
     */
     public class Email : APIBean
     {

          /**
             Array of Email Blind Carbon Copy recipients
          */
          public EmailAddress[] BccRecipients { get; set; }
          /**
             Array of Email Carbon Copy recipients
          */
          public EmailAddress[] CcRecipients { get; set; }
          /**
             Array of attatchments
          */
          public EmailAttachmentData[] EmailAttachmentData { get; set; }
          /**
             Message body
          */
          public string MessageBody { get; set; }
          /**
             Message body mime type
          */
          public string MessageBodyMimeType { get; set; }
          /**
             Subject of the email
          */
          public string Subject { get; set; }
          /**
             Array of Email recipients
          */
          public EmailAddress[] ToRecipients { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public Email()  {
          }

          /**
             Constructor used by the implementation

             @param ToRecipients        array of recipients
             @param CcRecipients        array of cc recipients
             @param BccRecipients       array of bcc recipients
             @param EmailAttachmentData array of attatchments
             @param MessageBody         body of the email
             @param MessageBodyMimeType mime type of the body
             @param Subject             of the email
             @since V2.0
          */
          public Email(EmailAddress[] toRecipients, EmailAddress[] ccRecipients, EmailAddress[] bccRecipients, EmailAttachmentData[] emailAttachmentData, string messageBody, string messageBodyMimeType, string subject) : base () {
               this.ToRecipients = ToRecipients;
               this.CcRecipients = CcRecipients;
               this.BccRecipients = BccRecipients;
               this.EmailAttachmentData = EmailAttachmentData;
               this.MessageBody = MessageBody;
               this.MessageBodyMimeType = MessageBodyMimeType;
               this.Subject = Subject;
          }

          /**
             Constructor used by the implementation

             @param ToRecipients array of recipients
             @param Subject      of the email
             @param MessageBody  body of the email
             @since V2.0
          */
          public Email(EmailAddress[] toRecipients, string subject, string messageBody) : base () {
               this.ToRecipients = ToRecipients;
               this.Subject = Subject;
               this.MessageBody = MessageBody;
          }

          /**
             Returns the array of recipients

             @return BccRecipients array of bcc recipients
             @since V2.0
          */
          public EmailAddress[] GetBccRecipients() {
               return this.BccRecipients;
          }

          /**
             Set the array of recipients

             @param BccRecipients array of bcc recipients
             @since V2.0
          */
          public void SetBccRecipients(EmailAddress[] BccRecipients) {
               this.BccRecipients = BccRecipients;
          }

          /**
             Returns the array of recipients

             @return CcRecipients array of cc recipients
             @since V2.0
          */
          public EmailAddress[] GetCcRecipients() {
               return this.CcRecipients;
          }

          /**
             Set the array of recipients

             @param CcRecipients array of cc recipients
             @since V2.0
          */
          public void SetCcRecipients(EmailAddress[] CcRecipients) {
               this.CcRecipients = CcRecipients;
          }

          /**
             Returns an array of attachments

             @return EmailAttachmentData array with the email attachments
             @since V2.0
          */
          public EmailAttachmentData[] GetEmailAttachmentData() {
               return this.EmailAttachmentData;
          }

          /**
             Set the email attachment data array

             @param EmailAttachmentData array of email attatchments
             @since V2.0
          */
          public void SetEmailAttachmentData(EmailAttachmentData[] EmailAttachmentData) {
               this.EmailAttachmentData = EmailAttachmentData;
          }

          /**
             Returns the message body of the email

             @return Message Body string of the email
             @since V2.0
          */
          public string GetMessageBody() {
               return this.MessageBody;
          }

          /**
             Set the message body of the email

             @param MessageBody message body of the email
             @since V2.0
          */
          public void SetMessageBody(string MessageBody) {
               this.MessageBody = MessageBody;
          }

          /**
             Returns the myme type of the message body

             @return Mime type string of the message boddy
             @since V2.0
          */
          public string GetMessageBodyMimeType() {
               return this.MessageBodyMimeType;
          }

          /**
             Set the mime type for the message body

             @param MessageBodyMimeType type of the body message
             @since V2.0
          */
          public void SetMessageBodyMimeType(string MessageBodyMimeType) {
               this.MessageBodyMimeType = MessageBodyMimeType;
          }

          /**
             Returns the subject of the email

             @return Subject string of the email
             @since V2.0
          */
          public string GetSubject() {
               return this.Subject;
          }

          /**
             Set the subject of the email

             @param Subject of the email
             @since V2.0
          */
          public void SetSubject(string Subject) {
               this.Subject = Subject;
          }

          /**
             Returns the array of recipients

             @return ToRecipients array of recipients
             @since V2.0
          */
          public EmailAddress[] GetToRecipients() {
               return this.ToRecipients;
          }

          /**
             Set the array of recipients

             @param ToRecipients array of recipients
             @since V2.0
          */
          public void SetToRecipients(EmailAddress[] ToRecipients) {
               this.ToRecipients = ToRecipients;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
