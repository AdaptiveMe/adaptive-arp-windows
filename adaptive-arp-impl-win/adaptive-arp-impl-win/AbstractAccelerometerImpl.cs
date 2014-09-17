/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * 
 *
 * =====================================================================================================================
 */
using Adaptive.Arp.Api;
using System.Collections.Generic;

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
