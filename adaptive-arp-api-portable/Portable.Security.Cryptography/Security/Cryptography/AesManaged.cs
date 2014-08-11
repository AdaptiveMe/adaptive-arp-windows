// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Security.Cryptography.Adaptation;
using System.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm.
    /// </summary>
    public sealed class AesManaged : SymmetricAlgorithm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AesManaged"/> class.
        /// </summary>
        public AesManaged()
            : base(PlatformAdapter.Resolve<ICryptographyFactory>().CreateAesManaged())
        {
        }
    }
}
