using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class ContactPhone
    {
        public ContactPhone()
        {
        }

        public NumberType Type { get; set; }

        public string Number { get; set; }

        public bool IsPrimary { get; set; }
    }
}
