using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Impl;
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
            this.isTrackingStarted = false;
        }

        protected override void startTracking()
        {
            if (!this.isTrackingStarted)
            {
                this.accelerometer.Start();
                this.isTrackingStarted = true;
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
