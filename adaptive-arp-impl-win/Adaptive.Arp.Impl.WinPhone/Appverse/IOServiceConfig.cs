using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    [XmlRootAttribute("io-services-config", Namespace = "", IsNullable = false)]
    public class IOServicesConfig
    {
        [XmlArray("services", IsNullable = false), XmlArrayItem("service", typeof(IOService))]
        public List<IOService>
            Services = new List<IOService>();

    }
}
