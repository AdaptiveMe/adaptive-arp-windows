using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    [XmlRoot(ElementName = "service")]
	public class IOService
	{
		[XmlAttributeAttribute(AttributeName = "name", DataType = "string")]
		public string Name { get; set; }

		[XmlAttributeAttribute(AttributeName = "type")]
		public ServiceType Type { get; set; }

		[XmlElement("end-point")]
		public IOServiceEndpoint Endpoint { get; set; }

		[XmlAttributeAttribute(AttributeName = "req-method")]
		public RequestMethod RequestMethod { get; set; }
	}
}
