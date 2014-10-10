using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.Util
{
    public class UserAgentUtils
    {
        public static string GetUserAgent()
        {
            String osPlatform = "Windows";
            String osVersion = "8.1";
            return "X-Server: Adaptive 1.0 (" + osPlatform + " " + osVersion + "/" + Environment.ProcessorCount + " Cores)";
        }
    }
}
