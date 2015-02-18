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

    * @version v2.1.7

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Network reachability callback result
        Auto-generated implementation of INetworkReachabilityCallback specification.
     */
     public class NetworkReachabilityCallbackImpl : BaseCallbackImpl, INetworkReachabilityCallback
     {

          /**
             Constructor with callback id.

          @param id  The id of the callback.
          */
          public NetworkReachabilityCallbackImpl(long id) : base(id)
          {
          }

          /**
             No data received - error condition, not authorized .

             @param error Error value
             @since v2.0
          */
          public void OnError(INetworkReachabilityCallbackError error)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkReachabilityCallbackError( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(error) +") )");
          }

          /**
             Correct data received.

             @param reachable Indicates if the host is reachable
             @since v2.0
          */
          public void OnResult(bool reachable)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkReachabilityCallbackResult( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(reachable) +") )");
          }

          /**
             Data received with warning - ie Found entries with existing key and values have been overriden

             @param reachable Indicates if the host is reachable
             @param warning   Warning value
             @since v2.0
          */
          public void OnWarning(bool reachable, INetworkReachabilityCallbackWarning warning)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkReachabilityCallbackWarning( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(reachable) +"), JSON.parse(" + GetJSONProcessor().SerializeObject(warning) +") )");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
