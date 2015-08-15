using ACP.HMAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<byte[]> ComputeHash(HttpContent httpContent)
        {
            using (MD5 md5 = MD5.Create())
            {
                var content = await httpContent.ReadAsByteArrayAsync();
                byte[] hash = md5.ComputeHash(content);
                return hash;
            }
        }

        public async Task<bool> IsMd5Valid(HttpRequestMessage requestMessage)
        {
            var hashHeader = requestMessage.Content.Headers.ContentMD5;
            if (requestMessage.Content == null)
            {
                return hashHeader == null || hashHeader.Length == 0;
            }
            var hash = await ComputeHash(requestMessage.Content);
            return hash.SequenceEqual(hashHeader);
        }
    }
}
