// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Globalization;

namespace System
{
    /// <summary>
    ///     Provides extension methods for uppercasing and lowercasing strings.
    /// </summary>
    public static class GlobalizationExtensions
    {
        /// <summary>
        ///     Returns a copy of this string converted to uppercase, using the casing rules of the specified culture.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to uppercase.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>The uppercase equivalent of the current string.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value"/> is <see langword="null"/>.
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <paramref name="culture"/> is <see langword="null"/>.
        /// </exception>
        public static string ToUpper(this string value, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (culture == null)
                throw new ArgumentNullException("culture");

            return culture.TextInfo.ToUpper(value);
        }

        /// <summary>
        ///     Returns a copy of this string converted to lowercase, using the casing rules of the specified culture.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to uppercase.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>The lowercase equivalent of the current string.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value"/> is <see langword="null"/>.
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <paramref name="culture"/> is <see langword="null"/>.
        /// </exception>
        public static string ToLower(this string value, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (culture == null)
                throw new ArgumentNullException("culture");

            return culture.TextInfo.ToLower(value);
        }
    }
}
