// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using ICryptoTransform = pcl::System.Security.Cryptography.ICryptoTransform;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT SymmetricKeyAlgorithmProvider to a decrypting ICryptoTransform
    internal class SymmetricDecryptorAdapter : SymmetricCryptoTransformAdapter
    {
        public SymmetricDecryptorAdapter(SymmetricKeyAlgorithmProvider provider, byte[] key, byte[] iv)
            : base(provider, key, iv)
        {
        }

        public override IBuffer Transform(IBuffer buffer)
        {
            return CryptographicEngine.Decrypt(Key, buffer, IV);
        }
    }
}
