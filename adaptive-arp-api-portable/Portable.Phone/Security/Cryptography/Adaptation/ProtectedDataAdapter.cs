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
            try
            {
                return ProtectedData.Protect(userData, optionalEntropy);
            }
            catch (CryptographicException ex)
            {
                throw new PclCryptographicException(ex.Message, ex.InnerException);
            }
        }

        public byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy)
        {
            try
            {
                return ProtectedData.Unprotect(encryptedData, optionalEntropy);
            }
            catch (CryptographicException ex)
            {
                throw new PclCryptographicException(ex.Message, ex.InnerException);
            }
        }
    }
}
