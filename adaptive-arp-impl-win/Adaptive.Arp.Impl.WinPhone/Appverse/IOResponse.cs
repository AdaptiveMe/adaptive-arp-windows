using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
	public class IOResponse : IOHeaders
	{
		public byte[] ContentBinary { get; set; }

		public int GetContentLengthBinary {
			get {
				if (ContentBinary != null) {
					return ContentBinary.Length;
				} else {
					return 0;
				}
			}
		}
	}
}
