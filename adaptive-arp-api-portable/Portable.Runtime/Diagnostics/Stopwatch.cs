// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics.Adaptation;
using System.Adaptation;

namespace System.Diagnostics
{
    /// <summary>
    ///     Provides a set of methods and properties that you can use to accurately measure elapsed time.
    /// </summary>
    public class Stopwatch
    {
        private readonly IStopwatch _underlyingStopwatch;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Stopwatch"/> class.
        /// </summary>
        public Stopwatch()
        {
            _underlyingStopwatch = PlatformAdapter.Resolve<IStopwatchFactory>().Create();
        }

        /// <summary>
        ///     Gets the frequency of the timer as the number of ticks per second.
        /// </summary>
        /// <value>
        ///     A <see cref="Int64"/> containing the number of ticks per second.
        /// </value>
        public static long Frequency
        {
            get
            {
                var stopWatch = PlatformAdapter.Resolve<IStopwatchStatic>();

                return stopWatch.Frequency;
            }
        }

        /// <summary>
        ///     Indicates whether the timer is based on a high-resolution performance counter.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the timer is based on a high-resolution performance counter, otherwise, <see langword="false"/>.
        /// </value>
        public static bool IsHighResolution
        {
            get
            {
                var stopWatch = PlatformAdapter.Resolve<IStopwatchStatic>();

                return stopWatch.IsHighResolution;
            }

        }

        /// <summary>
        ///     Gets the total elapsed time measured by the current instance.
        /// </summary>
        /// <value>
        ///     A read-only <see cref="TimeSpan"/> representing the total elapsed time measured by the current instance.
        /// </value>
        public TimeSpan Elapsed
        {
            get { return _underlyingStopwatch.Elapsed; }
        }

        /// <summary>
        ///     Gets the total elapsed time measured by the current instance, in milliseconds.
        /// </summary>
        /// <value>
        ///     A read-only long integer representing the total number of milliseconds measured by the current instance.    
        /// </value>
        public long ElapsedMilliseconds
        {
            get { return _underlyingStopwatch.ElapsedMilliseconds; }
        }

        /// <summary>
        ///     Gets the total elapsed time measured by the current instance, in timer ticks.
        /// </summary>
        /// <value>
        ///     A read-only long integer representing the total number of timer ticks measured by the current instance.
        /// </value>
        public long ElapsedTicks
        {
            get { return _underlyingStopwatch.ElapsedTicks; }
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="Stopwatch"/> timer is running.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the <see cref="Stopwatch"/> instance is currently running and measuring elapsed time for an interval; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsRunning
        {
            get { return _underlyingStopwatch.IsRunning; }
        }

        /// <summary>
        ///     Gets the current number of ticks in the timer mechanism.
        /// </summary>
        /// <returns>
        ///     A long integer representing the tick counter value of the underlying timer mechanism.
        /// </returns>
        public static long GetTimestamp()
        {
            return PlatformAdapter.Resolve<IStopwatchStatic>().GetTimestamp();
        }

        /// <summary>
        ///     Stops time interval measurement and resets the elapsed time to zero. 
        /// </summary>
        public void Reset()
        {
            _underlyingStopwatch.Reset();
        }

        /// <summary>
        ///     Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        public void Restart()
        {
            _underlyingStopwatch.Restart();
        }

        /// <summary>
        ///     Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        public void Start()
        {
            _underlyingStopwatch.Start();
        }

        /// <summary>
        ///     Initializes a new Stopwatch instance, sets the elapsed time property to zero, and starts measuring elapsed time.
        /// </summary>
        /// <returns>
        ///     A <see cref="Stopwatch"/> that has just begun measuring elapsed time.
        /// </returns>
        public static Stopwatch StartNew()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            return watch;
        }

        /// <summary>
        ///     Stops measuring elapsed time for an interval.
        /// </summary>
        public void Stop()
        {
            _underlyingStopwatch.Stop();
        }
    }
}
