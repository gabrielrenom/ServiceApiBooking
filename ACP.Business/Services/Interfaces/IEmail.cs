using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string Server, string From, string To, string Subject, string Body, string Password, int Port);
    }
}