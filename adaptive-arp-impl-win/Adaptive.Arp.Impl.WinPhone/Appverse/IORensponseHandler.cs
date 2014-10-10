using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public interface IOResponseHandler
    {
        bool HandleResponse(IOResponseHandle handle, IOResponse response);
    }
}
