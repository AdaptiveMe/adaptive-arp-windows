using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class Contact : ContactLite
    {
        /// <summary>
        /// Parameterless constructor is needed when parsing jsonstring to object.
        /// </summary>
        public Contact()
        {
        }

        public string Company { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string[] WebSites { get; set; }

        public string Notes { get; set; }

        public RelationshipType Relationship { get; set; }

        public ContactAddress[] Addresses { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoBase64Encoded { get; set; }



    }
}
