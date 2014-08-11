// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Text;
using System.Adaptation;
using System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on HMACSHA1.
    /// </summary>
    public class Rfc2898DeriveBytes : DeriveBytes
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Rfc2898DeriveBytes"/> class using a password and salt to derive the key.
        /// </summary>
        /// <param name="password">
        ///     The password used to derive the key. 
        /// </param>
        /// <param name="salt">
        ///     The key salt used to derive the key. 
        /// </param>
        /// <exception cref="ArgumentException">
        ///     The specified salt size is smaller than 8 bytes.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="password"/> is <see langword="null"/>.
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <paramref name="salt"/> is <see langword="null"/>.
        /// </exception>
        public Rfc2898DeriveBytes(string password, byte[] salt)
            : this(password, salt, 1000)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Rfc2898DeriveBytes"/> class using a password, a salt, and number of iterations to derive the key.
        /// </summary>
        /// <param name="password">
        ///     The password used to derive the key. 
        /// </param>
        /// <param name="salt">
        ///     The key salt used to derive the key. 
        /// </param>
        /// <param name="iterations">
        ///     The number of iterations for the operation. 
        /// </param>
        /// <exception cref="ArgumentException">
        ///     The specified salt size is smaller than 8 bytes or the iteration count is less than 1. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="password"/> is <see langword="null"/>.
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <paramref name="salt"/> is <see langword="null"/>.
        /// </exception>
        public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
            : base(PlatformAdapter.Resolve<ICryptographyFactory>().CreateRfc2898DeriveBytes(password, salt, iterations))
        {
        }
    }
}
