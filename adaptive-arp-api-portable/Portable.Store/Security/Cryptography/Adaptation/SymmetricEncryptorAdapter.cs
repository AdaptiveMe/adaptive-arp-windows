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
    // Adapts a WinRT SymmetricKeyAlgorithmProvider to an encrypting ICryptoTransform
    internal class SymmetricEncryptorAdapter : SymmetricCryptoTransformAdapter
    {
        public SymmetricEncryptorAdapter(SymmetricKeyAlgorithmProvider provider, byte[] key, byte[] iv)
            : base(provider, key, iv)
        {
        }

        public override IBuffer Transform(IBuffer buffer)
        {
            return CryptographicEngine.Encrypt(Key, buffer, IV);
        }
    }
}
