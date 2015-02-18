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

    * @version v2.1.8

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a local or remote service response.

        @author Aryslan
        @since v2.0
        @version 1.0
     */
     public class ServiceResponse : APIBean
     {

          /**
             Encoding of the binary payload - by default assumed to be UTF8.
          */
          public IServiceContentEncoding ContentEncoding { get; set; }
          /**
             Response data content. The content should be in some well-known web format - in specific, binaries returned
should be encoded in base64.
          */
          public string Content { get; set; }
          /**
             The length in bytes for the Content field.
          */
          public int ContentLength { get; set; }
          /**
             The request/response content type (MIME TYPE).
          */
          public string ContentType { get; set; }
          /**
             The serviceHeaders array (name,value pairs) to be included on the I/O service request.
          */
          public ServiceHeader[] ServiceHeaders { get; set; }
          /**
             Information about the session.
          */
          public ServiceSession ServiceSession { get; set; }
          /**
             HTTP Status code of the response. With this status code it is possible to perform some actions, redirects, authentication, etc.
          */
          public int StatusCode { get; set; }

          /**
             Default constructor.

             @since V2.0
          */
          public ServiceResponse()  {
          }

          /**
             Constructor with fields

             @param Content         Request/Response data content (plain text).
             @param ContentType     The request/response content type (MIME TYPE).
             @param ContentEncoding Encoding of the binary payload - by default assumed to be UTF8.
             @param ContentLength   The length in bytes for the Content field.
             @param ServiceHeaders  The serviceHeaders array (name,value pairs) to be included on the I/O service request.
             @param ServiceSession  Information about the session
             @param StatusCode      HTTP Status code of the response.
             @since V2.0
          */
          public ServiceResponse(string content, string contentType, IServiceContentEncoding contentEncoding, int contentLength, ServiceHeader[] serviceHeaders, ServiceSession serviceSession, int statusCode) : base () {
               this.Content = Content;
               this.ContentType = ContentType;
               this.ContentEncoding = ContentEncoding;
               this.ContentLength = ContentLength;
               this.ServiceHeaders = ServiceHeaders;
               this.ServiceSession = ServiceSession;
               this.StatusCode = StatusCode;
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
             Returns the content

             @return Content
             @since V2.0
          */
          public string GetContent() {
               return this.Content;
          }

          /**
             Set the content

             @param Content Request/Response data content (plain text).
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
             Set the content length.

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
             Returns the status code of the response.

             @return HTTP status code
             @since V2.1.4
          */
          public int GetStatusCode() {
               return this.StatusCode;
          }

          /**
             Sets the status code of the response

             @param StatusCode HTTP status code
             @since V2.1.4
          */
          public void SetStatusCode(int StatusCode) {
               this.StatusCode = StatusCode;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
