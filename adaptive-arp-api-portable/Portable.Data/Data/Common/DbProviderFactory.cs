// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data.Common
{
    public abstract class DbProviderFactory
    {
        protected DbProviderFactory()
        {
        }

        public abstract DbCommand CreateCommand();
        public abstract DbConnection CreateConnection();
        public abstract DbParameter CreateParameter();
    }
}
