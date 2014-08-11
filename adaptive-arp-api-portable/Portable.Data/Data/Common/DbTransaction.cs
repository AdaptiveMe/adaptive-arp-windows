// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections;

namespace System.Data.Common
{
    public abstract class DbTransaction : IDbTransaction
    {
        protected DbTransaction()
        {
        }

        public DbConnection Connection 
        {
            get { return DbConnection; }
        }
        
        protected abstract DbConnection DbConnection 
        { 
            get; 
        }

        public abstract IsolationLevel IsolationLevel 
        { 
            get; 
        }

        IDbConnection IDbTransaction.Connection
        {
            get { return DbConnection; }
        }

        public abstract void Commit();
        public abstract void Rollback();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
