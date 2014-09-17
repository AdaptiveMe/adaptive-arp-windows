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

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    public class CapabilitiesImpl : AbstractCapabilitiesImpl
    {
        public sealed override bool HasSensorSupport(ICapabilities.Sensor type)
        {
            bool supported = false;
            switch (type)
            {
                case Sensor.Accelerometer:
                    supported = Microsoft.Devices.Sensors.Accelerometer.IsSupported;
                    break;
                case Sensor.AmbientLight:
                    Windows.Devices.Sensors.LightSensor lightSensor = Windows.Devices.Sensors.LightSensor.GetDefault();
                    supported = (lightSensor!=null);
                    break;
                case Sensor.Barometer:
                    break;
                case Sensor.Geolocation:
                    Windows.Devices.Geolocation.Geolocator geolocator = new Windows.Devices.Geolocation.Geolocator();
                    Windows.Devices.Geolocation.PositionStatus status = geolocator.LocationStatus;
                    if (status != Windows.Devices.Geolocation.PositionStatus.NotAvailable) supported = true;
                    geolocator = null;
                    break;
                case Sensor.Gyroscope:
                    supported = Microsoft.Devices.Sensors.Gyroscope.IsSupported;
                    break;
                case Sensor.Magnetometer:
                    supported = Microsoft.Devices.Sensors.Compass.IsSupported;
                    break;
                case Sensor.Proximity:
                    break;
                default:
                    break;
            }
            return supported;
        }
    }
}
