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

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            string s=  sBuilder.ToString();

            return hash.SequenceEqual(hashHeader);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}
