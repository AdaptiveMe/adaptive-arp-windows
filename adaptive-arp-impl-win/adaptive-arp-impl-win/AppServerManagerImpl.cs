using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptive.Arp.Api;

namespace Adaptive.Arp.Impl
{
    public class AppServerManagerImpl : IAppServerManager
    {
        private List<IAppServerListener> serverListenerList;
        private List<IAppServer> serverList;

        public AppServerManagerImpl()
        {
            serverListenerList = new List<IAppServerListener>();
            serverList = new List<IAppServer>();
        }

        public void AddServerListener(IAppServerListener listener)
        {
            if (listener !=null && !serverListenerList.Contains(listener))
            {
                serverListenerList.Add(listener);
            }
        }

        public IAppServer[] GetServers()
        {
            return serverList.ToArray();
        }

        public void PauseServer(IAppServer server)
        {
            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnPausing(server);
            }

            server.PauseServer();

            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnPaused(server);
            }
        }

        public void RemoveServerListener(IAppServerListener listener)
        {
            if (listener != null && serverListenerList.Contains(listener))
            {
                serverListenerList.Remove(listener);
            }
        }

        public void RemoveServerListeners()
        {
            serverListenerList.Clear();
        }

        public void ResumeServer(IAppServer server)
        {
            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnResuming(server);
            }

            server.ResumeServer();

            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnResumed(server);
            }
        }

        public void StartServer()
        {
            foreach (IAppServerListener listener in serverListenerList)
            {
                IAppServer server = new AppServerImpl();      
                // Start server missing???
                listener.OnStart(server);
            }
        }

        public void StopServer(IAppServer server)
        {
            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnStopping(server);
            }

            server.StopServer();

            foreach (IAppServerListener listener in serverListenerList)
            {
                listener.OnStopped(server);
            }
        }

        public void StopServers()
        {
            foreach (IAppServer server in serverList)
            {
                foreach (IAppServerListener listener in serverListenerList)
                {
                    listener.OnStopping(server);
                }

                server.StopServer();

                foreach (IAppServerListener listener in serverListenerList)
                {
                    listener.OnStopped(server);
                }
            }
        }
    }
}
