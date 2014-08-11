// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Security.Cryptography.Adaptation;
using System.Diagnostics;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Represents the abstract base class from which all implementations of symmetric algorithms must inherit.
    /// </summary>
    public class SymmetricAlgorithm
    {
        private readonly ISymmetricAlgorithm _underlyingAlgorithm;

        internal SymmetricAlgorithm(ISymmetricAlgorithm underlyingAlgorithm)
        {
            Debug.Assert(underlyingAlgorithm != null);

            _underlyingAlgorithm = underlyingAlgorithm;
        }

        internal ISymmetricAlgorithm UnderlyingAlgorithm
        {
            get { return _underlyingAlgorithm; }
        }

        /// <summary>
        ///     Gets or sets the initialization vector (IV) for the symmetric algorithm.
        /// </summary>
        /// <value>
        ///     The initialization vector (IV) for the symmetric algorithm.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="CryptographicException">
        ///     The length of <paramref name="value"/> is not equal to the block size.
        /// </exception>
        public byte[] IV
        {
            get { return _underlyingAlgorithm.IV; }
            set { _underlyingAlgorithm.IV = value; }
        }

        /// <summary>
        ///     Gets or sets the secret key for the symmetric algorithm.
        /// </summary>
        /// <value>
        ///     The secret key to use for the symmetric algorithm.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="CryptographicException">
        ///     The length of <paramref name="value"/> is not of a valid size.
        /// </exception>
        public byte[] Key
        {
            get { return _underlyingAlgorithm.Key; }
            set { _underlyingAlgorithm.Key = value; }
        }

        /// <summary>
        ///     Creates a symmetric encryptor object with the current <see cref="Key"/> and initialization vector (<see cref="IV"/>).
        /// </summary>
        /// <returns>
        ///     A symmetric encryptor object.
        /// </returns>
        public ICryptoTransform CreateEncryptor()
        {
            return _underlyingAlgorithm.CreateEncryptor(Key, IV);
        }

        /// <summary>
        ///     Creates a symmetric encryptor object with the specified key and initialization vector (IV).
        /// </summary>
        /// <returns>
        ///     A symmetric encryptor object.
        /// </returns>
        /// <param name="iv">
        ///     The secret key to use for the symmetric algorithm. 
        /// </param>
        /// <param name="key">
        ///     The initialization vector to use for the symmetric algorithm. 
        /// </param>
        /// <exception cref="CryptographicException">
        ///     The length of <paramref name="iv"/> is not equal to the block size.
        /// </exception>
        public ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
        {
            return _underlyingAlgorithm.CreateEncryptor(key, iv);
        }

        /// <summary>
        ///     Creates a symmetric decryptor object with the current <see cref="Key"/> and initialization vector (<see cref="IV"/>).
        /// </summary>
        /// <returns>
        ///     A symmetric decryptor object.
        /// </returns>
        public ICryptoTransform CreateDecryptor()
        {
            return _underlyingAlgorithm.CreateDecryptor(Key, IV);
        }

        /// <summary>
        ///     Creates a symmetric decryptor object with the specified key and initialization vector (IV).
        /// </summary>
        /// <returns>
        ///     A symmetric decryptor object.
        /// </returns>
        /// <param name="iv">
        ///     The secret key to use for the symmetric algorithm. 
        /// </param>
        /// <param name="key">
        ///     The initialization vector to use for the symmetric algorithm. 
        /// </param>
        /// <exception cref="CryptographicException">
        ///     The length of <paramref name="iv"/> is not equal to the block size.
        /// </exception>
        public ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
        {
            return _underlyingAlgorithm.CreateDecryptor(key, iv);
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="SymmetricAlgorithm"/> class.
        /// </summary>
        public void Dispose()
        {
            _underlyingAlgorithm.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="SymmetricAlgorithm"/> class.
        /// </summary>
        public void Clear()
        {
            Dispose();
        }
    }
}
