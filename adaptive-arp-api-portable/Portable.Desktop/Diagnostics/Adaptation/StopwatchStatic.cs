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
            get { return Stopwatch.Frequency; }
        }

        public bool IsHighResolution
        {
            get { return Stopwatch.IsHighResolution; }
        }

        public long GetTimestamp()
        {
            return Stopwatch.GetTimestamp();
        }
    }
}
