// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;

namespace System.Security.Cryptography.Adaptation
{
    internal interface IProtectedDataFactory
    {
        IProtectedData CreateProtectedData();
    }
}
