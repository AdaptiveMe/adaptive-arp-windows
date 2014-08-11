// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;

namespace System.Security.Cryptography.Adaptation
{
    internal interface IProtectedData
    {
        byte[] Protect(byte[] userData, byte[] optionalEntropy);
        byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy);
    }
}
