namespace Sharpen
{
	using System;
	using System.IO;
    using PCLStorage;

	internal class FileLock
	{
		private Stream s;

		public FileLock (Stream s)
		{
			this.s = s;
		}

		public void Release ()
		{
			// TODO: This is not portable!
            //this.s.Unlock (0, int.MaxValue);
		}
	}
}
