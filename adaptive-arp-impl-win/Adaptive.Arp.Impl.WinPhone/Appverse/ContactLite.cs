using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class ContactLite
    {
        public ContactLite()
        {
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string DisplayName { get; set; }

        public string Group { get; set; }

        public ContactPhone[] Phones { get; set; }

        public ContactEmail[] Emails { get; set; }

        public override string ToString()
        {
            return ID + " " +
            Name + " " +
            Firstname + " " +
            Lastname + " " +
            DisplayName + " " +
            Phones.ToString();

        }
    }
}
