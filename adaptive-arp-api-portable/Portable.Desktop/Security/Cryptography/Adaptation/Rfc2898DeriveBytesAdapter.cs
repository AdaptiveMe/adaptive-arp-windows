// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System.Diagnostics;
using System.IO;
using pcl::System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a HashDeriveBytes to a IHashDeriveBytes
    internal class Rfc2898DeriveBytesAdapter : IDeriveBytes
    {
        private readonly Rfc2898DeriveBytes _underlyingDeriveBytes;

        public Rfc2898DeriveBytesAdapter(Rfc2898DeriveBytes underlyingDeriveBytes)
        {
            Debug.Assert(underlyingDeriveBytes != null);

            _underlyingDeriveBytes = underlyingDeriveBytes;
        }

        internal DeriveBytes UnderlyingDeriveBytes
        {
            get { return _underlyingDeriveBytes; }
        }

        public int IterationCount
        {
            get { return _underlyingDeriveBytes.IterationCount; }
            set { _underlyingDeriveBytes.IterationCount = value; }
        }

        public byte[] Salt
        {
            get { return _underlyingDeriveBytes.Salt; }
            set { _underlyingDeriveBytes.Salt = value; }
        }

        public byte[] GetBytes(int cb)
        {
            return _underlyingDeriveBytes.GetBytes(cb);
        }

        public void Dispose()
        {
            IDisposable disposable = _underlyingDeriveBytes as IDisposable;
            if (disposable != null)
            {   // Only true on .NET
                disposable.Dispose();
            }
        }

        public void Reset()
        {
            _underlyingDeriveBytes.Reset();
        }
    }
}
