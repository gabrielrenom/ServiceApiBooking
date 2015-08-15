using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACP.HMAC.Interfaces
{
    public interface ISignatureManager
    {
        string SignatureHash(string secret, string value);

        Task<bool> IsMd5Valid(HttpRequestMessage requestMessage);

        Task<byte[]> ComputeHash(HttpContent httpContent);
    }
}
