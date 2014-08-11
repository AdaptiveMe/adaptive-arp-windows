// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    /// <summary>
    ///     Provides extensions for reflection objects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ReflectionExtensions
    {
        /// <summary>
        ///     Returns an <see cref="ICustomAttributeProvider"/> for the specified member.
        /// </summary>
        /// <param name="member">
        ///     The <see cref="MemberInfo"/> to return an <see cref="ICustomAttributeProvider"/> for.
        /// </param>
        /// <returns>
        ///     A <see cref="ICustomAttributeProvider"/> for <paramref name="member"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        public static ICustomAttributeProvider AsCustomAttributeProvider(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            return new MemberCustomAttributeProvider(member);
        }

        /// <summary>
        ///     Returns an <see cref="ICustomAttributeProvider"/> for the specified assembly.
        /// </summary>
        /// <param name="assembly">
        ///     The <see cref="Assembly"/> to return an <see cref="ICustomAttributeProvider"/> for.
        /// </param>
        /// <returns>
        ///     A <see cref="ICustomAttributeProvider"/> for <paramref name="assembly"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="assembly"/> is <see langword="null"/>.
        /// </exception>
        public static ICustomAttributeProvider AsCustomAttributeProvider(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return new AssemblyCustomAttributeProvider(assembly);
        }

        /// <summary>
        ///     Returns an <see cref="ICustomAttributeProvider"/> for the specified parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The <see cref="ParameterInfo"/> to return an <see cref="ICustomAttributeProvider"/> for.
        /// </param>
        /// <returns>
        ///     A <see cref="ICustomAttributeProvider"/> for <paramref name="parameter"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parameter"/> is <see langword="null"/>.
        /// </exception>
        public static ICustomAttributeProvider AsCustomAttributeProvider(this ParameterInfo parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return new ParameterCustomAttributeProvider(parameter);
        }
    }
}
