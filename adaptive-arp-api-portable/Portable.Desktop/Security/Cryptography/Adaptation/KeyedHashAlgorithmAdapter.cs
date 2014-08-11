// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.IO;
using IKeyedHashAlgorithm = pcl::System.Security.Cryptography.Adaptation.IKeyedHashAlgorithm;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a KeyedHashAlgorithm to a IKeyedHashAlgorithm
    internal class KeyedHashAlgorithmAdapter : HashAlgorithmAdapter, IKeyedHashAlgorithm
    {
        private readonly KeyedHashAlgorithm _underlyingAlgorithm;

        public KeyedHashAlgorithmAdapter(KeyedHashAlgorithm underlyingAlgorithm)
            : base(underlyingAlgorithm)
        {
            _underlyingAlgorithm = underlyingAlgorithm;
        }

        public void SetKey(byte[] key)
        {
            _underlyingAlgorithm.Key = key;
        }
    }
}
