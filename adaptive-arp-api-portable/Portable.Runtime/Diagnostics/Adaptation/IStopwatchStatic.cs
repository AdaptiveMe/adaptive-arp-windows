using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Diagnostics.Adaptation
{
    internal interface IStopwatchStatic
    {
        long Frequency
        {
            get;
        }

        bool IsHighResolution
        {
            get;
        }

        long GetTimestamp();

    }
}
