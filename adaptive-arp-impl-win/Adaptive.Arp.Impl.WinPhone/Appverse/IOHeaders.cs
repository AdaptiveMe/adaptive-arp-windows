using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
	public class IOHeaders
	{
		public string ContentType { get; set; }

		public IOHeader[] Headers { get; set; }

		public string Content { get; set; }
		
		/// <summary>
		/// Returns byte[] representing Content field.
		/// </summary>
		/// <returns></returns>
		public byte[] GetRawContent ()
		{
			if (Content == null) {
				return null;
			}
			return Encoding.UTF8.GetBytes (Content);
		}

		/// <summary>
		/// Length in bytes for 
		/// </summary>
		/// <returns></returns>
		public int GetContentLength ()
		{
			byte[] rawContent = GetRawContent ();
			if (rawContent == null) {
				return 0;
			}

			return rawContent.Length;
		}

		public IOSessionContext Session { get; set; }
	}
}
