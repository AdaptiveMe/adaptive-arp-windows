// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Adaptation
{
    internal interface IAdapterResolver
    {
        object Resolve(Type type);
    }
}
