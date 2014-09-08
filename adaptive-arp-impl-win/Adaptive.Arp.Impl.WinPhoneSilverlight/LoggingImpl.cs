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
using System.Diagnostics;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    public class LoggingImpl : ILogging
    {
        //SL only
        /// <summary>
        /// Writes a Log line to the Output Console. DEBUG and ERROR will be shown only in DEBUG releases.
        /// </summary>
        /// <param name="level">The level/kind of the message to show</param>
        /// <param name="category">Category or API that writes the message</param>
        /// <param name="message">Content of the message</param>
        public override void Log(ILogging.LogLevel level, string category, string message)
        {
            StringBuilder sb = new StringBuilder();
            bool bIsDebug = true;
            switch (level)
            {
                case LogLevel.Debug:
                    sb.Append("DEBUG ");
                    break;

                case LogLevel.Error:
                    sb.Append("ERROR ");
                    break;

                case LogLevel.Info:
                    sb.Append("INFO ");
                    bIsDebug = false;
                    break;

                case LogLevel.Warn:
                    sb.Append("WARNING ");
                    bIsDebug = false;
                    break;
            }
            sb.Append(!(String.IsNullOrWhiteSpace(category)) ? "(" + category + ") -- Message: " + message : " -- Message: " + message);
            switch (bIsDebug)
            {
                case true:
                    Debug.WriteLine(sb.ToString());
                    break;

                case false:
                    Debugger.Log(1, category, sb.ToString() + Environment.NewLine);
                    break;
            }
        }

        /// <summary>
        /// Writes a Log line to the Output Console. DEBUG and ERROR will be shown only in DEBUG releases.
        /// </summary>
        /// <param name="level">The level/kind of the message to show</param>
        /// <param name="message">Content of the message</param>
        public override void Log(ILogging.LogLevel level, string message)
        {
            Log(level, String.Empty, message);
        }
    }
}