/**
--| ADAPTIVE RUNTIME PLATFORM |----------------------------------------------------------------------------------------

(C) Copyright 2013-2015 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the
License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 . Unless required by appli-
-cable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,  WITHOUT
WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the  License  for the specific language governing
permissions and limitations under the License.

Original author:

    * Carlos Lozano Diez
            <http://github.com/carloslozano>
            <http://twitter.com/adaptivecoder>
            <mailto:carlos@adaptive.me>

Contributors:

    * Ferran Vila Conesa
             <http://github.com/fnva>
             <http://twitter.com/ferran_vila>
             <mailto:ferran.vila.conesa@gmail.com>

    * See source code files for contributors.

Release:

    * @version v2.2.0

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Api.Impl
{

     /**
        Interface for webview context management purposes
        Auto-generated implementation of IAppContextWebview specification.
     */
     public class AppContextWebviewDelegate : IAppContextWebview
     {

          /**
             Default Constructor.
          */
          public AppContextWebviewDelegate() : base()
          {
          }

          /**
             Additional views may be added to an application - a separate activity - and if these will make calls to the
ARP methods, they must be registered by adding them to the context. When they are added to the context, ARP
methods are bound to the webview so that they're callable from the HTML application. The primary webview should
not be added using this method.

             @param webView Platform specific webview reference (WebView, UIWebView, WKWebView,etc.)
             @since v2.0
          */
          public void AddWebview(Object webView) {
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:addWebview");
          }

          /**
             Evaluate the specified javascript on the main webview of the application.

             @param javaScriptText The javascript expression to execute on the webview.
          */
          public void ExecuteJavaScript(string javaScriptText) {
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:executeJavaScript");
          }

          /**
             Evaluate the specified javascript on the specified webview of the application.

             @param javaScriptText   The javascript expression to execute on the webview.
             @param webViewReference The target webview on which to execute the expression.
          */
          public void ExecuteJavaScript(string javaScriptText, Object webViewReference) {
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:executeJavaScript");
          }

          /**
             Returns a reference to the main application webview. This is the first application webview and can not be removed
with the removeWebview method. The object returned should be cast to the platform specific implementation
WebView, WKWebView, etc.

             @return Object representing the specific and primary webview instance of the application.
             @since v2.0
          */
          public Object GetWebviewPrimary() {
               // Object response;
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:getWebviewPrimary");
               // return response;
          }

          /**
             Returns an array of webviews currently managed by the context - composed of primary and the list of those added.
This method will always return at least one element; the primary webview.

             @return Array with all the Webview instances being managed by ARP.
             @since v2.0
          */
          public Object[] GetWebviews() {
               // Object[] response;
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:getWebviews");
               // return response;
          }

          /**
             When a webview is disposed - no longer in use from an external activity - the webview should be removed to unbind
ARP functions and release resources. The primary webview can not be removed.

             @param webView The instance of the webview to be removed from the binding.
             @since v2.0
          */
          public void RemoveWebview(Object webView) {
               // TODO: Not implemented.
               throw new NotSupportedException("AppContextWebviewDelegate:removeWebview");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
