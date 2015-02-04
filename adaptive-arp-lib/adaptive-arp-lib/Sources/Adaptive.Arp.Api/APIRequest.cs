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

    * @version v2.1.4

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Structure representing a HTML5 request to the native API.

        @author Carlos Lozano Diez
        @since v2.0
        @version 1.0
     */
     public class APIRequest
     {

          /**
             Identifier of API version of this request.
          */
          public string ApiVersion { get; set; }
          /**
             Identifier of callback or listener for async operations.
          */
          public long AsyncId { get; set; }
          /**
             String representing the bridge type to obtain.
          */
          public string BridgeType { get; set; }
          /**
             String representing the method name to call.
          */
          public string MethodName { get; set; }
          /**
             Parameters of the request as JSON formatted strings.
          */
          public string[] Parameters { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public APIRequest()  {
          }

          /**
             Constructor with method name. No parameters

             @param BridgeType Name of the bridge to be invoked.
             @param MethodName Name of the method
             @since V2.0
          */
          public APIRequest(string bridgeType, string methodName) : base () {
               this.BridgeType = BridgeType;
               this.MethodName = MethodName;
          }

          /**
             Constructor with all the parameters

             @param BridgeType Name of the bridge to be invoked.
             @param MethodName Name of the method
             @param Parameters Array of parameters as JSON formatted strings.
             @param AsyncId    Id of callback or listener or zero if none for synchronous calls.
             @since V2.0
          */
          public APIRequest(string bridgeType, string methodName, string[] parameters, long asyncId) : base () {
               this.BridgeType = BridgeType;
               this.MethodName = MethodName;
               this.Parameters = Parameters;
               this.AsyncId = AsyncId;
          }

          /**
             Returns the request's API version. This should be the same or higher than the platform managing the
request.

             @return String with the API version of the request.
          */
          public string GetApiVersion() {
               return this.ApiVersion;
          }

          /**
             Sets the request's API version. This should be the same or higher than the platform managing the
request.

             @param ApiVersion String with the API version of the request.
          */
          public void SetApiVersion(string ApiVersion) {
               this.ApiVersion = ApiVersion;
          }

          /**
             Returns the callback or listener id assigned to this request OR zero if there is no associated callback or
listener.

             @return Long with the unique id of the callback or listener, or zero if there is no associated async event.
          */
          public long GetAsyncId() {
               return this.AsyncId;
          }

          /**
             Sets the callback or listener id to the request.

             @param AsyncId The unique id of the callback or listener.
          */
          public void SetAsyncId(long AsyncId) {
               this.AsyncId = AsyncId;
          }

          /**
             Bridge Type Getter

             @return Bridge Type
             @since V2.0
          */
          public string GetBridgeType() {
               return this.BridgeType;
          }

          /**
             Bridge Type Setter

             @param BridgeType Bridge Type
             @since V2.0
          */
          public void SetBridgeType(string BridgeType) {
               this.BridgeType = BridgeType;
          }

          /**
             Method name Getter

             @return Method name
             @since V2.0
          */
          public string GetMethodName() {
               return this.MethodName;
          }

          /**
             Method name Setter

             @param MethodName Method name
             @since V2.0
          */
          public void SetMethodName(string MethodName) {
               this.MethodName = MethodName;
          }

          /**
             Parameters Getter

             @return Parameters
             @since V2.0
          */
          public string[] GetParameters() {
               return this.Parameters;
          }

          /**
             Parameters Setter

             @param Parameters Parameters, JSON formatted strings of objects.
             @since V2.0
          */
          public void SetParameters(string[] Parameters) {
               this.Parameters = Parameters;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
