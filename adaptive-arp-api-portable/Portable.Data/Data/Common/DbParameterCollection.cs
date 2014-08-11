// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections;

namespace System.Data.Common
{
    public abstract class DbParameterCollection : IDataParameterCollection, IList, ICollection, IEnumerable
    {
        protected DbParameterCollection()
        {
        }

        public abstract int Count 
        { 
            get; 
        }

        public virtual bool IsFixedSize 
        {
            get { return false; }
        }

        public virtual bool IsReadOnly 
        {
            get { return false; }
        }

        public virtual bool IsSynchronized 
        {
            get { return false; }
        }

        public DbParameter this[int index]
        {
            get { return this.GetParameter(index); }
            set { this.SetParameter(index, value); }
        }

        public DbParameter this[string parameterName] 
        {
            get { return this.GetParameter(parameterName); }
            set { this.SetParameter(parameterName, value); }
        }

        public abstract object SyncRoot { get; }
        public abstract int Add(object value);
        public abstract void AddRange(Array values);
        public abstract void Clear();
        public abstract bool Contains(object value);
        public abstract bool Contains(string value);
        public abstract void CopyTo(Array array, int index);
        public abstract IEnumerator GetEnumerator();
        public abstract int IndexOf(object value);
        public abstract int IndexOf(string parameterName);
        public abstract void Insert(int index, object value);
        public abstract void Remove(object value);
        public abstract void RemoveAt(int index);
        public abstract void RemoveAt(string parameterName);
        protected abstract DbParameter GetParameter(int index);
        protected abstract DbParameter GetParameter(string parameterName);
        protected abstract void SetParameter(int index, DbParameter value);
        protected abstract void SetParameter(string parameterName, DbParameter value);

        object IList.this[int index]
        {
            get { return this.GetParameter(index); }
            set { this.SetParameter(index, (DbParameter)value); }
        }

        object IDataParameterCollection.this[string parameterName]
        {
            get { return this.GetParameter(parameterName); }
            set { this.SetParameter(parameterName, (DbParameter)value); }
        }
    }
}
