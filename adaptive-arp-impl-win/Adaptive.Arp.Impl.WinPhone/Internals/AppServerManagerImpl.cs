using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Internals
{
    public class AppServerManagerImpl : AbstractAppServerManagerImpl
    {
        private static IAppServerManager instance;

        public static IAppServerManager Instance
        {
            get
            {
                if (instance == null) instance = new AppServerManagerImpl();
                return instance;
            }
        }

        private AppServerManagerImpl()
            : base()
        {
            
        }

        public override IAppServer CreateServerInstance()
        {
            throw new NotImplementedException();
        }
    }
}
