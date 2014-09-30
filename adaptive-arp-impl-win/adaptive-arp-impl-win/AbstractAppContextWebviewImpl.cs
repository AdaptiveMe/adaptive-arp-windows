using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl
{
    public abstract class AbstractAppContextWebviewImpl : IAppContextWebview
    {

        private List<object> webviewList = null;
        protected AbstractAppContextWebviewImpl()
            : base()
        {
            webviewList = new List<object>();
        }

        public void AddWebview(object webView)
        {
            if (!webviewList.Contains(webView))
            {
                webviewList.Add(webView);
            }
        }

        public abstract object GetWebviewPrimary();

        public object[] GetWebviews()
        {
            object[] webViewArray = webviewList.ToArray();
            Array.Resize<object>(ref webViewArray, webViewArray.Length + 1);
            webViewArray[webViewArray.Length - 1] = GetWebviewPrimary();
            return webViewArray;
        }

        public void RemoveWebview(object webView)
        {
            if (webviewList.Contains(webView))
            {
                webviewList.Remove(webView);
            }
        }

        public abstract void SetWebviewPrimary(object webView);
    }
}
