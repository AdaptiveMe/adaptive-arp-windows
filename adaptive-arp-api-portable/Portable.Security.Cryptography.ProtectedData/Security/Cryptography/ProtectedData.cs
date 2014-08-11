// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Adaptation;
using System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Provides methods for encrypting and decrypting data.
    /// </summary>
    public static class ProtectedData
    {
        private static IProtectedData UnderlyingProtectedData
        {
            get 
            { 
                var factory = PlatformAdapter.Resolve<IProtectedDataFactory>();

                return factory.CreateProtectedData();
            }
        }

        /// <summary>
        ///     Encrypts the data in a specified byte array and returns a byte array that contains the encrypted data.
        /// </summary>
        /// <param name="userData">
        ///     A byte array that contains data to encrypt. 
        /// </param>
        /// <param name="optionalEntropy">
        ///     An optional additional byte array used to increase the complexity of the encryption, or <see langword="null"/> for no additional complexity.
        /// </param>
        /// <returns>
        ///     A byte array representing the encrypted data.
        /// </returns>
        /// <remarks>
        ///     The protected data is associated with the current user.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="userData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="CryptographicException">
        ///     The encryption failed.
        /// </exception>
        public static byte[] Protect(byte[] userData, byte[] optionalEntropy)
        {
            return UnderlyingProtectedData.Protect(userData, optionalEntropy);
        }

        /// <summary>
        ///     Decrypts the data in a specified byte array and returns a byte array that contains the decrypted data.
        /// </summary>
        /// <param name="encryptedData">
        ///     A byte array containing data encrypted using the <see cref="Protect"/> method. 
        /// </param>
        /// <param name="optionalEntropy">
        ///     An optional additional byte array that was used to encrypt the data, or <see langword="null"/> if the additional byte array was not used.
        /// </param>
        /// <returns>
        ///     A byte array representing the decrypted data.
        /// </returns>
        /// <remarks>
        ///     The protected data is associated with the current user.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="encryptedData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="CryptographicException">
        ///     The decryption failed.
        /// </exception>
        public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy)
        {
            return UnderlyingProtectedData.Unprotect(encryptedData, optionalEntropy);
        }


 
    }
}
