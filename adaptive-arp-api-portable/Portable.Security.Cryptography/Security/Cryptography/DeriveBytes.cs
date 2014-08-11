// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Security.Cryptography.Adaptation;
using System.Diagnostics;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Represents the abstract base class from which all classes that derive byte sequences of a specified length inherit.
    /// </summary>
    public abstract class DeriveBytes : IDisposable
    {
        private readonly IDeriveBytes _underlyingDeriveBytes;

        internal DeriveBytes(IDeriveBytes underlyingDeriveBytes)
        {
            Debug.Assert(underlyingDeriveBytes != null);

            _underlyingDeriveBytes = underlyingDeriveBytes;
        }

        /// <summary>
        ///     Gets or sets the number of iterations for the operation.
        /// </summary>
        /// <value>
        ///     The number of iterations for the operation.
        /// </value>
        /// <exception cref="ArgumentException">
        ///     The number of iterations is less than 1. 
        /// </exception>
        public int IterationCount
        {
            get { return _underlyingDeriveBytes.IterationCount; }
            set { _underlyingDeriveBytes.IterationCount = value; }
        }

        /// <summary>
        ///     Gets or sets the key salt value for the operation.
        /// </summary>
        /// <value>
        ///     The key salt value for the operation.
        /// </value>
        /// <exception cref="ArgumentException">
        ///     The specified salt size is smaller than 8 bytes.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        public byte[] Salt
        {
            get { return _underlyingDeriveBytes.Salt; }
            set { _underlyingDeriveBytes.Salt = value; }
        }

        /// <summary>
        ///     Releases all resources used by the current instance of the <see cref="DeriveBytes"/> class.
        /// </summary>
        public void Dispose()
        {
            _underlyingDeriveBytes.Dispose();
        }

        /// <summary>
        ///     Returns the pseudo-random key for this object.
        /// </summary>
        /// <param name="byteCount">
        ///     The number of pseudo-random key bytes to generate. 
        /// </param>
        /// <returns>
        ///     A byte array filled with pseudo-random key bytes.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="byteCount"/> is less than 0.
        /// </exception>
        public byte[] GetBytes(int byteCount)
        {
            return _underlyingDeriveBytes.GetBytes(byteCount);
        }

        /// <summary>
        ///     Resets the state of the operation.
        /// </summary>
        public void Reset()
        {
            _underlyingDeriveBytes.Reset();
        }
    }
}
