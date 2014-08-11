// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace System.Reflection
{
    internal class AssemblyCustomAttributeProvider : ICustomAttributeProvider
    {
        private readonly Assembly _assembly;

        public AssemblyCustomAttributeProvider(Assembly assembly)
        {
            Debug.Assert(assembly != null);

            _assembly = assembly;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return _assembly.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _assembly.GetCustomAttributes(attributeType, inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _assembly.IsDefined(attributeType, inherit);
        }
    }
}
