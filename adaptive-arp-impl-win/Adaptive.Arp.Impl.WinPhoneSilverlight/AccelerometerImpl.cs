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
using Adaptive.Arp.Impl.Util;
using Microsoft.Devices.Sensors;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight
{
    public class AccelerometerImpl : AbstractAccelerometerImpl
    {
        private Accelerometer accelerometer;
        private bool isTrackingStarted;

        public AccelerometerImpl() : base()
        {
            this.accelerometer = new Accelerometer();
            this.accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            this.isTrackingStarted = false;
        }

        protected override void startTracking()
        {
            SensorState state = this.accelerometer.State;
            
            if (!this.isTrackingStarted)
            {
                this.accelerometer.Start();

                if (state == SensorState.Disabled || state == SensorState.NotSupported) {
                    notifyError(IAccelerationListener.Error.Unavailable);
                }
                else if (state == SensorState.NoPermissions)
                {
                    notifyError(IAccelerationListener.Error.Unauthorized);
                }
                else
                {
                    this.isTrackingStarted = true;
                }
            }
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            SensorState state = this.accelerometer.State;
            Acceleration acceleration = new Acceleration(e.SensorReading.Acceleration.X, e.SensorReading.Acceleration.Y, e.SensorReading.Acceleration.Z, TimeUtils.CurrentTimeMillis());
            if (state == SensorState.Ready)
            {
                notifyResult(acceleration);
            }
            else if (state == SensorState.Initializing)
            {
                notifyWarning(acceleration, IAccelerationListener.Warning.NeedsCalibration);
            }
            else if (state == SensorState.NoData)
            {
                notifyWarning(acceleration, IAccelerationListener.Warning.Stale);
            }
        }

        protected override void stopTracking()
        {
            if (isTrackingStarted)
            {
                this.accelerometer.Stop();
                this.isTrackingStarted = false;
            }
        }

        protected override bool isTracking()
        {
            return this.isTrackingStarted;
        }
    }
}
