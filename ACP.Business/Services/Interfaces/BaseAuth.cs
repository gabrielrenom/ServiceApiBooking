using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public abstract class BaseAuth
    {
        private HashAlgorithm _cryptographyProvider;

        public enum CryptographyProvider
        {
            SHA1,
            SHA256,
            SHA384,
            SHA512,
            MD5,
        }              

        public void SetCryptographyProvider(CryptographyProvider cryptographyProvider)
        {
            switch (cryptographyProvider)
            {
                case CryptographyProvider.SHA1: _cryptographyProvider = new SHA1CryptoServiceProvider();  break;
                case CryptographyProvider.SHA256: _cryptographyProvider = new SHA256CryptoServiceProvider(); break;
                case CryptographyProvider.SHA384: _cryptographyProvider = new SHA384CryptoServiceProvider(); break;
                case CryptographyProvider.SHA512: _cryptographyProvider = new SHA512CryptoServiceProvider(); break;
                case CryptographyProvider.MD5: _cryptographyProvider = new MD5CryptoServiceProvider(); break;
                default: _cryptographyProvider = new SHA1CryptoServiceProvider(); break;                  
            }             
        }

        public HashAlgorithm GetCryptographyProvider()
        {
            return _cryptographyProvider ?? new SHA1CryptoServiceProvider();
        }
    }
}
