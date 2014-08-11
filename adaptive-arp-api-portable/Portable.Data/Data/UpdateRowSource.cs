// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data
{
    public enum UpdateRowSource
    {
        None = 0,
        OutputParameters = 1,
        FirstReturnedRecord = 2,
        Both = 3
    }
}
