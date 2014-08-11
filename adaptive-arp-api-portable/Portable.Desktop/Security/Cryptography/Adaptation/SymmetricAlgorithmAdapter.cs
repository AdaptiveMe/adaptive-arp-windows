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
    // Adapts a SymmetricAlgorithm to a ISymmetricAlgorithm
    internal class SymmetricAlgorithmAdapter : ISymmetricAlgorithm
    {
        private readonly SymmetricAlgorithm _underlyingAlgorithm;

        public SymmetricAlgorithmAdapter(SymmetricAlgorithm underlyingAlgorithm)
        {
            Debug.Assert(underlyingAlgorithm != null);

            _underlyingAlgorithm = underlyingAlgorithm;
        }

        internal SymmetricAlgorithm UnderlyingAlgorithm
        {
            get { return _underlyingAlgorithm; }
        }

        public byte[] IV
        {
            get { return _underlyingAlgorithm.IV; }
            set { CryptoServices.TranslateExceptions(() => _underlyingAlgorithm.IV = value); }
        }


        public byte[] Key
        {
            get { return _underlyingAlgorithm.Key; }
            set { CryptoServices.TranslateExceptions(() => _underlyingAlgorithm.Key = value); }
        }

        public int KeySize
        {
            get { return _underlyingAlgorithm.KeySize; }
            set { CryptoServices.TranslateExceptions(() => _underlyingAlgorithm.KeySize = value); }
        }

        public IPclCryptoTransform CreateEncryptor(byte[] key, byte[] iv)
        {
            return CryptoServices.TranslateExceptions(() => {
                var underlyingTransform = _underlyingAlgorithm.CreateEncryptor(key, iv);

                return new CryptoTransformAdapter(underlyingTransform);
            });
        }

        public IPclCryptoTransform CreateDecryptor(byte[] key, byte[] iv)
        {
            return CryptoServices.TranslateExceptions(() => {
                var underlyingTransform = _underlyingAlgorithm.CreateDecryptor(key, iv);

                return new CryptoTransformAdapter(underlyingTransform);
            });
        }

        public void Dispose()
        {
            _underlyingAlgorithm.Clear();
        }
    }
}
