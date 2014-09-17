using Adaptive.Arp.Api;
/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * 
 *
 * =====================================================================================================================
 */
using System;

namespace Adaptive.Arp.Impl
{
    public class AppRegistryImpl : IAppRegistry
    {
        private IAnalytics analytics {  set; get; }

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
