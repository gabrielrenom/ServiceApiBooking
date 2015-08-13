﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using ACP.HMAC.Interfaces;

namespace ACP.HMAC.Services
{
    public class HMACMessage : IHMACMessage
    {        
        public string MessageBuilder(HttpRequestMessage requestMessage)
        {

            string result = null;

            try
            {            
                if (IsHeaderValid(requestMessage))                   
                {
                    DateTime date = Convert.ToDateTime(requestMessage.Headers.GetValues("Date").FirstOrDefault());

                    string md5 = requestMessage.Content == null || requestMessage.Content.Headers.ContentMD5 == null ? "" : Convert.ToBase64String(requestMessage.Content.Headers.ContentMD5);

                    string httpMethod = requestMessage.Method.Method;

                    string contentType = requestMessage.Content.Headers.ContentType.MediaType;
                    string username = requestMessage.Headers.GetValues("X-ApiAuth-Username").First();
                    string uri = requestMessage.RequestUri.AbsolutePath.ToLower();

                    //## More headers could be added
                    result = String.Join("\n", httpMethod, md5, date.ToString(CultureInfo.InvariantCulture), username, uri);

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }

            return result;
        }

        public bool IsHeaderValid(HttpRequestMessage requestMessage)
        {         
            return  requestMessage.Headers.Contains("Date") &&
                    requestMessage.Headers.Contains("X-ApiAuth-Username") &&
                    requestMessage.Content.Headers.ContentMD5 != null ? true : false;
        }
    }
}
