
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ACP.Business.Services.Interfaces;
using ACP.HMAC.Interfaces;
using ACP.HMAC;

namespace ACP.Business.Services
{
    public class HMACService : IHMACService
    {
        private static int PERIOD_VALID_MINUTES = 0;
        private IHMACMessage _message;
        private ISignatureManager _signatureManager;   

        public HMACService(IHMACMessage message, ISignatureManager signatureManager)
        {
            _message = message;
            _signatureManager= signatureManager;
        }

        public Task<byte[]> ComputeHash(HttpContent httpContent)
        {
            return _signatureManager.ComputeHash(httpContent);
        }        

        public string GetKeyFromUser(string username)
        { 
            string result = null;

            IDictionary<string, string> _userPasswords = new Dictionary<string, string>()
                  {
                      {"username","password"}
                  };

            if (_userPasswords.ContainsKey(username))
            {
                var userPassword = _userPasswords[username];
                result = HashData(userPassword, new SHA1CryptoServiceProvider());
            }
            
            return result;
        }

        public string HashData(string inputData, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            byte[] hashed = algorithm.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashed);
        }

        public Task<bool> IsMd5Valid(HttpRequestMessage requestMessage)
        {
            return _signatureManager.IsMd5Valid(requestMessage);
        }

        public string MessageBuilder(System.Net.Http.HttpRequestMessage requestmessage)
        {
            return _message.MessageBuilder(requestmessage);
        }
        public string SignatureHash(string secret, string value)
        {
            return _signatureManager.SignatureHash(secret, value);
        }

        public bool IsDateValid(HttpRequestMessage requestMessage)
        {
            var utcNow = DateTime.UtcNow;
            //var date = requestMessage.Headers.Date.Value.UtcDateTime;
            //if (date >= utcNow.AddMinutes(PERIOD_VALID_MINUTES)
            //    || date <= utcNow.AddMinutes(PERIOD_VALID_MINUTES))
            //{
            //    return false;
            //}
            return true;
        }    
    }
}
