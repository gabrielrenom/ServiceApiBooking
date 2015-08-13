using ACP.HMAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACP.HMAC.Services
{
    public class SignatureManager : ISignatureManager
    {
        public string SignatureHash(string secret, string value)
        {
            string signature;

            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
           
            using (var hmac = new HMACSHA256(secretBytes))
            {
                byte[] hash = hmac.ComputeHash(valueBytes);
                signature = Convert.ToBase64String(hash);
            }

            return signature;
        }
    }
}
