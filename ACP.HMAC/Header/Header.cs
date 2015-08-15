using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACP.HMAC
{
    public static class Header
    {
        public enum SecurityLevel { High,Medium,Low};

        //## Request/response date (this will prevent attacks)
        public static readonly string DATE_HEADER = "X-ApiAuth-Date";

        //## User name attached in the header
        public static readonly string USERNAME_HEADER = "X-ApiAuth-Username";

        //## Authentication type (with key)
        public static readonly string AUTH_HEADER = "ApiAuth";

        //## The only defined value, "nosniff", prevents Internet Explorer and Google Chrome from MIME-sniffing a response away from the declared content-type. This also applies to Google Chrome, when downloading extensions. This reduces exposure to drive-by download attacks and sites serving user uploaded content that, by clever naming, could be treated by MSIE as executable or dynamic HTML files.
        //## ie: X-Content-Type-Options: nosniff
        public static readonly string SNIFF_HEADER = "X-Content-Type-Options";

        //## This header enables the Cross-site scripting (XSS) filter built into most recent web browsers. It's usually enabled by default anyway, so the role of this header is to re-enable the filter for this particular website if it was disabled by the user. This header is supported in IE 8+, and in Chrome (not sure which versions). The anti-XSS filter was added in Chrome 4. Its unknown if that version honored this header.
        //## ie: X-XSS-Protection: 1; mode=block
        public static readonly string XSS_HEADER = "X-XSS-Protection";

        //## HTTP Strict-Transport-Security (HSTS) enforces secure (HTTP over SSL/TLS) connections to the server. This reduces impact of bugs in web applications leaking session data through cookies and external links and defends against Man-in-the-middle attacks. HSTS also disables the ability for user's to ignore SSL negotiation warnings.
        //## ie: Simple example, using a long (1 year) max-age:  Strict-Transport-Security: max-age=31536000
        //## ie: If all present and future subdomains will be HTTPS: Strict-Transport-Security: max-age=31536000; includeSubDomains
        //## ie: Recommended: If the site owner would like their domain to be included in the HSTS preload list maintained by Chrome (and used by Firefox and Safari), then use: Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
        public static readonly string STRICT_TRANSPORT_SECURITY_HEADER = "Strict-Transport-Security";

        /// <summary>
        /// This Medthod will check the header by passing the level of security
        /// </summary>
        /// <param name="requestMessage">Request Message received from the client</param>
        /// <param name="level">Level of security  High,Medium,Low</param>
        /// <returns></returns>
        public static bool IsHeaderValid(HttpRequestMessage requestMessage, SecurityLevel level)
        {
            bool result = false;

            switch (level)
            {
                case SecurityLevel.Low:         result  =   requestMessage.Headers.Contains(USERNAME_HEADER) &&
                                                            requestMessage.Content.Headers.ContentMD5 != null ? true : false; break;
                case SecurityLevel.Medium:      result  =   requestMessage.Headers.Contains(DATE_HEADER) &&
                                                            requestMessage.Headers.Contains(USERNAME_HEADER) &&
                                                            requestMessage.Content.Headers.ContentMD5 != null ? true : false; break;
                case SecurityLevel.High:        result  =   requestMessage.Headers.Contains(DATE_HEADER) &&
                                                            requestMessage.Headers.Contains(USERNAME_HEADER) &&
                                                            requestMessage.Headers.Contains(USERNAME_HEADER) &&
                                                            requestMessage.Content.Headers.ContentMD5 != null ? true : false; break;
                default:                        result  =   requestMessage.Headers.Contains(DATE_HEADER) &&
                                                            requestMessage.Headers.Contains(USERNAME_HEADER) &&
                                                            requestMessage.Headers.Contains(SNIFF_HEADER) &&
                                                            requestMessage.Headers.Contains(XSS_HEADER) &&
                                                            requestMessage.Headers.Contains(STRICT_TRANSPORT_SECURITY_HEADER) &&
                                                            requestMessage.Content.Headers.ContentMD5 != null ? true : false; break;
            }

            return result;
        }
    }
}
