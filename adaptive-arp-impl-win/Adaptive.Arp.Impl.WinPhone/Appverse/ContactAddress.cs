using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class ContactAddress
    {
        /// <summary>
        /// Parameterless constructor is needed when parsing jsonstring to object.
        /// </summary>
        public ContactAddress()
        {
        }

        public DispositionType Type { get; set; }

        public string Address { get; set; }

        public string AddressNumber { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}
