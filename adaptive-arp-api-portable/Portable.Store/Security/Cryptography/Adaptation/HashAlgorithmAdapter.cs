// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using IHashAlgorithm = pcl::System.Security.Cryptography.Adaptation.IHashAlgorithm;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT HashAlgorithmProvider to IHashAlgorithm
    internal class HashAlgorithmAdapter : HashCryptoTransformAdapter
    {
        private readonly HashAlgorithmProvider _provider;

        public HashAlgorithmAdapter(string algorithmName)
        {
            Debug.Assert(algorithmName != null && algorithmName.Length > 0);

            _provider = HashAlgorithmProvider.OpenAlgorithm(algorithmName);
        }

        public override byte[] ComputeHash(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            CryptographicHash hash = _provider.CreateHash();

            byte[] buffer = new byte[4096];

            int bytesRead;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                hash.Append(buffer.AsBuffer(0, bytesRead));
            }

            var hashed = hash.GetValueAndReset();

            return hashed.ToArray();
        }

        public override IBuffer Transform(IBuffer buffer)
        {
            Debug.Assert(buffer != null);

            // NOTE: The use of the reusable hash instead of calling ComputeHash - this is because 
            // ComputeHash gives a different behavior (it throws) when handed an empty buffer

            CryptographicHash hash = _provider.CreateHash();

            if (buffer.Length > 0)
                hash.Append(buffer);

            return hash.GetValueAndReset();
        }
    }
}
