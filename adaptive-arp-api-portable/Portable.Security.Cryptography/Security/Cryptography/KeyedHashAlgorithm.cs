// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Represents the abstract class from which all implementations of keyed hash algorithms must derive.
    /// </summary>
    public class KeyedHashAlgorithm : HashAlgorithm
    {
        internal KeyedHashAlgorithm(IKeyedHashAlgorithm underlyingAlgorithm, byte[] key)
            : base(underlyingAlgorithm)
        {
            underlyingAlgorithm.SetKey(key);
        }
    }
}
