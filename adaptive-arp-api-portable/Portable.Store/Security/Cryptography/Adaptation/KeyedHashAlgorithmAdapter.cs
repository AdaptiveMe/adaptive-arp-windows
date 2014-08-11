// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.Adaptation;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using IKeyedHashAlgorithm = pcl::System.Security.Cryptography.Adaptation.IKeyedHashAlgorithm;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT MacAlgorithmProvider to IHashAlgorithm
    internal class KeyedHashAlgorithmAdapter : HashCryptoTransformAdapter, IKeyedHashAlgorithm
    {
        private readonly MacAlgorithmProvider _provider;
        private CryptographicKey _key;

        public KeyedHashAlgorithmAdapter(string algorithmName)
        {
            Debug.Assert(algorithmName != null && algorithmName.Length > 0);

            _provider = MacAlgorithmProvider.OpenAlgorithm(algorithmName);
        }

        public void SetKey(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException("value");

            // Don't use AsBuffer here, because we want to clone the key
            var keyBuffer = WindowsRuntimeBuffer.Create(key, 0, key.Length, key.Length);

            _key = _provider.CreateKey(keyBuffer);
        }

        public override IBuffer Transform(IBuffer buffer)
        {
            Debug.Assert(buffer != null);

            return CryptographicEngine.Sign(_key, buffer);
        }
    }
}
