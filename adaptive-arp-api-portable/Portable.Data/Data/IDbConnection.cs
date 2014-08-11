// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data
{
    public interface IDbConnection : IDisposable
    {
        string ConnectionString { get; set; }
        
        int ConnectionTimeout { get; }

        string Database { get; }

        ConnectionState State { get; }

        IDbTransaction BeginTransaction();
        IDbTransaction BeginTransaction(IsolationLevel il);

        void ChangeDatabase(string databaseName);
        void Close();

        IDbCommand CreateCommand();
        void Open();
    }
}
