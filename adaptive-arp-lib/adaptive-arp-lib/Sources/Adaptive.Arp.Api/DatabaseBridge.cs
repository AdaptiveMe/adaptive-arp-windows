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

    * @version v2.1.3

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;
using Adaptive.Arp.Util;

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Cloud operations
        Auto-generated implementation of IDatabase specification.
     */
public class DatabaseBridge : BaseDataBridge, IDatabase, APIBridge
{

          /**
             API Delegate.
          */
          private IDatabase _delegate;

          /**
             Constructor with delegate.

             @param _delegate The delegate implementing platform specific functions.
          */
          public DatabaseBridge(IDatabase _delegate) : base()
          {
               this._delegate = _delegate;
          }
          /**
             Get the delegate implementation.
             @return IDatabase delegate that manages platform specific functions..
          */
          public IDatabase GetDelegate()
          {
               return this._delegate;
          }
          /**
             Set the delegate implementation.

             @param _delegate The delegate implementing platform specific functions.
          */
          public void SetDelegate(IDatabase _delegate)
          {
               this._delegate = _delegate;
          }

          /**
             Creates a database on default path for every platform.

             @param callback Asynchronous callback
             @param database Database object to create
             @since v2.0
          */
          public void CreateDatabase(Database database, IDatabaseResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing createDatabase({"+database+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.CreateDatabase(database, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'createDatabase' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'createDatabase'.");
                    }
                    }
                    
               }

          /**
             Creates a databaseTable inside a database for every platform.

             @param database      Database for databaseTable creating.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @param callback      DatabaseTable callback with the response
             @since v2.0
          */
          public void CreateTable(Database database, DatabaseTable databaseTable, IDatabaseTableResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing createTable({"+database+"},{"+databaseTable+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.CreateTable(database, databaseTable, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'createTable' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'createTable'.");
                    }
                    }
                    
               }

          /**
             Deletes a database on default path for every platform.

             @param database Database object to delete
             @param callback Asynchronous callback
             @since v2.0
          */
          public void DeleteDatabase(Database database, IDatabaseResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing deleteDatabase({"+database+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.DeleteDatabase(database, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'deleteDatabase' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'deleteDatabase'.");
                    }
                    }
                    
               }

          /**
             Deletes a databaseTable inside a database for every platform.

             @param database      Database for databaseTable removal.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @param callback      DatabaseTable callback with the response
             @since v2.0
          */
          public void DeleteTable(Database database, DatabaseTable databaseTable, IDatabaseTableResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing deleteTable({"+database+"},{"+databaseTable+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.DeleteTable(database, databaseTable, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'deleteTable' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'deleteTable'.");
                    }
                    }
                    
               }

          /**
             Executes SQL statement into the given database. The replacements
should be passed as a parameter

             @param database     The database object reference.
             @param statement    SQL statement.
             @param replacements List of SQL statement replacements.
             @param callback     DatabaseTable callback with the response.
             @since v2.0
          */
          public void ExecuteSqlStatement(Database database, string statement, string[] replacements, IDatabaseTableResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing executeSqlStatement({"+database+"},{"+statement+"},{"+replacements+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.ExecuteSqlStatement(database, statement, replacements, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'executeSqlStatement' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'executeSqlStatement'.");
                    }
                    }
                    
               }

          /**
             Executes SQL transaction (some statements chain) inside given database.

             @param database     The database object reference.
             @param statements   The statements to be executed during transaction.
             @param rollbackFlag Indicates if rollback should be performed when any
                    statement execution fails.
             @param callback     DatabaseTable callback with the response.
             @since v2.0
          */
          public void ExecuteSqlTransactions(Database database, string[] statements, bool rollbackFlag, IDatabaseTableResultCallback callback)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing executeSqlTransactions({"+database+"},{"+statements+"},{"+rollbackFlag+"},{"+callback+"}).");
               }

               if (this._delegate != null)
               {
                    this._delegate.ExecuteSqlTransactions(database, statements, rollbackFlag, callback);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'executeSqlTransactions' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'executeSqlTransactions'.");
                    }
                    }
                    
               }

          /**
             Checks if database exists by given database name.

             @param database Database Object to check if exists
             @return True if exists, false otherwise
             @since v2.0
          */
          public bool ExistsDatabase(Database database)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing existsDatabase({"+database+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.ExistsDatabase(database);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'existsDatabase' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'existsDatabase'.");
                    }
                    }
                    return result;                    
               }

          /**
             Checks if databaseTable exists by given database name.

             @param database      Database for databaseTable consulting.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @return True if exists, false otherwise
             @since v2.0
          */
          public bool ExistsTable(Database database, DatabaseTable databaseTable)
          {
               // Start logging elapsed time.
               long tIn = TimerUtil.CurrentTimeMillis();
               ILogging logger = AppRegistryBridge.GetInstance().GetLoggingBridge();

               if (logger!=null)
               {
                    logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executing existsTable({"+database+"},{"+databaseTable+"}).");
               }

               bool result = false;
               if (this._delegate != null)
               {
                    result = this._delegate.ExistsTable(database, databaseTable);
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.DEBUG, this.apiGroup.ToString(),"DatabaseBridge executed 'existsTable' in "+(TimerUtil.CurrentTimeMillis()-tIn)+"ms.");
                    }
               } else {
                    if (logger!=null)
                    {
                         logger.Log(ILoggingLogLevel.ERROR, this.apiGroup.ToString(),"DatabaseBridge no delegate for 'existsTable'.");
                    }
                    }
                    return result;                    
               }

          /**
             Invokes the given method specified in the API request object.

             @param request APIRequest object containing method name and parameters.
             @return APIResponse with status code, message and JSON response or a JSON null string for void functions. Status code 200 is OK, all others are HTTP standard error conditions.
          */
          public new APIResponse Invoke(APIRequest request)
          {
               APIResponse response = new APIResponse();
               int responseCode = 200;
               String responseMessage = "OK";
               String responseJSON = "null";
               switch (request.GetMethodName())
               {
                    case "createDatabase":
                         Database database0 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         IDatabaseResultCallback callback0 = new DatabaseResultCallbackImpl(request.GetAsyncId());
                         this.CreateDatabase(database0, callback0);
                         break;
                    case "createTable":
                         Database database1 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         DatabaseTable databaseTable1 = GetJSONProcessor().DeserializeObject<DatabaseTable>(request.GetParameters()[1]);
                         IDatabaseTableResultCallback callback1 = new DatabaseTableResultCallbackImpl(request.GetAsyncId());
                         this.CreateTable(database1, databaseTable1, callback1);
                         break;
                    case "deleteDatabase":
                         Database database2 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         IDatabaseResultCallback callback2 = new DatabaseResultCallbackImpl(request.GetAsyncId());
                         this.DeleteDatabase(database2, callback2);
                         break;
                    case "deleteTable":
                         Database database3 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         DatabaseTable databaseTable3 = GetJSONProcessor().DeserializeObject<DatabaseTable>(request.GetParameters()[1]);
                         IDatabaseTableResultCallback callback3 = new DatabaseTableResultCallbackImpl(request.GetAsyncId());
                         this.DeleteTable(database3, databaseTable3, callback3);
                         break;
                    case "executeSqlStatement":
                         Database database4 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         string statement4 = GetJSONProcessor().DeserializeObject<string>(request.GetParameters()[1]);
                         string[] replacements4 = GetJSONProcessor().DeserializeObject<string[]>(request.GetParameters()[2]);
                         IDatabaseTableResultCallback callback4 = new DatabaseTableResultCallbackImpl(request.GetAsyncId());
                         this.ExecuteSqlStatement(database4, statement4, replacements4, callback4);
                         break;
                    case "executeSqlTransactions":
                         Database database5 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         string[] statements5 = GetJSONProcessor().DeserializeObject<string[]>(request.GetParameters()[1]);
                         bool rollbackFlag5 = GetJSONProcessor().DeserializeObject<bool>(request.GetParameters()[2]);
                         IDatabaseTableResultCallback callback5 = new DatabaseTableResultCallbackImpl(request.GetAsyncId());
                         this.ExecuteSqlTransactions(database5, statements5, rollbackFlag5, callback5);
                         break;
                    case "existsDatabase":
                         Database database6 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         bool response6 = this.ExistsDatabase(database6);
                         responseJSON = GetJSONProcessor().SerializeObject(response6);
                         break;
                    case "existsTable":
                         Database database7 = GetJSONProcessor().DeserializeObject<Database>(request.GetParameters()[0]);
                         DatabaseTable databaseTable7 = GetJSONProcessor().DeserializeObject<DatabaseTable>(request.GetParameters()[1]);
                         bool response7 = this.ExistsTable(database7, databaseTable7);
                         responseJSON = GetJSONProcessor().SerializeObject(response7);
                         break;
                    default:
                         // 404 - response null.
                         responseCode = 404;
                         responseMessage = "DatabaseBridge does not provide the function '"+request.GetMethodName()+"' Please check your client-side API version; should be API version >= v2.1.3.";
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
