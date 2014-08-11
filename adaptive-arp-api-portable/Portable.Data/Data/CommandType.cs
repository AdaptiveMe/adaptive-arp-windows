// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data
{
    public enum CommandType
    {
        Text = 1,
        StoredProcedure = 4,
        TableDirect = 512,
    }
}
