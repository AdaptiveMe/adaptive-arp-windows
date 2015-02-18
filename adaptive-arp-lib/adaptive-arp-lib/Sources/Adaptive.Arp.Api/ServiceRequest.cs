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

    * @version v2.1.9

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a local or remote service request.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class ServiceRequest : APIBean
     {

          /**
             Encoding of the content - by default assumed to be UTF8. This may be populated by the application, the platform
populates this field with defaults for the service.
          */
          public IServiceContentEncoding ContentEncoding { get; set; }
          /**
             Body parameters to be included in the body of the request to a service. These may be applied
during GET/POST operations. No body parameters are included if this array is null or length zero.
          */
          public ServiceRequestParameter[] BodyParameters { get; set; }
          /**
             Request data content (plain text). This should be populated by the application. The content should be
in some well-known web format - in specific, binaries submitted should be encoded to base64 and the content
type should be set respectively by the application.
          */
          public string Content { get; set; }
          /**
             The length in bytes of the content. This may be populated by the application, the platform
calculates this length automatically if a specific contentLength is not specified.
          */
          public int ContentLength { get; set; }
          /**
             The request content type (MIME TYPE). This may be populated by the application, the platform
populates this field with defaults for the service.
          */
          public string ContentType { get; set; }
          /**
             Query string parameters to be appended to the service URL when making the request. These may be applied
during GET/POST operations. No query parameters are appended if this array is null or length zero.
          */
          public ServiceRequestParameter[] QueryParameters { get; set; }
          /**
             This host indicates the origin host of the request. This, could be useful in case of redirected requests.
          */
          public string RefererHost { get; set; }
          /**
             The serviceHeaders array (name,value pairs) to be included in the request. This may be populated by the
application, the platform populates this field with defaults for the service and the previous headers.
In specific, the platform maintains request and response state automatically.
          */
          public ServiceHeader[] ServiceHeaders { get; set; }
          /**
             Session attributes and cookies. This may be populated by the application, the platform populates
this field with defaults for the service and the previous state information. In specific, the platform
maintains request and response state automatically.
          */
          public ServiceSession ServiceSession { get; set; }
          /**
             Token used for the creation of the request with the destination service, endpoint, function and method
identifiers. This should not be manipulated by the application directly.
          */
          public ServiceToken ServiceToken { get; set; }
          /**
             This attribute allows for the default user-agent string to be overridden by the application.
          */
          public string UserAgent { get; set; }

          /**
             Default constructor.

             @since V2.0
          */
          public ServiceRequest()  {
          }

          /**
             Convenience constructor.

             @param Content      Content payload.
             @param ServiceToken ServiceToken for the request.
             @since V2.0.6
          */
          public ServiceRequest(string content, ServiceToken serviceToken) : base () {
               this.Content = Content;
               this.ServiceToken = ServiceToken;
          }

          /**
             Returns the content encoding

             @return ContentEncoding
             @since V2.0
          */
          public IServiceContentEncoding GetContentEncoding() {
               return this.ContentEncoding;
          }

          /**
             Set the content encoding

             @param ContentEncoding Encoding of the binary payload - by default assumed to be UTF8.
             @since V2.0
          */
          public void SetContentEncoding(IServiceContentEncoding ContentEncoding) {
               this.ContentEncoding = ContentEncoding;
          }

          /**
             Gets the body parameters of the request.

             @return ServiceRequestParameter array or null if none are specified.
             @since V2.0.6
          */
          public ServiceRequestParameter[] GetBodyParameters() {
               return this.BodyParameters;
          }

          /**
             Sets the body parameters of the request.

             @param BodyParameters ServiceRequestParameter array or null if none are specified.
             @since V2.0.6
          */
          public void SetBodyParameters(ServiceRequestParameter[] BodyParameters) {
               this.BodyParameters = BodyParameters;
          }

          /**
             Returns the content

             @return Content
             @since V2.0
          */
          public string GetContent() {
               return this.Content;
          }

          /**
             Set the content

             @param Content Request/Response data content (plain text)
             @since V2.0
          */
          public void SetContent(string Content) {
               this.Content = Content;
          }

          /**
             Returns the content length

             @return ContentLength
             @since V2.0
          */
          public int GetContentLength() {
               return this.ContentLength;
          }

          /**
             Set the content length

             @param ContentLength The length in bytes for the Content field.
             @since V2.0
          */
          public void SetContentLength(int ContentLength) {
               this.ContentLength = ContentLength;
          }

          /**
             Returns the content type

             @return ContentType
             @since V2.0
          */
          public string GetContentType() {
               return this.ContentType;
          }

          /**
             Set the content type

             @param ContentType The request/response content type (MIME TYPE).
             @since V2.0
          */
          public void SetContentType(string ContentType) {
               this.ContentType = ContentType;
          }

          /**
             Gets the query parameters of the request.

             @return ServiceRequestParameter array or null if none are specified.
             @since V2.0.6
          */
          public ServiceRequestParameter[] GetQueryParameters() {
               return this.QueryParameters;
          }

          /**
             Sets the query parameters of the request.

             @param QueryParameters ServiceRequestParameter array or null if none are specified.
             @since V2.0.6
          */
          public void SetQueryParameters(ServiceRequestParameter[] QueryParameters) {
               this.QueryParameters = QueryParameters;
          }

          /**
             Returns the referer host (origin) of the request.

             @return Referer host of the request
             @since V2.1.4
          */
          public string GetRefererHost() {
               return this.RefererHost;
          }

          /**
             Sets the value for the referer host of the request.

             @param RefererHost Referer host of the request
             @since V2.1.4
          */
          public void SetRefererHost(string RefererHost) {
               this.RefererHost = RefererHost;
          }

          /**
             Returns the array of ServiceHeader

             @return ServiceHeaders
             @since V2.0
          */
          public ServiceHeader[] GetServiceHeaders() {
               return this.ServiceHeaders;
          }

          /**
             Set the array of ServiceHeader

             @param ServiceHeaders The serviceHeaders array (name,value pairs) to be included on the I/O service request.
             @since V2.0
          */
          public void SetServiceHeaders(ServiceHeader[] ServiceHeaders) {
               this.ServiceHeaders = ServiceHeaders;
          }

          /**
             Getter for service session

             @return The element service session
             @since V2.0
          */
          public ServiceSession GetServiceSession() {
               return this.ServiceSession;
          }

          /**
             Setter for service session

             @param ServiceSession The element service session
             @since V2.0
          */
          public void SetServiceSession(ServiceSession ServiceSession) {
               this.ServiceSession = ServiceSession;
          }

          /**
             Gets the ServiceToken of the request.

             @return ServiceToken.
             @since V2.0.6
          */
          public ServiceToken GetServiceToken() {
               return this.ServiceToken;
          }

          /**
             Sets the ServiceToken of the request.

             @param ServiceToken ServiceToken to be used for the invocation.
             @since V2.0.6
          */
          public void SetServiceToken(ServiceToken ServiceToken) {
               this.ServiceToken = ServiceToken;
          }

          /**
             Gets the overridden user-agent string.

             @return User-agent string.
             @since V2.0.6
          */
          public string GetUserAgent() {
               return this.UserAgent;
          }

          /**
             Sets the user-agent to override the default user-agent string.

             @param UserAgent User-agent string.
             @since V2.0.6
          */
          public void SetUserAgent(string UserAgent) {
               this.UserAgent = UserAgent;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
