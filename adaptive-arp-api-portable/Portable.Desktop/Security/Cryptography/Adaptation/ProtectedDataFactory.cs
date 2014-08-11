// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using pcl::System.Security.Cryptography.Adaptation;

namespace System.Security.Cryptography.Adaptation
{
    // NOTE: Do not merge this other factories - they need to be self contained so that 
    // absense of the assemblies that contain other factory interfaces do not prevent
    // this factory from loading.
    internal class ProtectedDataFactory : IProtectedDataFactory
    {
        public ProtectedDataFactory()
        {
        }

        public IProtectedData CreateProtectedData()
        {
            return new ProtectedDataAdapter();
        }
    }
}
