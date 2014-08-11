// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace System.Reflection
{
    internal class MemberCustomAttributeProvider : ICustomAttributeProvider
    {
        private readonly MemberInfo _member;

        public MemberCustomAttributeProvider(MemberInfo member)
        {
            Debug.Assert(member != null);

            _member = member;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return _member.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _member.GetCustomAttributes(attributeType, inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _member.IsDefined(attributeType, inherit);
        }
    }
}
