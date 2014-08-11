// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace System
{
    /// <summary>
    ///     Provides extension methods for closing disposable objects.
    /// </summary>
    public static class CloseExtensions
    {
        /// <summary>
        ///     Releases all resources used by the <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="Stream"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            stream.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="TextReader"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="TextWriter"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="BinaryReader"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="BinaryWriter"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this BinaryWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="XmlReader"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="WebResponse"/>.
        /// </summary>
        /// <param name="response">
        ///     The <see cref="WebResponse"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="response"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this WebResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            response.Dispose();
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="WaitHandle"/>.
        /// </summary>
        /// <param name="handle">
        ///     The <see cref="WaitHandle"/> to dispose.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="handle"/> is <see langword="null"/>.
        /// </exception>
        public static void Close(this WaitHandle handle)
        {
            if (handle == null)
                throw new ArgumentNullException("handle");

            handle.Dispose();
        }
    }
}
