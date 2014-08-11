// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Security.Cryptography.Adaptation
{
    internal interface IDeriveBytes : IDisposable
    {
        int IterationCount
        {
            get;
            set;
        }

        byte[] Salt
        {
            get;
            set;
        }

        byte[] GetBytes(int cb);

        void Reset();
    }
}
