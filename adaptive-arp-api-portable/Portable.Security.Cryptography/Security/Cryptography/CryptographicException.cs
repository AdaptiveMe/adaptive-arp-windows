// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     The exception that is thrown when an error occurs during a cryptographic operation.
    /// </summary>
    public class CryptographicException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CryptographicException"/> class with default properties. 
        /// </summary>
        public CryptographicException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptographicException"/> class with a specified error message. 
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for the exception. 
        /// </param>
        public CryptographicException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CryptographicException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for the exception. 
        /// </param>
        /// <param name="inner">
        ///     The exception that is the cause of the current exception. If the inner parameter is not <see langword="null"/>, the current exception is raised in a catch block that handles the inner exception. 
        /// </param>
        public CryptographicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
