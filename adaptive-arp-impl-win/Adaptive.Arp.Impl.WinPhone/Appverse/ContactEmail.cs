using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class ContactEmail
    {
        public ContactEmail()
        {
        }

        public DispositionType Type { get; set; }

        public bool IsPrimary { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string CommonName { get; set; }

        public string Address { get; set; }

    }
}
