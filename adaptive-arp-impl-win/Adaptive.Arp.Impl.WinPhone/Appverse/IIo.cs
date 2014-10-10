using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public interface IIo
    {

        IOService[] GetServices();

        IOService GetService(string name);

        IOService GetService(string name, ServiceType type);

        IOResponse InvokeService(IORequest request, IOService service);

        IOResponse InvokeService(IORequest request, string serviceName);

        IOResponse InvokeService(IORequest request, string serviceName, ServiceType type);

        IOResponseHandle InvokeService(IORequest request, IOService service, IOResponseHandler handler);

        IOResponseHandle InvokeService(IORequest request, string serviceName, IOResponseHandler handler);

        IOResponseHandle InvokeService(IORequest request, string serviceName, ServiceType type, IOResponseHandler handler);

        /// <summary>
        /// Invokes a service for getting a big binary, storing it into filesystem and returning the reference url.
        /// Only OCTET_BINARY service types are allowed.
        /// </summary>
        /// <returns>The reference Url for the stored file (if success, null otherwise.</returns>
        /// <param name="request">Request.</param>
        /// <param name="service">Service.</param>
        /// <param name="storePath">Store path (realtive path under application Documents folder).</param>
        string InvokeServiceForBinary(IORequest request, IOService service, string storePath);

    }//end IIo

}
