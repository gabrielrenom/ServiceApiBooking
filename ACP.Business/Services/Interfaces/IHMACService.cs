using ACP.HMAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IHMACService
    {
        string SignatureHash(string secret, string value);
        string GetKeyFromUser(string username);
        string HashData(string inputData, HashAlgorithm algorithm);
        string MessageBuilder(System.Net.Http.HttpRequestMessage requestmessage);
        Task<byte[]> ComputeHash(HttpContent httpContent);
        bool IsDateValid(HttpRequestMessage requestMessage);
        Task<bool> IsMd5Valid(HttpRequestMessage requestMessage);
    }
}
