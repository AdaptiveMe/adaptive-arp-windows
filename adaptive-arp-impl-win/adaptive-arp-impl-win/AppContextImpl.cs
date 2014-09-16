using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public class AppContextImpl : IAppContext
    {
        private Object applicationContext;
        private IAppContext.Type applicationContextType;

        public AppContextImpl()
        {

        }

        public AppContextImpl(object context, IAppContext.Type type)
        {
            this.applicationContext = context;
            this.applicationContextType = type;
        }

        public void setContext(object context, IAppContext.Type type)
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
                    // TODO: Type unknown.
                    //this.applicationContextType = IAppContext.Type.Unknown;
                }
            }
        }

        public override object GetContext()
        {
            return this.applicationContext;
        }

        public override IAppContext.Type GetContextType()
        {
            return this.applicationContextType;
        }
    }
}
