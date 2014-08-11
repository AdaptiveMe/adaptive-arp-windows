// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data.Common
{
    public abstract class DbCommand : IDbCommand
    {
        protected DbCommand()
        {
        }

        public abstract string CommandText
        {
            get;
            set;
        }

        public abstract int CommandTimeout
        {
            get;
            set;
        }

        public abstract CommandType CommandType
        {
            get;
            set;
        }

        public DbConnection Connection
        {
            get { return DbConnection; }
            set { DbConnection = value; }
        }

        public DbParameterCollection Parameters
        {
            get { return DbParameterCollection; }
        }

        public DbTransaction Transaction
        {
            get { return DbTransaction; }
            set { DbTransaction = value; }
        }

        public abstract UpdateRowSource UpdatedRowSource
        {
            get;
            set;
        }

        protected abstract DbConnection DbConnection
        {
            get;
            set;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public abstract void Cancel();

        protected abstract DbParameter CreateDbParameter();

        public DbParameter CreateParameter()
        {
            return CreateDbParameter();
        }

        protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

        public abstract int ExecuteNonQuery();

        public DbDataReader ExecuteReader()
        {
            return ExecuteDbDataReader(CommandBehavior.Default);
        }

        public DbDataReader ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteDbDataReader(behavior);
        }

        public abstract object ExecuteScalar();

        public abstract void Prepare();

        IDbDataParameter IDbCommand.CreateParameter()
        {
            return CreateDbParameter();
        }

        IDataReader IDbCommand.ExecuteReader()
        {
            return ExecuteDbDataReader(CommandBehavior.Default);
        }

        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteDbDataReader(behavior);
        }

        protected abstract DbParameterCollection DbParameterCollection 
        { 
            get; 
        }

        protected abstract DbTransaction DbTransaction 
        { 
            get; 
            set; 
        }

        IDbConnection IDbCommand.Connection 
        { 
            get { return DbConnection; } 
            set { DbConnection = (DbConnection)value; }
        }

        IDataParameterCollection IDbCommand.Parameters 
        { 
            get { return DbParameterCollection; }
        }

        IDbTransaction IDbCommand.Transaction 
        { 
            get { return DbTransaction; }
            set { DbTransaction = (DbTransaction)value; }
        }
    }
}
