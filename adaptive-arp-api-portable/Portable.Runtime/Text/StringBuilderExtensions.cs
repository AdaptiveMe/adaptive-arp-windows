// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Text
{
    /// <summary>
    ///     Provides extension methods for <see cref="StringBuilder"/> instances.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        ///     Removes all characters from the current StringBuilder instance.
        /// </summary>
        /// <param name="builder">
        ///     The <see cref="StringBuilder"/> to clear.
        /// </param>
        /// <returns>
        ///     An object whose Length is 0 (zero).
        /// </returns>
        public static StringBuilder Clear(this StringBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Length = 0;

            return builder;
        }
    }
}
