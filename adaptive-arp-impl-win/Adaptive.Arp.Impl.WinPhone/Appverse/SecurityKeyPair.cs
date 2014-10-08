using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class SecurityKeyPair
    {
        private string key = String.Empty;
        private string value = String.Empty;

        /// <summary>
        /// Name of the Key
        /// </summary>
        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        /// <summary>
        /// Value of the Key
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public SecurityKeyPair()
        {
        }

        public SecurityKeyPair(string KeyName, string Value)
        {
            this.Key = KeyName;
            this.Value = Value;
        }
    }
}
