using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public class AppRegistryImpl : IAppRegistry
    {
        private IAnalytics analytics { public set; private get; }

        public IAnalytics GetApplicationAnalytics()
        {
            return analytics;
        }

        public IGlobalization GetApplicationGlobalization()
        {
            throw new NotImplementedException();
        }

        public ILifecycle GetApplicationLifecycle()
        {
            throw new NotImplementedException();
        }

        public IManagement GetApplicationManagement()
        {
            throw new NotImplementedException();
        }

        public IPrinting GetApplicationPrinting()
        {
            throw new NotImplementedException();
        }

        public ISettings GetApplicationSettings()
        {
            throw new NotImplementedException();
        }

        public IUpdate GetApplicationUpdate()
        {
            throw new NotImplementedException();
        }

        public IAppContext GetPlatformContext()
        {
            throw new NotImplementedException();
        }

        public IAppContextWebview GetPlatformContextWeb()
        {
            throw new NotImplementedException();
        }

        public IAppResourceHandler GetPlatformResourceHandler()
        {
            throw new NotImplementedException();
        }

        public ICapabilities GetSystemCapabilities()
        {
            throw new NotImplementedException();
        }

        public IDevice GetSystemDevice()
        {
            throw new NotImplementedException();
        }

        public IDisplay GetSystemDisplay()
        {
            throw new NotImplementedException();
        }

        public IOS GetSystemOS()
        {
            throw new NotImplementedException();
        }

        public IRuntime GetSystemRuntime()
        {
            throw new NotImplementedException();
        }
    }
}
