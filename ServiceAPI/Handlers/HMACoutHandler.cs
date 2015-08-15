using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Handlers
{
    public class HMACoutHandler : DelegatingHandler
    {
        private readonly IHMACService _HMACService;

        public HMACoutHandler(IHMACService HMACService)
        {
            _HMACService = HMACService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
         System.Threading.CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode && response.Content != null)
            {                
                response.Content.Headers.ContentMD5 = await _HMACService.ComputeHash(response.Content);
            }
            return response;
        }
    }
}
