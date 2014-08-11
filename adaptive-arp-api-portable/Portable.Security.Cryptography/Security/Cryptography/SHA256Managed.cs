// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Adaptation;
using System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Computes the SHA-256 hash for the input data using the managed library.
    /// </summary>
    public class SHA256Managed : HashAlgorithm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SHA256Managed"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     The Federal Information Processing Standards (FIPS) security setting is enabled. This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.
        /// </exception>
        public SHA256Managed()
            : base(PlatformAdapter.Resolve<ICryptographyFactory>().CreateSha256Managed())
        {
        }
    }
}
