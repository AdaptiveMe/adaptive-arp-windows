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

namespace System.Diagnostics
{
    internal class Stopwatch : IStopwatch
    {
        private bool _isRunning;
        private long _elapsedTicks;
        private long _startTimestamp;

        public Stopwatch()
        {
        }

        public TimeSpan Elapsed
        {
            get { return new TimeSpan(ElapsedTicks); }
        }

        public long ElapsedMilliseconds
        {
            get { return ElapsedTicks / TimeSpan.TicksPerMillisecond; }
        }

        public long ElapsedTicks
        {
            get
            {
                long elapsedTicks = _elapsedTicks;
                if (_isRunning)
                {
                    elapsedTicks += ElapsedTicksSinceLastStart;
                }

                return elapsedTicks;
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        private long ElapsedTicksSinceLastStart
        {
            get { return GetTimestamp() - _startTimestamp; }
        }

        public void Start()
        {
            if (!_isRunning)
            {
                _startTimestamp = GetTimestamp();
                _isRunning = true;
            }            
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _elapsedTicks += ElapsedTicksSinceLastStart;
                _isRunning = false;
            }
        }

        public void Restart()
        {
            Reset();
            Start();            
        }

        public static long GetTimestamp()
        {
            return DateTime.UtcNow.Ticks;
        }

        public void Reset()
        {
            _startTimestamp = 0;
            _elapsedTicks = 0;
            _isRunning = false;
        }
    }
}
