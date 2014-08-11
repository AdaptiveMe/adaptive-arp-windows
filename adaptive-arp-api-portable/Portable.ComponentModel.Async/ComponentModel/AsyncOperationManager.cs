// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Threading;

namespace System.ComponentModel
{
    /// <summary>
    ///     Provides concurrency management for classes that support asynchronous method calls.
    /// </summary>
    public static class AsyncOperationManager
    {
        /// <summary>
        ///     Returns an <see cref="AsyncOperation"/> for tracking the duration of a particular asynchronous operation.
        /// </summary>
        /// <param name="userSuppliedState">
        ///     An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. 
        /// </param>
        /// <returns>
        ///     An <see cref="AsyncOperation"/> that you can use to track the duration of an asynchronous method invocation.
        /// </returns>
        public static AsyncOperation CreateOperation(object userSuppliedState)
        {
            return CreateOperation(null, userSuppliedState);
        }

        /// <summary>
        ///     Returns an <see cref="AsyncOperation"/> for tracking the duration of a particular asynchronous operation.
        /// </summary>
        /// <param name="userSuppliedState">
        ///     An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. 
        /// </param>
        /// <param name="context">
        ///     A <see cref="SynchronizationContext"/> for the asynchronous operation.
        /// </param>
        /// <returns>
        ///     An <see cref="AsyncOperation"/> that you can use to track the duration of an asynchronous method invocation.
        /// </returns>
        public static AsyncOperation CreateOperation(SynchronizationContext context, object userSuppliedState)
        {
            if (context == null)
                context = SynchronizationContext.Current ?? new SynchronizationContext();

            return new AsyncOperation(context, userSuppliedState);
        }
    }
}
