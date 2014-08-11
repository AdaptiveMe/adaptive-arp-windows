// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Defines the basic operations of cryptographic transformations.
    /// </summary>
    public interface ICryptoTransform : IDisposable
    {
        /// <summary>
        ///     Transforms the specified region of the input byte array and copies the resulting transform to the specified region of the output byte array.
        /// </summary>
        /// <param name="inputBuffer">
        ///     The input for which to compute the transform. 
        /// </param>
        /// <param name="inputOffset">
        ///     The offset into the input byte array from which to begin using data. 
        /// </param>
        /// <param name="inputCount">
        ///     The number of bytes in the input byte array to use as data. 
        /// </param>
        /// <param name="outputBuffer">
        ///     The output to which to write the transform. 
        /// </param>
        /// <param name="outputOffset">
        ///     The offset into the output byte array from which to begin writing data. 
        /// </param>
        /// <returns>
        ///     The number of bytes written.
        /// </returns>
        int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

        /// <summary>
        ///     Transforms the specified region of the specified byte array.
        /// </summary>
        /// <param name="inputBuffer">
        ///     The input for which to compute the transform. 
        /// </param>
        /// <param name="inputOffset">
        ///     The offset into the input byte array from which to begin using data. 
        /// </param>
        /// <param name="inputCount">
        ///     The number of bytes in the input byte array to use as data. 
        /// </param>
        /// <returns>
        ///     The computed transform.
        /// </returns>
        byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);

        /// <summary>
        ///     Gets a value indicating whether the current transform can be reused.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the current transform can be reused; otherwise, <see langword="false"/>.
        /// </value>
        bool CanReuseTransform { get; }

        /// <summary>
        ///     Gets a value indicating whether multiple blocks can be transformed
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if multiple blocks can be transformed; otherwise, <see langword="false"/>.
        /// </value>
        bool CanTransformMultipleBlocks { get; }

        /// <summary>
        ///     Gets the size of the input block size.
        /// </summary>
        /// <value>
        ///     The size of the input data blocks in bytes.
        /// </value>
        int InputBlockSize { get; }

        /// <summary>
        ///     Gets the size of the output block size.
        /// </summary>
        /// <value>
        ///     The size of the output data blocks in bytes.
        /// </value>
        int OutputBlockSize { get; }

    }
}
