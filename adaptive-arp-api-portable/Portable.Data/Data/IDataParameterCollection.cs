// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections;

namespace System.Data
{
    public interface IDataParameterCollection : IList, ICollection, IEnumerable
    {
        bool Contains(string parameterName);
        int IndexOf(string parameterName);
        void RemoveAt(string parameterName);

        object this[string parameterName] 
        { 
            get; 
            set; 
        }
    }
}
