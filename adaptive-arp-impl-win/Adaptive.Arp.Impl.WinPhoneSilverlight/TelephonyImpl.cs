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
using Microsoft.Phone.Tasks;
using System;
using System.Text.RegularExpressions;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    public class TelephonyImpl : ITelephony
    {
        private LoggingImpl _Log = new LoggingImpl();
        private string _logCategory = "Telephony";

        /// <summary>
        /// Shows the Phone Dialer UI to the user with the specified number
        /// </summary>
        /// <param name="number">String containing the phone number to dial</param>
        /// <returns>True if the UI was shown to the user, otherwise false</returns>
        public override ITelephony.Status Call(string number)
        {
            try
            {
                PhoneCallTask phone = new PhoneCallTask();
                if (Regex.IsMatch(number, @"\d"))
                {
                    _Log.Log(ILogging.LogLevel.Info, _logCategory, "Method Call -- Dialing Number " + number);
                    phone.PhoneNumber = number;
                    phone.Show();
                    return Status.Dialing;
                }
                _Log.Log(ILogging.LogLevel.Warn, _logCategory, "Method Call -- Provided number is not valid: " + number);
                return Status.Failed;
            }
            catch (Exception ex)
            {
                _Log.Log(ILogging.LogLevel.Error, _logCategory, "No Permission");
                return Status.Failed;
            }
        }
    }
}