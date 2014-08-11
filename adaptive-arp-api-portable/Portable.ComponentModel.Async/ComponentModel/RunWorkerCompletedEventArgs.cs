﻿// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Reflection;

namespace System.ComponentModel
{
    /// <summary>
    ///     Provides data for the <see cref="BackgroundWorker.RunWorkerCompleted"/> event.
    /// </summary>
    public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object _result;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RunWorkerCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="result">
        ///     The result of an asynchronous operation.
        /// </param>
        /// <param name="error">
        ///     Any error that occurred during the asynchronous operation.
        /// </param>
        /// <param name="cancelled">
        ///     A value indicating whether the asynchronous operation was cancelled.
        /// </param>
        public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled)
            : base(error, cancelled, null)
        {
            _result = result;
        }

        /// <summary>
        ///     Gets a value that represents the result of an asynchronous operation.
        /// </summary>
        /// <exception cref="TargetInvocationException">
        ///     <see cref="AsyncCompletedEventArgs.Error"/> is not <see langword="null"/>. The <see cref="Exception.InnerException"/> property holds a reference to <see cref="AsyncCompletedEventArgs.Error"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="AsyncCompletedEventArgs.Cancelled"/> is <see langword="true"/>.
        /// </exception>
        public object Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return _result;
            }
        }

        /// <summary>
        ///     Gets a value that represents the user state.
        /// </summary>
        /// <value>
        ///     An <see cref="object"/> representing the user state.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new object UserState
        {
            get { return base.UserState; }
        }
    }
}
