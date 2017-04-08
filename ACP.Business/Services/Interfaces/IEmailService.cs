using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string Server, string From, string To, string Subject, string Body, string Password, int Port);

            /// <summary>
            /// It allows you to send an emamil.
            /// </summary>
            /// <param name="from"></param>
            /// <param name="fromname"></param>
            /// <param name="to"></param>
            /// <param name="toname"></param>
            /// <param name="key"></param>
            /// <param name="IsHtml"></param>
            /// <param name="body"></param>
            /// <param name="subject"></param>
            /// <returns></returns>
            Task<bool> SendEmail(string from, string fromname, string to, string toname, string key, bool IsHtml, string body = null, string subject = null);
    }
}