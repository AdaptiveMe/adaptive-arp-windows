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
using IHashAlgorithm = pcl::System.Security.Cryptography.Adaptation.IHashAlgorithm;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT SymmetricKeyAlgorithmProvider to ICryptoTransform
    internal abstract class HashCryptoTransformAdapter : CryptoTransformAdapter, IHashAlgorithm
    {
        protected HashCryptoTransformAdapter()
        {
        }
        
        public override int InputBlockSize
        {
            get { return 1; }
        }

        public override int OutputBlockSize
        {
            get { return 1; }
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            return ComputeHash(buffer, 0, buffer.Length);
        }

        public byte[] ComputeHash(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || count > buffer.Length)
                throw new ArgumentOutOfRangeException("offset");

            if ((buffer.Length - count) < offset)
                throw new ArgumentException();

            IBuffer hashed = Transform(buffer.AsBuffer(offset, count));

            return hashed.ToArray();
        }

        public virtual byte[] ComputeHash(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            MemoryStream stream = new MemoryStream();
            inputStream.CopyTo(stream);

            return ComputeHash(stream.ToArray());
        }
    }
}
