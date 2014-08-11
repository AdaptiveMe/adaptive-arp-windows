// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Security.Cryptography.Adaptation
{
    internal interface ISymmetricAlgorithm : IDisposable
    {
        byte[] Key
        {
            get;
            set;
        }

        byte[] IV
        {
            get;
            set;
        }

        ICryptoTransform CreateEncryptor(byte[] key, byte[] iv);
        ICryptoTransform CreateDecryptor(byte[] key, byte[] iv);        
    }
}
