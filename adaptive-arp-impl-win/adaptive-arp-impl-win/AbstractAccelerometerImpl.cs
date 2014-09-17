using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public abstract class AbstractAccelerometerImpl : IAccelerometer
    {
        private List<IAccelerationListener> listenerList;

        public AbstractAccelerometerImpl()
        {
            listenerList = new List<IAccelerationListener>();
        }

        public void AddAccelerationListener(IAccelerationListener listener)
        {
            if (listener != null && !listenerList.Contains(listener))
            {
                listenerList.Add(listener);
                if (!isTracking()) startTracking();
            }
        }

        public void RemoveAccelerationListener(IAccelerationListener listener)
        {
            if (listener != null && listenerList.Contains(listener))
            {
                listenerList.Remove(listener);
            }
        }

        public void RemoveAccelerationListeners()
        {
            listenerList.Clear();
            if (isTracking()) stopTracking();
        }

        protected abstract void startTracking();

        protected abstract void stopTracking();

        protected abstract bool isTracking();

        public void notifyResult(Acceleration acceleration)
        {
            foreach (IAccelerationListener listener in listenerList)
            {
                listener.OnResult(acceleration);
            }
        }

        public void notifyWarning(Acceleration acceleration, IAccelerationListener.Warning warning)
        {
            foreach (IAccelerationListener listener in listenerList)
            {
                listener.OnWarning(acceleration, warning);
            }
        }

        public void notifyError(IAccelerationListener.Error error)
        {
            foreach (IAccelerationListener listener in listenerList)
            {
                listener.OnError(error);
            }
        }
    }
}
