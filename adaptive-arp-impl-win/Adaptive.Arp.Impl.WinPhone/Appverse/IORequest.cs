using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
	public class IORequest : IOHeaders
	{
        public String Method {get;set;}

        public HTTPProtocolVersion ProtocolVersion { get; set; }
	}
}
