// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.Security.Cryptography;
using pcl::System.Security.Cryptography.Adaptation;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;

namespace System.Security.Cryptography.Adaptation
{
    // NOTE: Do not merge this other factories - they need to be self contained so that 
    // absense of the assemblies that contain other factory interfaces do not prevent
    // this factory from loading.
    internal class CryptographyFactory : ICryptographyFactory
    {
        public CryptographyFactory()
        {
        }

        public ISymmetricAlgorithm CreateAesManaged()
        {
            return new SymmetricAlgorithmAdapter(SymmetricAlgorithmNames.AesCbcPkcs7);
        }

        public IHashAlgorithm CreateSha256Managed()
        {
            return new HashAlgorithmAdapter(HashAlgorithmNames.Sha256);
        }

        public IHashAlgorithm CreateSha1Managed()
        {
            return new HashAlgorithmAdapter(HashAlgorithmNames.Sha1);
        }

        public IKeyedHashAlgorithm CreateHMacSha256()
        {
            return new KeyedHashAlgorithmAdapter(MacAlgorithmNames.HmacSha256);
        }

        public IKeyedHashAlgorithm CreateHMacSha1()
        {
            return new KeyedHashAlgorithmAdapter(MacAlgorithmNames.HmacSha1);
        }

        public IDeriveBytes CreateRfc2898DeriveBytes(string password, byte[] salt, int iterations)
        {
            var deriveBytes = new Rfc2898DeriveBytesAdapter(KeyDerivationAlgorithmNames.Pbkdf2Sha1, password);
            deriveBytes.Salt = salt;
            deriveBytes.IterationCount = iterations;

            return deriveBytes;
        }
    }
}
