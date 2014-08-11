// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Threading;

namespace System.ComponentModel
{
    /// <summary>
    ///     Tracks the lifetime of an asynchronous operation.
    /// </summary>
    public sealed class AsyncOperation
    {
        private readonly SynchronizationContext _synchronizationContext;
        private readonly object _userSuppliedState;
        private bool _completed;

        internal AsyncOperation(SynchronizationContext synchronizationContext, object userSuppliedState)
        {
            _synchronizationContext = synchronizationContext;
            _userSuppliedState = userSuppliedState;
            _synchronizationContext.OperationStarted();
        }

        /// <summary>
        ///     Gets the current SynchronizationContext
        /// </summary>
        /// <value>
        ///     The current <see cref="SynchronizationContext"/>.
        /// </value>
        public SynchronizationContext SynchronizationContext
        {
            get { return _synchronizationContext; }
        }
        
        /// <summary>
        ///     Gets an object used to uniquely identify an asynchronous operation.
        /// </summary>
        /// <value>
        ///     The state object passed to the asynchronous method invocation.
        /// </value>
        public object UserSuppliedState
        {
            get { return _userSuppliedState; }
        }
        
        /// <summary>
        ///     Ends the lifetime of an asynchronous operation.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="OperationCompleted"/> has been called previously for this task. 
        /// </exception>
        public void OperationCompleted()
        {
            EnsureNotCompleted();

            try
            {
                _synchronizationContext.OperationCompleted();
            }
            finally
            {
                _completed = true;
            }
        }

        /// <summary>
        ///     Invokes a delegate on the thread or context appropriate for the application model.
        /// </summary>
        /// <param name="d">
        ///     A SendOrPostCallback object that wraps the delegate to be called when the operation ends. 
        /// </param>
        /// <param name="arg">
        ///     An argument for the delegate contained in the d parameter
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="OperationCompleted"/> has been called previously for this task. 
        /// </exception>
        public void Post(SendOrPostCallback d, object arg)
        {
            EnsureNotCompleted();

            _synchronizationContext.Post(d, arg);
        }

        /// <summary>
        ///     Ends the lifetime of an asynchronous operation.
        /// </summary>
        /// <param name="d">
        ///     A SendOrPostCallback object that wraps the delegate to be called when the operation ends. 
        /// </param>
        /// <param name="arg">
        ///     An argument for the delegate contained in the d parameter
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="OperationCompleted"/> has been called previously for this task. 
        /// </exception>
        public void PostOperationCompleted(SendOrPostCallback d, object arg)
        {
            Post(d, arg);
            OperationCompleted();
        }

       
        private void EnsureNotCompleted()
        {
            if (_completed)
                throw new InvalidOperationException();
        }
    }
}
