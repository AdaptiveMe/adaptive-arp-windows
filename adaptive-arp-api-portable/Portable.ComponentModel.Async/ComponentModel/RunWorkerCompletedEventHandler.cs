// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.ComponentModel
{
    /// <summary>
    ///     Represents the method that will handle the <see cref="BackgroundWorker.RunWorkerCompleted"/> event of a <see cref="BackgroundWorker"/> class.
    /// </summary>
    /// <param name="sender">
    ///     The source of the event.
    /// </param>
    /// <param name="e">
    ///     A <see cref="RunWorkerCompletedEventArgs"/> that contains the event data.
    /// </param>
    public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
}
