using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl
{
    public abstract class AbstractLifecycleImpl : ILifecycle
    {
        private List<ILifecycleListener> listenerList = null;
        private Lifecycle lastState = new Lifecycle(Lifecycle.State.Stopping);
        public bool IsVisible { get; set; }

        protected AbstractLifecycleImpl()
        {
            listenerList = new List<ILifecycleListener>();
        }

        public void AddLifecycleListener(ILifecycleListener listener)
        {
            if (!listenerList.Contains(listener))
            {
                listenerList.Add(listener);
            }
        }

        public void RemoveLifecycleListener(ILifecycleListener listener)
        {
            if (listenerList.Contains(listener))
            {
                listenerList.Remove(listener);
            }
        }

        public void RemoveLifecycleListeners()
        {
            listenerList.Clear();
        }

        public bool IsBackground()
        {
            return !IsVisible;
        }

        private async void NotifyStateChange(Lifecycle state)
        {
            if (state.GetState() != null && lastState.GetState() != null && state.GetState() != lastState.GetState())
            {
                if (Debugger.IsAttached)
                {

                    Debug.WriteLine("Lifecycle state change - {0} => {1}", lastState.GetState(), state.GetState());
                }

                this.lastState = state;
                Task.Run(async () =>
                {
                    foreach (ILifecycleListener listener in listenerList)
                    {
                        listener.OnResult(state);
                    }
                });
            }
        }

        public void Starting()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Starting));
        }

        public void Started()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Started));
        }

        public void Running()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Running));
        }

        public void Paused()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Paused));
        }

        public void PauseIdle()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.PausedIdle));
        }

        public void PauseRun()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.PausedRun));
        }

        public void Resuming()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Resuming));
        }

        public void Stopping()
        {
            NotifyStateChange(new Lifecycle(Lifecycle.State.Stopping));
        }

        public void Error(Exception ex)
        {
            Task.Run(async () =>
            {
                foreach (ILifecycleListener listener in listenerList)
                {
                    // TODO: Implement error.
                }
            });
        }
    }
}
