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
 *     * 
 *
 * =====================================================================================================================
 */
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public abstract class AbstractCapabilitiesImpl : ICapabilities
    {
        public override bool HasButtonSupport(ICapabilities.Button type)
        {
            return false;
        }

        public override bool HasCommunicationSupport(ICapabilities.Communication type)
        {
            return false;
        }

        public override bool HasDataSupport(ICapabilities.Data type)
        {
            return false;
        }

        public override bool HasMediaSupport(ICapabilities.Media type)
        {
            return false;
        }

        public override bool HasNetSupport(ICapabilities.Net type)
        {
            return false;
        }

        public override bool HasNotificationSupport(ICapabilities.Notification type)
        {
            return false;
        }

        public override bool HasSensorSupport(ICapabilities.Sensor type)
        {
            return false;
        }
    }
}
