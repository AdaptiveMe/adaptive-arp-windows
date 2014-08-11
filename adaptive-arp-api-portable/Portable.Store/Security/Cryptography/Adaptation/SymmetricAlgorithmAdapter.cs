// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using ISymmetricAlgorithm = pcl::System.Security.Cryptography.Adaptation.ISymmetricAlgorithm;
using ICryptoTransform = pcl::System.Security.Cryptography.ICryptoTransform;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT SymmetricKeyAlgorithmProvider to ISymmetricAlgorithm
    internal class SymmetricAlgorithmAdapter : ISymmetricAlgorithm
    {
        private readonly SymmetricKeyAlgorithmProvider _provider;
        private byte[] _key;
        private byte[] _iv;
        
        public SymmetricAlgorithmAdapter(string algorithmName)
        {
            Debug.Assert(algorithmName != null && algorithmName.Length > 0);

            _provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(algorithmName);
        }

        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public byte[] IV
        {
            get { return _iv; }
            set { _iv = value; }
        }

        public ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (iv == null)
                throw new ArgumentNullException("iv");

            return new SymmetricEncryptorAdapter(_provider, key, iv);
        }

        public ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (iv == null)
                throw new ArgumentNullException("iv");

            return new SymmetricDecryptorAdapter(_provider, key, iv);
        }

        public void Dispose()
        {
        }
    }
}
