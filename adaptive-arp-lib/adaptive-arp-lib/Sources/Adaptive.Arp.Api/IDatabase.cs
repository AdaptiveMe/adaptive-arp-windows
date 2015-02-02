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

namespace Adaptive.Arp.Api
{

     /**
        Interface for Managing the Cloud operations

        @author Ferran Vila Conesa
        @since v2.0
        @version 1.0
     */
     public interface IDatabase : IBaseData
     {

          /**
             Creates a database on default path for every platform.

             @param callback Asynchronous callback
             @param database Database object to create
             @since v2.0
          */
          void CreateDatabase(Database database, IDatabaseResultCallback callback);

          /**
             Creates a databaseTable inside a database for every platform.

             @param database      Database for databaseTable creating.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @param callback      DatabaseTable callback with the response
             @since v2.0
          */
          void CreateTable(Database database, DatabaseTable databaseTable, IDatabaseTableResultCallback callback);

          /**
             Deletes a database on default path for every platform.

             @param database Database object to delete
             @param callback Asynchronous callback
             @since v2.0
          */
          void DeleteDatabase(Database database, IDatabaseResultCallback callback);

          /**
             Deletes a databaseTable inside a database for every platform.

             @param database      Database for databaseTable removal.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @param callback      DatabaseTable callback with the response
             @since v2.0
          */
          void DeleteTable(Database database, DatabaseTable databaseTable, IDatabaseTableResultCallback callback);

          /**
             Executes SQL statement into the given database. The replacements
should be passed as a parameter

             @param database     The database object reference.
             @param statement    SQL statement.
             @param replacements List of SQL statement replacements.
             @param callback     DatabaseTable callback with the response.
             @since v2.0
          */
          void ExecuteSqlStatement(Database database, string statement, string[] replacements, IDatabaseTableResultCallback callback);

          /**
             Executes SQL transaction (some statements chain) inside given database.

             @param database     The database object reference.
             @param statements   The statements to be executed during transaction.
             @param rollbackFlag Indicates if rollback should be performed when any
                    statement execution fails.
             @param callback     DatabaseTable callback with the response.
             @since v2.0
          */
          void ExecuteSqlTransactions(Database database, string[] statements, bool rollbackFlag, IDatabaseTableResultCallback callback);

          /**
             Checks if database exists by given database name.

             @param database Database Object to check if exists
             @return True if exists, false otherwise
             @since v2.0
          */
          bool ExistsDatabase(Database database);

          /**
             Checks if databaseTable exists by given database name.

             @param database      Database for databaseTable consulting.
             @param databaseTable DatabaseTable object with the name of the databaseTable inside.
             @return True if exists, false otherwise
             @since v2.0
          */
          bool ExistsTable(Database database, DatabaseTable databaseTable);

     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
