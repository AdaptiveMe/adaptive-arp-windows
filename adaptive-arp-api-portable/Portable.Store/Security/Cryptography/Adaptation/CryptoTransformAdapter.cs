// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using ICryptoTransform = pcl::System.Security.Cryptography.ICryptoTransform;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT hash or crypto algorithm to a ICryptoTransform
    internal abstract class CryptoTransformAdapter : ICryptoTransform
    {
        private static readonly byte[] Empty = new byte[0];

        protected CryptoTransformAdapter()
        {
        }

        public abstract int InputBlockSize
        {
            get;
        }

        public abstract int OutputBlockSize
        {
            get;
        }

        public bool CanReuseTransform
        {
            get { return true; }
        }

        public bool CanTransformMultipleBlocks
        {
            get { return false; }
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            if (inputBuffer == null)
                throw new ArgumentNullException("inputBuffer");

            if (inputOffset < 0)
                throw new ArgumentOutOfRangeException("inputOffset");
            
            if ((inputCount < 0) || (inputCount > inputBuffer.Length))
                throw new ArgumentException("inputCount");
            
            if ((inputBuffer.Length - inputCount) < inputOffset)
                throw new ArgumentException();

            IBuffer transformed = Transform(inputBuffer.AsBuffer(inputOffset, inputCount));

            uint bytesWritten = transformed.Length;

            Debug.Assert(bytesWritten <= Int32.MaxValue);

            transformed.CopyTo(0, outputBuffer, outputOffset, (int)bytesWritten);

            return (int)bytesWritten;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            return Empty;
        }

        public abstract IBuffer Transform(IBuffer buffer);

        public void Dispose()
        {
        }
    }
}
