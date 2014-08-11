namespace Sharpen
{
	using System;
    using System.IO.Compression;

	internal class InflaterInputStream : InputStream
	{
		protected InputStream @in;

		public InflaterInputStream (InputStream s)
		{
			this.@in = s;
            base.Wrapped = new DeflateStream(s.GetWrappedStream(), CompressionMode.Decompress);
		}
	}
}
