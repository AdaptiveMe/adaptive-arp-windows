using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;
using Adaptive.Arp.Impl;
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
