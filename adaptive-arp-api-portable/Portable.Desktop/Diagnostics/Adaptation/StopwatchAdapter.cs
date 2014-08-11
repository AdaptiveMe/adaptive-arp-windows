// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using pcl::System.Diagnostics.Adaptation;

namespace System.Diagnostics.Adaptation
{
    internal class StopwatchAdapter : IStopwatch
    {
        private readonly Stopwatch _underlyingStopwatch = new Stopwatch();

        public StopwatchAdapter()
        {
        }

        public bool IsRunning 
        {
            get { return _underlyingStopwatch.IsRunning; }
        }

        public long ElapsedTicks 
        {
            get { return _underlyingStopwatch.ElapsedTicks; }
        }

        public long ElapsedMilliseconds 
        {
            get { return _underlyingStopwatch.ElapsedMilliseconds; }
        }

        public TimeSpan Elapsed 
        {
            get { return _underlyingStopwatch.Elapsed; }
        }

        public void Reset()
        {
            _underlyingStopwatch.Reset();
        }

        public void Restart()
        {
            _underlyingStopwatch.Reset();
            _underlyingStopwatch.Start();
        }

        public void Start()
        {
            _underlyingStopwatch.Start();
        }

        public void Stop()
        {
            _underlyingStopwatch.Stop();
        }
    }
}
