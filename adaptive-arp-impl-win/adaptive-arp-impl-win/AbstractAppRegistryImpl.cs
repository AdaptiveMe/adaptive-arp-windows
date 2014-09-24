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
    public abstract class AbstractAppRegistryImpl : IAppRegistry
    {
        protected AbstractAppRegistryImpl()
        {

        }

        public IAnalytics GetApplicationAnalytics()
        {
            return this.ApplicationAnalytics;
        }

        public IGlobalization GetApplicationGlobalization()
        {
            return this.ApplicationGlobalization;
        }

        public ILifecycle GetApplicationLifecycle()
        {
            return this.ApplicationLifecycle;
        }

        public IManagement GetApplicationManagement()
        {
            return this.ApplicationManagement;
        }

        public IPrinting GetApplicationPrinting()
        {
            return this.ApplicationPrinting;
        }

        public ISettings GetApplicationSettings()
        {
            return this.ApplicationSettings;
        }

        public IUpdate GetApplicationUpdate()
        {
            return this.ApplicationUpdate;
        }

        public IAppContext GetPlatformContext()
        {
            return this.PlatformContext;
        }

        public IAppContextWebview GetPlatformContextWeb()
        {
            return this.PlatformContextWeb;
        }

        public IAppResourceHandler GetPlatformResourceHandler()
        {
            return this.PlatformResourceHandler;
        }

        public ICapabilities GetSystemCapabilities()
        {
            return this.SystemCapabilities;
        }

        public IDevice GetSystemDevice()
        {
            return this.SystemDevice;
        }

        public IDisplay GetSystemDisplay()
        {
            return this.SystemDisplay;
        }

        public IOS GetSystemOS()
        {
            return this.SystemOS;
        }

        public IRuntime GetSystemRuntime()
        {
            return this.SystemRuntime;
        }

        protected IAnalytics ApplicationAnalytics { get; set; }

        protected IGlobalization ApplicationGlobalization { get; set; }

        protected ILifecycle ApplicationLifecycle { get; set; }

        protected IManagement ApplicationManagement { get; set; }

        protected IPrinting ApplicationPrinting { get; set; }

        protected ISettings ApplicationSettings { get; set; }

        protected IUpdate ApplicationUpdate { get; set; }

        protected IAppContext PlatformContext { get; set; }

        protected IAppContextWebview PlatformContextWeb { get; set; }

        protected IAppResourceHandler PlatformResourceHandler { get; set; }

        protected ICapabilities SystemCapabilities { get; set; }

        protected IDevice SystemDevice { get; set; }

        protected IDisplay SystemDisplay { get; set; }

        protected IOS SystemOS { get; set; }

        protected IRuntime SystemRuntime { get; set; }
    }
}
