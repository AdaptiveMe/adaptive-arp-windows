// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.ComponentModel
{
    /// <summary>
    ///     Provides data for the <see cref="BackgroundWorker.DoWork"/> event.
    /// </summary>
    public class DoWorkEventArgs : CancelEventArgs
    {
        private readonly object _argument;
        private object _result;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DoWorkEventArgs"/> class.
        /// </summary>
        /// <param name="argument">
        ///     An <see cref="object"/> representing the argument of an asynchronous operation.
        /// </param>
        public DoWorkEventArgs(object argument)
        {
            _argument = argument;
        }

        /// <summary>
        ///     Gets a value that represents the argument of an asynchronous operation.
        /// </summary>
        /// <value>
        ///     An <see cref="object"/> representing the argument of an asynchronous operation.
        /// </value>
        public object Argument
        {
            get { return _argument; }
        }

        /// <summary>
        ///     Gets or sets a value that represents the result of an asynchronous operation.
        /// </summary>
        /// <value>
        ///     An <see cref="object"/> representing the result of an asynchronous operation.
        /// </value>
        public object Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
