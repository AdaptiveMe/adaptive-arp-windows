namespace Sharpen
{
	using System;
    using System.IO.Compression;

	internal class DeflaterOutputStream : OutputStream
	{
		public DeflaterOutputStream (OutputStream s)
		{
            base.Wrapped = new DeflateStream(s.GetWrappedStream(),CompressionMode.Compress);
		}

		public void Finish ()
		{
			((DeflateStream)base.Wrapped).Close ();
		}
	}
}
