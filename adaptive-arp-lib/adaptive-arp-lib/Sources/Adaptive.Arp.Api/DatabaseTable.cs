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

    * @version v2.1.2

-------------------------------------------| aut inveniam viam aut faciam |--------------------------------------------
*/

using System;

namespace Adaptive.Arp.Api
{
     /**
        Represents a data table composed of databaseColumns and databaseRows.

        @author Ferran Vila Conesa
        @since v2.0
        @version 1.0
     */
     public class DatabaseTable : APIBean
     {

          /**
             Number of databaseColumns.
          */
          public int ColumnCount { get; set; }
          /**
             Definition of databaseColumns.
          */
          public DatabaseColumn[] DatabaseColumns { get; set; }
          /**
             Rows of the table containing the data.
          */
          public DatabaseRow[] DatabaseRows { get; set; }
          /**
             Name of the table.
          */
          public string Name { get; set; }
          /**
             Number of databaseRows.
          */
          public int RowCount { get; set; }

          /**
             Default constructor

             @since V2.0
          */
          public DatabaseTable()  {
          }

          /**
             Constructor by default

             @param Name The name of the table
             @since V2.0
          */
          public DatabaseTable(string name) : base () {
               this.Name = Name;
          }

          /**
             Constructor using fields

             @param Name            The name of the table
             @param ColumnCount     The number of databaseColumns
             @param RowCount        The number of databaseRows
             @param DatabaseColumns The databaseColumns of the table
             @param DatabaseRows    The databaseRows of the table
             @since V2.0
          */
          public DatabaseTable(string name, int columnCount, int rowCount, DatabaseColumn[] databaseColumns, DatabaseRow[] databaseRows) : base () {
               this.Name = Name;
               this.ColumnCount = ColumnCount;
               this.RowCount = RowCount;
               this.DatabaseColumns = DatabaseColumns;
               this.DatabaseRows = DatabaseRows;
          }

          /**
             Get the number of databaseColumns

             @return The number of databaseColumns
             @since V2.0
          */
          public int GetColumnCount() {
               return this.ColumnCount;
          }

          /**
             Sets the number of databaseColumns

             @param ColumnCount The number of databaseColumns
             @since V2.0
          */
          public void SetColumnCount(int ColumnCount) {
               this.ColumnCount = ColumnCount;
          }

          /**
             Get the databaseColumns

             @return The databaseColumns
             @since V2.0
          */
          public DatabaseColumn[] GetDatabaseColumns() {
               return this.DatabaseColumns;
          }

          /**
             Sets the databaseColumns of the table

             @param DatabaseColumns The databaseColumns of the table
             @since V2.0
          */
          public void SetDatabaseColumns(DatabaseColumn[] DatabaseColumns) {
               this.DatabaseColumns = DatabaseColumns;
          }

          /**
             Get the databaseRows of the table

             @return The databaseRows of the table
             @since V2.0
          */
          public DatabaseRow[] GetDatabaseRows() {
               return this.DatabaseRows;
          }

          /**
             Sets the databaseRows of the table

             @param DatabaseRows The databaseRows of the table
             @since V2.0
          */
          public void SetDatabaseRows(DatabaseRow[] DatabaseRows) {
               this.DatabaseRows = DatabaseRows;
          }

          /**
             Returns the name of the table

             @return The name of the table
             @since V2.0
          */
          public string GetName() {
               return this.Name;
          }

          /**
             Sets the name of the table

             @param Name The name of the table
             @since V2.0
          */
          public void SetName(string Name) {
               this.Name = Name;
          }

          /**
             Get the number of databaseRows

             @return The number of databaseRows
             @since V2.0
          */
          public int GetRowCount() {
               return this.RowCount;
          }

          /**
             Sets the number of databaseRows

             @param RowCount The number of databaseRows
             @since V2.0
          */
          public void SetRowCount(int RowCount) {
               this.RowCount = RowCount;
          }


     }
}

/**
------------------------------------| Engineered with ♥ in Barcelona, Catalonia |--------------------------------------
*/
