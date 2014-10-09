﻿/*
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
using System.Collections.Generic;
using System.Globalization;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class DeviceImpl : IDevice
    {
        private List<IButtonListener> buttonListenerList = new List<IButtonListener>();

        /// <summary>
        /// Adds the specified Button listener to the listener collection
        /// </summary>
        /// <param name="listener">Listener object to add to the collection</param>
        public void AddButtonListener(IButtonListener listener)
        {
            if (!buttonListenerList.Contains(listener)) buttonListenerList.Add(listener);
        }

        /// <summary>
        /// Gets information about the device
        /// </summary>
        /// <returns>DeviceInfo object containing the device information</returns>
        public DeviceInfo GetDeviceInfo()
        {
            //Not implemented yet for WP8.1 
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the culture the device is currently using
        /// </summary>
        /// <returns>Locale object</returns>
        public Locale GetLocaleCurrent()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            return new Locale(ci.Name, ci.EnglishName);
        }

        /// <summary>
        /// Removes the specified listener from the Listener collection
        /// </summary>
        /// <param name="listener">listener object to remove from the collection</param>
        public void RemoveButtonListener(IButtonListener listener)
        {
            if (buttonListenerList.Contains(listener)) buttonListenerList.Remove(listener);
        }

        /// <summary>
        /// Clears the Listener collection
        /// </summary>
        public void RemoveButtonListeners()
        {
            buttonListenerList.Clear();
        }
    }
}