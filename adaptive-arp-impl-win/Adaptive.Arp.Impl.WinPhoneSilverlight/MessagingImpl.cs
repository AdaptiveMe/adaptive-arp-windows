/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * David Barranco Bonilla
 *             <https://github.com/aryslan>
 *             <mailto:ddbc@gft.com>
 *
 * =====================================================================================================================
 */

using Adaptive.Arp.Api;
using System;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    public class MessagingImpl : IMessaging
    {
        //ALL CLASS IS SL AND 8.1
        /// <summary>
        /// Shows the Native Email Composer UI to the user filled with the specified information
        /// </summary>
        /// <param name="data">Object containing all the email data to use</param>
        /// <param name="callback">Object that will be notified when results are ready</param>
        public void SendEmail(Email data, IMessagingCallback callback)
        {
            try
            {
                
                EmailMessage mail = new EmailMessage();
                mail.Body = data.GetMessageBody();
                mail.Subject = data.GetSubject();
                foreach (EmailAddress address in data.GetToRecipients())
                {
                    if (address != null && !String.IsNullOrWhiteSpace(address.GetAddress()))
                        mail.To.Add(new EmailRecipient(address.GetAddress()));
                }

                foreach (EmailAddress address in data.GetCcRecipients())
                {
                    if (address != null && !String.IsNullOrWhiteSpace(address.GetAddress()))
                        mail.CC.Add(new EmailRecipient(address.GetAddress()));
                }

                foreach (EmailAddress address in data.GetBccRecipients())
                {
                    if (address != null && !String.IsNullOrWhiteSpace(address.GetAddress()))
                        mail.Bcc.Add(new EmailRecipient(address.GetAddress()));
                }

                foreach (AttachmentData attachment in data.GetAttachmentData())
                {
                    EmailAttachment emailAttachment = new EmailAttachment();
                    emailAttachment.FileName = attachment.GetFileName();
                    ApplicationData.Current.TemporaryFolder.CreateFileAsync(attachment.GetFileName(), CreationCollisionOption.GenerateUniqueName).Completed = (action, status) =>
                    {
                        switch (status)
                        {
                            case Windows.Foundation.AsyncStatus.Completed:
                                StorageFile attachmentTempFile = action.GetResults();
                                mail = CreateAttachmentObject(attachmentTempFile, attachment, mail);
                                break;

                            default:
                                break;
                        }
                    };
                }

                EmailManager.ShowComposeNewEmailAsync(mail).Completed = (action, status) =>
                {
                    switch (status)
                    {
                        case Windows.Foundation.AsyncStatus.Completed:
                            callback.OnResult(true);
                            break;

                        default:
                            callback.OnError(IMessagingCallback.Error.Not_Sent);
                            break;
                    }
                };
            }
            catch (Exception ex)
            {
                callback.OnError(IMessagingCallback.Error.Not_Sent);
            }
        }

        /// <summary>
        /// Shows the Native SMS compose UI to the user filled with the specified information
        /// </summary>
        /// <param name="number">Phone number to send the SMS message</param>
        /// <param name="text">Message to send</param>
        /// <param name="callback">object that will be notified when results are ready</param>
        public void SendSMS(string number, string text, IMessagingCallback callback)
        {
            if (!String.IsNullOrWhiteSpace(number)
                && !String.IsNullOrWhiteSpace(text)
                && Regex.IsMatch(number, @"\d"))
            {
                ChatMessage sms = new ChatMessage();
                sms.Body = text;
                sms.Recipients.Add(number);
                ChatMessageManager.ShowComposeSmsMessageAsync(sms).Completed = (action, status) =>
                {
                    switch (status)
                    {
                        case Windows.Foundation.AsyncStatus.Completed:
                            callback.OnResult(true);
                            break;

                        default:
                            callback.OnError(IMessagingCallback.Error.Not_Sent);
                            break;
                    }
                };
            }
        }

        #region Private_Methods

        /// <summary>
        /// Fills Temp attachment file with content and adds it to the Email Composer Attachment collection
        /// </summary>
        /// <param name="attachmentTempFile">The Temp file that will store attachmend data</param>
        /// <param name="attachment">Attachment object to get data</param>
        /// <param name="mail">EmailComposer object to add the attachments</param>
        /// <returns>Email Composer object with attachments already added</returns>
        private EmailMessage CreateAttachmentObject(StorageFile attachmentTempFile, AttachmentData attachment, EmailMessage mail)
        {
            FileIO.WriteBytesAsync(attachmentTempFile, attachment.GetData()).Completed = (fileAction, fileStatus) =>
            {
                switch (fileStatus)
                {
                    case Windows.Foundation.AsyncStatus.Completed:
                        mail.Attachments.Add(new EmailAttachment(attachment.GetFileName(), RandomAccessStreamReference.CreateFromFile(attachmentTempFile)));
                        break;

                    default:
                        break;
                }
            };
            return mail;
        }

        #endregion Private_Methods
    }
}