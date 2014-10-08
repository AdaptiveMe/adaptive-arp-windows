using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class OSInfo
    {
        private string name;
        private string vendor;
        private string version;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Vendor
        {
            get
            {
                return vendor;
            }
            set
            {
                vendor = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public override string ToString()
        {
            return Name + " " + Version + " (" + Vendor + ")";
        }
    }

}
