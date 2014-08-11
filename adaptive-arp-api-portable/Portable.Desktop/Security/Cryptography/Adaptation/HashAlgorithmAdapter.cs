// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using IHashAlgorithm = pcl::System.Security.Cryptography.Adaptation.IHashAlgorithm;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a HashAlgorithm to a IHashAlgorithm
    internal class HashAlgorithmAdapter : IHashAlgorithm
    {
        private readonly HashAlgorithm _underlyingAlgorithm;

        public HashAlgorithmAdapter(HashAlgorithm underlyingAlgorithm)
        {
            Debug.Assert(underlyingAlgorithm != null);

            _underlyingAlgorithm = underlyingAlgorithm;
        }

        internal HashAlgorithm UnderlyingAlgorithm
        {
            get { return _underlyingAlgorithm; }
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            return _underlyingAlgorithm.ComputeHash(buffer);
        }

        public byte[] ComputeHash(byte[] buffer, int offset, int count)
        {
            return _underlyingAlgorithm.ComputeHash(buffer, offset, count);
        }

        public byte[] ComputeHash(Stream inputStream)
        {
            return _underlyingAlgorithm.ComputeHash(inputStream);
        }

        public void Dispose()
        {
            _underlyingAlgorithm.Clear();
        }
    }
}
