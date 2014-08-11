// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography.Adaptation;
using System.Diagnostics;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Represents the base class from which all implementations of cryptographic hash algorithms must derive.
    /// </summary>
    public class HashAlgorithm : IDisposable
    {
        private readonly IHashAlgorithm _underlyingAlgorithm;

        internal HashAlgorithm(IHashAlgorithm underlyingAlgorithm)
        {
            Debug.Assert(underlyingAlgorithm != null);

            _underlyingAlgorithm = underlyingAlgorithm;
        }

        internal IHashAlgorithm UnderlyingAlgorithm
        {
            get { return _underlyingAlgorithm; }
        }

        /// <summary>
        ///     Computes the hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">
        ///     The input to compute the hash code for.
        /// </param>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="buffer"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The object has already been disposed.
        /// </exception>
        public byte[] ComputeHash(byte[] buffer)
        {
            return _underlyingAlgorithm.ComputeHash(buffer);
        }

        /// <summary>
        ///     Computes the hash value for the specified region of the specified byte array.
        /// </summary>
        /// <param name="buffer">
        ///     The input to compute the hash code for.
        /// </param>
        /// <param name="count">
        ///     The number of bytes in the array to use as data. 
        /// </param>
        /// <param name="offset">
        ///     The offset into the byte array from which to begin using data. 
        /// </param>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="buffer"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="count"/> is an invalid value.
        ///     <para>
        ///     -or-
        ///     </para>
        ///     <paramref name="buffer"/> length is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="offset"/> is out of range. This parameter requires a non-negative number.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The object has already been disposed.
        /// </exception>
        public byte[] ComputeHash(byte[] buffer, int offset, int count)
        {
            return _underlyingAlgorithm.ComputeHash(buffer, offset, count);
        }

        /// <summary>
        ///     Computes the hash value for the specified <see cref="Stream"/> object.
        /// </summary>
        /// <param name="inputStream">
        ///     The input to compute the hash code for. 
        /// </param>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The object has already been disposed.
        /// </exception>
        public byte[] ComputeHash(Stream inputStream)
        {
            return _underlyingAlgorithm.ComputeHash(inputStream);
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="HashAlgorithm"/> class.
        /// </summary>
        public void Dispose()
        {
            _underlyingAlgorithm.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="HashAlgorithm"/> class.
        /// </summary>
        public void Clear()
        {
            Dispose();
        }
    }
}
