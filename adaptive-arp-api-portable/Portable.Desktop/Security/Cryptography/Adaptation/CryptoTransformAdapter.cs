// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using ISymmetricAlgorithm = pcl::System.Security.Cryptography.Adaptation.ISymmetricAlgorithm;
using IPclCryptoTransform = pcl::System.Security.Cryptography.ICryptoTransform;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a .NET ICryptoTransform to a PclContrib ICryptoTransform
    internal class CryptoTransformAdapter : IPclCryptoTransform
    {
        private readonly ICryptoTransform _underlyingTransform;

        public CryptoTransformAdapter(ICryptoTransform underlyingTransform)
        {
            Debug.Assert(underlyingTransform != null);

            _underlyingTransform = underlyingTransform;
        }

        public bool CanReuseTransform
        {
            get { return _underlyingTransform.CanReuseTransform; }
        }

        public bool CanTransformMultipleBlocks
        {
            get { return _underlyingTransform.CanTransformMultipleBlocks; }
        }

        public int InputBlockSize
        {
            get { return _underlyingTransform.InputBlockSize; }
        }

        public int OutputBlockSize
        {
            get { return _underlyingTransform.OutputBlockSize; }
        }

        public ICryptoTransform UnderlyingTransform
        {
            get { return _underlyingTransform; }
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return CryptoServices.TranslateExceptions(() => {
                return _underlyingTransform.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
            });
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {   
            return CryptoServices.TranslateExceptions(() => {
                return _underlyingTransform.TransformFinalBlock(inputBuffer, inputOffset, inputCount);
            });
        }

        public void Dispose()
        {
            _underlyingTransform.Dispose();
        }
    }
}
