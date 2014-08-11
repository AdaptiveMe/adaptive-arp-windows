// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using pcl::System.Diagnostics.Adaptation;

namespace System.Diagnostics.Adaptation
{
    internal class StopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Create()
        {
            return new Stopwatch();
        }
    }
}
