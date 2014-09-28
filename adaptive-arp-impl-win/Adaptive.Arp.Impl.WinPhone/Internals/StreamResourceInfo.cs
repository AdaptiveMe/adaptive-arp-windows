using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Adaptive.Arp.Impl.WinPhone.Internals
{
    public class StreamResourceInfo
    {
        private Stream stream;
        private String mimeType;

        public String ContentType
        {
            get
            {
                return this.mimeType;
            }
        }

        public Stream Stream
        {
            get
            {
                return this.stream;
            }
        }

        public StreamResourceInfo(Stream stream, String mimeType)
        {
            this.stream = stream;
            this.mimeType = mimeType;
        }
    }
}
