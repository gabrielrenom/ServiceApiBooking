using ACP.Business.Services.Interfaces;
using ACP.HMAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ServiceAPI.Handlers
{
    public class HMACinHandler: DelegatingHandler
    {
        private const string UnauthorizedMessage = "Unauthorized request";

        private readonly IHMACService _HMACService;

        public HMACinHandler(IHMACService HMACService)
        {            
            _HMACService = HMACService;
        }

        protected async Task<bool> IsAuthenticated(HttpRequestMessage requestMessage)
        {
            if (requestMessage.Headers.Authorization.Scheme != Header.AUTH_HEADER)
            {
                return false;
            }

            var isDateValid = _HMACService.IsDateValid(requestMessage);
            if (!isDateValid)
            {
                return false;
            }

            if (requestMessage.Headers.Authorization == null
                || requestMessage.Headers.Authorization.Scheme != Header.AUTH_HEADER)
            {
                return false;
            }
            string username = requestMessage.Headers.GetValues(Header.USERNAME_HEADER).First();
            var secret = _HMACService.GetKeyFromUser(username);
            if (secret == null)
            {
                return false;
            }

            var representation = _HMACService.MessageBuilder(requestMessage);
            if (representation == null)
            {
                return false;
            }

            if (requestMessage.Content.Headers.ContentMD5 != null
                && !await _HMACService.IsMd5Valid(requestMessage))
            {
                return false;
            }

            var signature = _HMACService.SignatureHash(secret, representation);

            //if(MemoryCache.Default.Contains(signature))
            //{
            //    return false;
            //}

            var result = requestMessage.Headers.Authorization.Parameter == signature;
            //if (result == true)
            //{
            //    MemoryCache.Default.Add(signature, username,
            //                            DateTimeOffset.UtcNow.AddMinutes(Configuration.ValidityPeriodInMinutes));
            //}
            return result;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var isAuthenticated = await IsAuthenticated(request);

            if (!isAuthenticated)
            {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, UnauthorizedMessage);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Header.AUTH_HEADER));
                return response;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}