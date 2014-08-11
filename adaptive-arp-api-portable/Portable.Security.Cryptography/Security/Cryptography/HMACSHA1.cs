// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Adaptation;
using System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Computes a Hash-based Message Authentication Code (HMAC) using the SHA-1 hash function. 
    /// </summary>
    public class HMACSHA1 : KeyedHashAlgorithm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HMACSHA1"/> class with the specified key data.
        /// </summary>
        /// <param name="key">
        ///     The secret key for HMACSHA1 encryption. The key can be any length, but if it is more than 64 bytes long it will be hashed (using SHA-1) to derive a 64-byte key. Therefore, the recommended size of the secret key is 64 bytes. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public HMACSHA1(byte[] key)
            : base(PlatformAdapter.Resolve<ICryptographyFactory>().CreateHMacSha1(), key)
        {
        }
    }
}
