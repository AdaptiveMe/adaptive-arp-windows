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

    * @version v2.1.6

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Network status listener events
        Auto-generated implementation of INetworkStatusListener specification.
     */
     public class NetworkStatusListenerImpl : BaseListenerImpl, INetworkStatusListener
     {

          /**
             Constructor with listener id.

             @param id  The id of the listener.
          */
          public NetworkStatusListenerImpl(long id) : base(id)
          {
          }

          /**
             No data received - error condition, not authorized or hardware not available.

             @param error Type of error encountered during reading.
             @since v2.0
          */
          public void OnError(INetworkStatusListenerError error)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkStatusListenerError( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(error) +") )");
          }

          /**
             Called when network connection changes somehow.

             @param network Change to this network.
             @since v2.0
          */
          public void OnResult(ICapabilitiesNet network)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkStatusListenerResult( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(network) +") )");
          }

          /**
             Status received with warning

             @param network Change to this network.
             @param warning Type of warning encountered during reading.
             @since v2.0
          */
          public void OnWarning(ICapabilitiesNet network, INetworkStatusListenerWarning warning)
          {
               AppRegistryBridge.GetInstance().GetPlatformContextWeb().ExecuteJavaScript("Adaptive.handleNetworkStatusListenerWarning( '"+GetId()+"', JSON.parse(" + GetJSONProcessor().SerializeObject(network) +"), JSON.parse(" + GetJSONProcessor().SerializeObject(warning) +") )");
          }

     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
