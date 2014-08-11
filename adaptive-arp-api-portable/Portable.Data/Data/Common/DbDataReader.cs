// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections;

namespace System.Data.Common
{
    public abstract class DbDataReader : IDataReader, IDisposable, IDataRecord, IEnumerable
    {
        protected DbDataReader()
        {
        }

        public abstract int Depth { get; }
        public abstract int FieldCount { get; }
        public abstract bool HasRows { get; }
        public abstract bool IsClosed { get; }
        public abstract object this[int ordinal] { get; }
        public abstract object this[string name] { get; }
        public abstract int RecordsAffected { get; }

        public virtual int VisibleFieldCount
        {
            get { return FieldCount; }
        }

        public abstract void Close();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Close();
            }
        }

        public abstract bool GetBoolean(int ordinal);
        public abstract byte GetByte(int ordinal);
        public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);
        public abstract char GetChar(int ordinal);
        public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

        public DbDataReader GetData(int ordinal)
        {
            return GetDbDataReader(ordinal);
        }

        public abstract string GetDataTypeName(int ordinal);
        public abstract DateTime GetDateTime(int ordinal);
        public abstract decimal GetDecimal(int ordinal);
        public abstract double GetDouble(int ordinal);
        public abstract IEnumerator GetEnumerator();
        public abstract Type GetFieldType(int ordinal);
        public abstract float GetFloat(int ordinal);
        public abstract Guid GetGuid(int ordinal);
        public abstract short GetInt16(int ordinal);
        public abstract int GetInt32(int ordinal);
        public abstract long GetInt64(int ordinal);
        public abstract string GetName(int ordinal);
        public abstract int GetOrdinal(string name);

        //public abstract DataTable GetSchemaTable();
        public abstract string GetString(int ordinal);
        public abstract object GetValue(int ordinal);
        public abstract int GetValues(object[] values);
        public abstract bool IsDBNull(int ordinal);
        public abstract bool NextResult();
        public abstract bool Read();
        protected abstract DbDataReader GetDbDataReader(int ordinal);

        public virtual Type GetProviderSpecificFieldType(int ordinal)
        {
            return this.GetFieldType(ordinal);
        }

        public virtual object GetProviderSpecificValue(int ordinal)
        {
            return this.GetValue(ordinal);
        }

        public virtual int GetProviderSpecificValues(object[] values)
        {
            return this.GetValues(values);
        }

        IDataReader IDataRecord.GetData(int ordinal)
        {
            return GetDbDataReader(ordinal);
        }

    }
}
