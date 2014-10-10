using Adaptive.Arp.Impl.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public class IPhoneIO : AbstractIO
    {

        private static string _VALIDATECERTIFICATES = "$ValidateCertificates$";

        public bool ValidateCertificates
        {
            get
            {
                bool bResult;
                Debug.WriteLine("*************** Should validate certificates for remote servers? " + IPhoneIO._VALIDATECERTIFICATES);
                Boolean.TryParse(IPhoneIO._VALIDATECERTIFICATES, out bResult);
                return bResult;
            }
        }

        public override string ServicesConfigFile
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string GetDirectoryRoot()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method overrided, to use NSData to get stream from file resource inside application. 
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/>
        /// </returns>
        public override byte[] GetConfigFileBinaryData()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateWebCertificates(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (this.ValidateCertificates)
            {
                Debug.WriteLine("*************** On ServerCertificateValidationCallback: Validate web certificates");
                return SecurityUtils.ValidateWebCertificates(sender, certificate, chain, sslPolicyErrors);
            }
            else return base.ValidateWebCertificates(sender, certificate, chain, sslPolicyErrors);
        }

        /// <summary>
        /// Method overrided, to start activity notification while invoking external service. 
        /// </summary>
        /// <param name="request">
        /// A <see cref="IORequest"/>
        /// </param>
        /// <param name="service">
        /// A <see cref="IOService"/>
        /// </param>
        /// <returns>
        /// A <see cref="IOResponse"/>
        /// </returns>
        public override IOResponse InvokeService(IORequest request, IOService service)
        {
            this.IOUserAgent = UserAgentUtils.GetUserAgent();

            IOResponse response = base.InvokeService(request, service);

            return response;
        }
    }
}
