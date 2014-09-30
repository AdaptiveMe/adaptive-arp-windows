using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class AppContextWebviewImpl : AbstractAppContextWebviewImpl
    {
        private static IAppContextWebview _instance;
        private WebView _primaryWebview;

        public static IAppContextWebview Instance
        {
            get
            {
                if (_instance == null) _instance = new AppContextWebviewImpl();
                return _instance;
            }
        }

        private AppContextWebviewImpl()
            : base()
        {

        }
        
        public override object GetWebviewPrimary()
        {
            return _primaryWebview;
        }

        public void SetWebviewPrimary(WebView webview)
        {
            _primaryWebview = webview;
        }


        public override void SetWebviewPrimary(object webView)
        {
            SetWebviewPrimary(webView as WebView);
        }
    }
}
