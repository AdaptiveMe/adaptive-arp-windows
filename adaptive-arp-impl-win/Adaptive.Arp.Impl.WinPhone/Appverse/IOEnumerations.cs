using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
	[XmlTypeAttribute()]
	public enum ServiceType
	{
		SOAP_XML,
		SOAP_JSON,
		XMLRPC_XML,
		REST_XML,
		XMLRPC_JSON,
		REST_JSON,
		AMF_SERIALIZATION,
		REMOTING_SERIALIZATION,
		OCTET_BINARY,
		GWT_RPC
	}

	[XmlTypeAttribute()]
	public enum RequestMethod
	{
		POST,
		GET
	}

    [XmlTypeAttribute()]
    public enum HTTPProtocolVersion
    {
        HTTP10,
        HTTP11
    }
}
