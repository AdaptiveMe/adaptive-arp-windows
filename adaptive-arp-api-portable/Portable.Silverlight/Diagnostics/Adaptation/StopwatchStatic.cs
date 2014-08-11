// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using pcl::System.Diagnostics.Adaptation;

namespace System.Diagnostics.Adaptation
{
    internal class StopwatchStatic : IStopwatchStatic
    {
        public StopwatchStatic()
        {
        }

        public long Frequency
        {
            get { return 10000000; }
        }

        public bool IsHighResolution
        {
            get { return false; }
        }

        public long GetTimestamp()
        {
            return DateTime.Now.Ticks;
        }
    }
}
