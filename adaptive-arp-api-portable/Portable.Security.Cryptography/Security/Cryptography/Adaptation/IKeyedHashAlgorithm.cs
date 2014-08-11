// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;

namespace System.Security.Cryptography.Adaptation
{
    internal interface IKeyedHashAlgorithm : IHashAlgorithm
    {
        void SetKey(byte[] key);
    }
}
