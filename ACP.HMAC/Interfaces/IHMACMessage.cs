using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACP.HMAC.Interfaces
{
    public interface IHMACMessage
    {
        string MessageBuilder(HttpRequestMessage requestMessage);
    }
}
