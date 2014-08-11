// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace System.Reflection
{
    internal class ParameterCustomAttributeProvider : ICustomAttributeProvider
    {
        private readonly ParameterInfo _parameter;

        public ParameterCustomAttributeProvider(ParameterInfo parameter)
        {
            Debug.Assert(parameter != null);

            _parameter = parameter;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return _parameter.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _parameter.GetCustomAttributes(attributeType, inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _parameter.IsDefined(attributeType, inherit);
        }
    }
}
