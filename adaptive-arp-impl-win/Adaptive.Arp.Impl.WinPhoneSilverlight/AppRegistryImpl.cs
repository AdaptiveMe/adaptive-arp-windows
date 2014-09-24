using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhoneSilverlight 
{
    public class AppRegistryImpl : AbstractAppRegistryImpl 
    {
        private static IAppRegistry instance;

        public static IAppRegistry Instance
        {
            get
            {
                if (instance == null) instance = new AppRegistryImpl();
                return instance;
            }
        }

        private AppRegistryImpl()
            : base()
        {

        }
    }
}
