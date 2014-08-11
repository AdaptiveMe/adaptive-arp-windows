extern alias pcl;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using IDeriveBytes = pcl::System.Security.Cryptography.Adaptation.IDeriveBytes;

namespace System.Security.Cryptography.Adaptation
{
    // Adapts a WinRT KeyDerivationAlgorithmProvider to a IDeriveBytes
    public class Rfc2898DeriveBytesAdapter : IDeriveBytes
    {
        private readonly CryptographicKey _password;
        private int _iterationCount;
        private byte[] _salt;
        
        public Rfc2898DeriveBytesAdapter(string algorithmName, string password)
        {
            Debug.Assert(algorithmName != null && algorithmName.Length > 0);

            KeyDerivationAlgorithmProvider provider = KeyDerivationAlgorithmProvider.OpenAlgorithm(algorithmName);

            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);

            _password = provider.CreateKey(buffer);
        }

        public int IterationCount
        {
            get { return _iterationCount; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _iterationCount = value;
            }
        }

        public byte[] Salt
        {
            get { return (byte[])_salt.Clone(); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("salt");

                if (value.Length < 8)
                    throw new ArgumentException();

                _salt = (byte[])value.Clone();
            }
        }

        public byte[] GetBytes(int cb)
        {
            if (cb < 0)
                throw new ArgumentOutOfRangeException("cb");

            KeyDerivationParameters parameters = KeyDerivationParameters.BuildForPbkdf2(_salt.AsBuffer(), (uint)_iterationCount);

            IBuffer keyMaterial = CryptographicEngine.DeriveKeyMaterial(_password, parameters, (uint)cb);

            return keyMaterial.ToArray();
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
