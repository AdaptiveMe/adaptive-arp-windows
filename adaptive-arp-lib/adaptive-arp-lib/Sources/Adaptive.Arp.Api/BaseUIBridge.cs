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
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Base application for UI purposes
        Auto-generated implementation of IBaseUI specification.
     */
     public class BaseUIBridge : IBaseUI
     {

          /**
             Group of API.
          */
          protected IAdaptiveRPGroup apiGroup;

          /**
             Default constructor.
          */
          public BaseUIBridge()
          {
               this.apiGroup = IAdaptiveRPGroup.UI;
          }

          /**
             Return the API group for the given interface.
          */
          public IAdaptiveRPGroup GetAPIGroup()
          {
               return this.apiGroup;
          }

          /**
             Return the API version for the given interface.
          */
          public String GetAPIVersion()
          {
               return "v2.1.8";
          }

          /**
             Return the JSON deserializer/serializer.
             @return JSON deserializer/serializer instance from factory.
          */
          public JSONProcessor GetJSONProcessor()
          {
               return AppRegistryBridge.GetJSONProcessor();
          }

          /**
             Invokes the given method specified in the API request object.

             @param request APIRequest object containing method name and parameters.
             @return APIResponse with status code, message and JSON response or a JSON null string for void functions. Status code 200 is OK, all others are HTTP standard error conditions.
          */
          public APIResponse Invoke(APIRequest request)
          {
               APIResponse response = new APIResponse();
               int responseCode = 200;
               String responseMessage = "OK";
               String responseJSON = "null";
               switch (request.GetMethodName())
               {
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "BaseUIBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.8.";
                         break;
               }
               response.SetResponse(responseJSON);
               response.SetStatusCode(responseCode);
               response.SetStatusMessage(responseMessage);
               return response;
          }
     }
}
/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
