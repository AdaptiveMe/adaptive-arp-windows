using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone
{
    

    public class AppRegistryImpl : AbstractAppRegistryImpl
    {
        private static IAppRegistry _instance;

        public static IAppRegistry Instance
        {
            get
            {
                if (_instance == null) _instance = new AppRegistryImpl();
                return _instance;
            }
        }

        private AppRegistryImpl() : base()
        {
            this.ApplicationLifecycle = LifecycleImpl.Instance;
        }
    }
}
