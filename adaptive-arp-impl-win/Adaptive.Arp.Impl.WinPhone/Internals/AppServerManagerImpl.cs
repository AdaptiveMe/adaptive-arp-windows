using Adaptive.Arp.Api;
using Adaptive.Arp.Impl.WinPhone.Appverse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            AppServerImpl server = null;
#if DEBUG
            if (Debugger.IsAttached)
            {
                server = new AppServerImpl("8080");
            }
            else
            {
#endif
                server = new AppServerImpl("127.0.0.1", "8080");
#if DEBUG
            }
#endif
            Regex appverseCompatibility = new Regex("^/service/*");
            server.addRule(appverseCompatibility, new AppverseBridge().HandleAppverse);
            return server;
        }
    }
}
