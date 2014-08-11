// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace System.Adaptation
{
    // An implementation IAdapterResolver that probes for platforms-specific adapters by dynamically
    // looking for concrete types in platform-specific assemblies, such as Portable.Silverlight.
    internal class ProbingAdapterResolver : IAdapterResolver
    {
        private readonly string[] _platformNames;
        private readonly Func<string, Assembly> _assemblyLoader;
        private readonly object _lock = new object();
        private Dictionary<Type, object> _adapters = new Dictionary<Type, object>();
        private Assembly _assembly;

        public ProbingAdapterResolver(params string[] platformNames)
            : this(Assembly.Load, platformNames)
        {
        }

        public ProbingAdapterResolver(Func<string, Assembly> assemblyLoader, params string[] platformNames)
        {
            Debug.Assert(platformNames != null);
            Debug.Assert(assemblyLoader != null);

            _platformNames = platformNames;
            _assemblyLoader = assemblyLoader;
        }

        public object Resolve(Type type)
        {
            Debug.Assert(type != null);

            lock (_lock)
            {
                object instance;
                if (!_adapters.TryGetValue(type, out instance))
                {
                    Assembly assembly = GetPlatformSpecificAssembly();
                    instance = ResolveAdapter(assembly, type);
                    _adapters.Add(type, instance);
                }

                return instance;
            }
        }

        private static object ResolveAdapter(Assembly assembly, Type interfaceType)
        {
            string typeName = MakeAdapterTypeName(interfaceType);

            Type type = assembly.GetType(typeName, throwOnError: false);
            if (type != null)
                return Activator.CreateInstance(type);

            return type;
        }

        private static string MakeAdapterTypeName(Type interfaceType)
        {
            Debug.Assert(interfaceType.IsInterface);
            Debug.Assert(interfaceType.DeclaringType == null);
            Debug.Assert(interfaceType.Name.StartsWith("I", StringComparison.Ordinal));

            // For example, if we're looking for an implementation of System.Security.Cryptography.ICryptographyFactory, 
            // then we'll look for System.Security.Cryptography.CryptographyFactory
            return interfaceType.Namespace + "." + interfaceType.Name.Substring(1);
        }

        private Assembly GetPlatformSpecificAssembly()
        {   // We should be under a lock

            if (_assembly == null)
            {
                _assembly = ProbeForPlatformSpecificAssembly();
                if (_assembly == null)
                    throw new InvalidOperationException(Strings.AssemblyNotSupported);
            }

            return _assembly;
        }

        private Assembly ProbeForPlatformSpecificAssembly()
        {
            foreach (string platformName in _platformNames)
            {
                Assembly assembly = ProbeForPlatformSpecificAssembly(platformName);
                if (assembly != null)
                    return assembly;
            }

            return null;
        }

        private Assembly ProbeForPlatformSpecificAssembly(string platformName)
        {
            AssemblyName assemblyName = new AssemblyName(GetType().Assembly.FullName);
            assemblyName.Name = "Portable." + platformName;    // for example, Portable.Phone

            try
            {
                return _assemblyLoader(assemblyName.FullName);
            }
            catch (FileNotFoundException)
            {
            }

            return null;
        }
    }
}
