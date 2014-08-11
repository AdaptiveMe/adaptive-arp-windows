// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;

namespace System.Security.Cryptography.Adaptation
{
    internal interface IHashAlgorithm : IDisposable
    {
        byte[] ComputeHash(byte[] buffer);
        byte[] ComputeHash(byte[] buffer, int offset, int count);
        byte[] ComputeHash(Stream inputStream);
    }
}
