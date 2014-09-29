using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;

namespace Adaptive.Arp.Impl.WinPhone.Internals
{
    public class StreamLocalResolver : IUriToStreamResolver
    {
        public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
        {
            if (uri == null) throw new Exception("No stream, no party.");
            string path = uri.AbsolutePath;
            return GetContent(path).AsAsyncOperation();
        }

        private async Task<IInputStream> GetContent(string path)
        {
            try
            {
                if (path.IndexOf('?') > 0)
                {
                    path = path.Substring(0, path.IndexOf('?'));
                }
                else if (path.IndexOf('#') > 0)
                {
                    path = path.Substring(0, path.IndexOf('#'));
                }
                else if (path.EndsWith("/"))
                {
                    path += "index.html";
                }
                Uri localUri = new Uri("ms-appx:///Html/WebResources/www" + path);
                Debug.WriteLine("- Content {0}", localUri);
                StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(localUri);
                IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
                return stream;
            }
            catch (Exception)
            {
                throw new Exception("Invalid path.");
            }
        }
    }
}
