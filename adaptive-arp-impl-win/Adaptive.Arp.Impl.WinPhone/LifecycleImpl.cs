using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class LifecycleImpl : AbstractLifecycleImpl
    {
        private static ILifecycle _instance;

        public static ILifecycle Instance
        {
            get
            {
                if (_instance == null) _instance = new LifecycleImpl();
                return _instance;
            }
        }
        private LifecycleImpl()
            : base()
        {
        }

    }
}
