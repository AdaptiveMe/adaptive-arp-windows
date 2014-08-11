// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Data.Common
{
    public abstract class DbParameter : IDbDataParameter
    {
        protected DbParameter()
        {
        }

        public abstract void ResetDbType();
        public abstract DbType DbType { get; set; }
        public abstract ParameterDirection Direction { get; set; }
        public abstract bool IsNullable { get; set; }
        public abstract string ParameterName { get; set; }
        public abstract int Size { get; set; }
        public abstract string SourceColumn { get; set; }
        public abstract bool SourceColumnNullMapping { get; set; }
        public abstract DataRowVersion SourceVersion { get; set; }
        public abstract object Value { get; set; }

        byte IDbDataParameter.Precision
        {
            get { return 0; }
            set { }
        }

        byte IDbDataParameter.Scale
        {
            get { return 0; }
            set { }
        }
    }
}
