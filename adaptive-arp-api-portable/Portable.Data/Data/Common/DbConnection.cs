// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections;

namespace System.Data.Common
{
    public abstract class DbConnection : IDbConnection
    {
        protected DbConnection()
        {
        }

        public event StateChangeEventHandler StateChange;

        public abstract string ConnectionString 
        { 
            get; 
            set; 
        }

        public virtual int ConnectionTimeout
        {
            get { return 15; }
        }

        public abstract string Database 
        { 
            get; 
        }

        public abstract string DataSource 
        { 
            get; 
        }

        protected virtual DbProviderFactory DbProviderFactory 
        {
            get { return null; }
        }

        public abstract string ServerVersion 
        { 
            get; 
        }

        public abstract ConnectionState State 
        { 
            get; 
        }
        
        public DbTransaction BeginTransaction()
        {
            return BeginDbTransaction(IsolationLevel.Unspecified);
        }

        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginDbTransaction(isolationLevel);
        }

        public abstract void ChangeDatabase(string databaseName);

        public abstract void Close();

        public DbCommand CreateCommand()
        {
            return CreateDbCommand();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Open();

        protected abstract DbCommand CreateDbCommand();
        protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

        protected virtual void Dispose(bool disposing)
        {
        }

        protected virtual void OnStateChange(StateChangeEventArgs e)
        {
            var handler = StateChange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        IDbTransaction IDbConnection.BeginTransaction()
        {
            return BeginDbTransaction(IsolationLevel.Unspecified);
        }

        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginDbTransaction(isolationLevel);
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            return CreateCommand();
        }
    }
}
