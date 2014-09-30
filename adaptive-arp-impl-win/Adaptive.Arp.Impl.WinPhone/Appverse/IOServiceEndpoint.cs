using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
	[XmlRootAttribute("end-point", Namespace = "")]
	public class IOServiceEndpoint
	{
		[XmlAttributeAttribute(AttributeName = "scheme", DataType = "string")]
		public string Scheme { get; set; }

		[XmlAttributeAttribute(AttributeName = "host", DataType = "string")]
		public string Host { get; set; }

		[XmlAttributeAttribute(AttributeName = "port", DataType = "int")]
		public int Port { get; set; }

		[XmlAttributeAttribute(AttributeName = "path", DataType = "string")]
		public string Path { get; set; }
		
		[XmlAttributeAttribute(AttributeName = "proxy-url", DataType = "string")]
		public string ProxyUrl { get; set; }
	}
}
