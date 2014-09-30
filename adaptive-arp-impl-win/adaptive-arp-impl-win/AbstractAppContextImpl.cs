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
    public abstract class AbstractAppContextImpl : IAppContext
    {
        private Object applicationContext;
        private IAppContext.Type applicationContextType;

        protected AbstractAppContextImpl() : base()
        {

        }

        protected AbstractAppContextImpl(object context, IAppContext.Type type) : this()
        {
            this.applicationContext = context;
            this.applicationContextType = type;
        }

        public  void SetContext(object context, IAppContext.Type type)
        {
            if (context != null)
            {
                this.applicationContext = context;
                if (type != null)
                {
                    this.applicationContextType = type;
                }
                else
                {
                    this.applicationContextType = IAppContext.Type.Unknown;
                }
            }
        }

        public sealed override object GetContext()
        {
            return this.applicationContext;
        }

        public sealed override IAppContext.Type GetContextType()
        {
            return this.applicationContextType;
        }
    }
}
