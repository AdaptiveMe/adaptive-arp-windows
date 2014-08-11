// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.IO;
using IProtectedData = pcl::System.Security.Cryptography.Adaptation.IProtectedData;
using PclCryptographicException = pcl::System.Security.Cryptography.CryptographicException;

namespace System.Security.Cryptography.Adaptation
{
    internal class ProtectedDataAdapter : IProtectedData
    {
        public byte[] Protect(byte[] userData, byte[] optionalEntropy)
        {
            return CryptoServices.TranslateExceptions(() => {
                return ProtectedData.Protect(userData, optionalEntropy, DataProtectionScope.CurrentUser);
            });
            
        }

        public byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy)
        {
            return CryptoServices.TranslateExceptions(() => {
                return ProtectedData.Unprotect(encryptedData, optionalEntropy, DataProtectionScope.CurrentUser);
            });
        }
    }
}
