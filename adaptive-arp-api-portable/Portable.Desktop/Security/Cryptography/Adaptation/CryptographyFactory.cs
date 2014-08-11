// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.Security.Cryptography;
using pcl::System.Security.Cryptography.Adaptation;

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
            return new SymmetricAlgorithmAdapter(new AesManaged());
        }

        public IHashAlgorithm CreateSha256Managed()
        {
            return new HashAlgorithmAdapter(new SHA256Managed());
        }

        public IHashAlgorithm CreateSha1Managed()
        {
            return new HashAlgorithmAdapter(new SHA1Managed());
        }

        public IKeyedHashAlgorithm CreateHMacSha256()
        {
            return new KeyedHashAlgorithmAdapter(new HMACSHA256());
        }

        public IKeyedHashAlgorithm CreateHMacSha1()
        {
            return new KeyedHashAlgorithmAdapter(new HMACSHA1());
        }

        public IDeriveBytes CreateRfc2898DeriveBytes(string password, byte[] salt, int iterations)
        {
            return new Rfc2898DeriveBytesAdapter(new Rfc2898DeriveBytes(password, salt, iterations));
        }
    }
}
