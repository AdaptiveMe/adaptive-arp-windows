// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Reflection
{
    /// <summary>
    ///     Provides custom attributes for reflection objects that support them.
    /// </summary>
    public interface ICustomAttributeProvider
    {
        /// <summary>
        ///     Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there are no custom attributes.
        /// </summary>
        /// <param name="inherit">
        ///     When true, look up the hierarchy chain for the inherited custom attribute. 
        /// </param>
        /// <returns>
        ///     An array of Objects representing custom attributes, or an empty array.
        /// </returns>
        /// <exception cref="TypeLoadException">
        ///     The custom attribute type cannot be loaded. 
        /// </exception>
        object[] GetCustomAttributes(bool inherit);

        /// <summary>
        ///     Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there are no custom attributes.
        /// </summary>
        /// <param name="inherit">
        ///     When true, look up the hierarchy chain for the inherited custom attribute. 
        /// </param>
        /// <param name="attributeType">
        ///     The type of the custom attributes. 
        /// </param>
        /// <returns>
        ///     An array of Objects representing custom attributes, or an empty array.
        /// </returns>
        /// <exception cref="TypeLoadException">
        ///     The custom attribute type cannot be loaded. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="attributeType"/> is <see langword="null"/>.
        /// </exception>
        object[] GetCustomAttributes(Type attributeType, bool inherit);

        /// <summary>
        ///     Indicates whether one or more instance of attributeType is defined on this member.
        /// </summary>
        /// <param name="attributeType">
        ///     The type of the custom attributes. 
        /// </param>
        /// <param name="inherit">
        ///     When true, look up the hierarchy chain for the inherited custom attribute. 
        /// </param>
        /// <returns>
        ///     true if the attributeType is defined on this member; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="attributeType"/> is <see langword="null"/>.
        /// </exception>
        bool IsDefined(Type attributeType, bool inherit);
    }
}
