using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public class AppContextWebviewImpl : IAppContextWebview
    {
        private object webViewPrimary;
        private List<object> webViewList;

        public AppContextWebviewImpl()
        {
            this.webViewList = new List<object>();
        }

        public AppContextWebviewImpl(object webViewPrimary) : this()
        {
            this.webViewPrimary = webViewPrimary;
            if (this.webViewPrimary != null)
            {
                this.webViewList.Add(this.webViewPrimary);
            }
        }

        public void setPrimaryWebView(object webViewPrimary)
        {
            if (webViewPrimary != null)
            {
                if (!this.webViewList.Contains(webViewPrimary))
                {
                    this.webViewList.Add(webViewPrimary);
                }
                this.webViewPrimary = webViewPrimary;
            }
        }

        public void AddWebview(object webView)
        {
            if (webView != null)
            {
                if (!this.webViewList.Contains(webView))
                {
                    this.webViewList.Add(webView);
                }
            }
        }

        public object GetWebviewPrimary()
        {
            return this.webViewPrimary;
        }

        public object[] GetWebviews()
        {
            return this.webViewList.ToArray();
        }

        public void RemoveWebview(object webView)
        {
            if (webView != null && !webView.Equals(this.webViewPrimary))
            {
                this.webViewList.Remove(webView);
            }
        }
    }
}
