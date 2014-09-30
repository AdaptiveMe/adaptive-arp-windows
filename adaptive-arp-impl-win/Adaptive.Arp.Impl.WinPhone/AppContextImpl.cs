using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class AppContextImpl : AbstractAppContextImpl
    {
        private static IAppContext _instance;

        public static IAppContext Instance
        {
            get
            {
                if (_instance == null) _instance = new AppContextImpl();
                return _instance;
            }
        }

        private AppContextImpl()
            : base()
        {

        }
    }
}
