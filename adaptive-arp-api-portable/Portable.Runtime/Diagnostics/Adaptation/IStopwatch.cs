using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Diagnostics.Adaptation
{
    internal interface IStopwatch
    {
        void Reset();

        bool IsRunning { get;}

        long ElapsedTicks { get;}

        long ElapsedMilliseconds { get; }

        TimeSpan Elapsed { get;}

        void Restart();

        void Start();

        void Stop();
    }
}
