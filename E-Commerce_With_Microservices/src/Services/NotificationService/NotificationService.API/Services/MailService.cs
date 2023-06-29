using NotificationService.API.Abstractions;

namespace NotificationService.API.Services
{
    public class MailService : IMailService
    {
        public Task SendMail(string UserId, string Title, string Content, bool Html = true)
        {
            throw new NotImplementedException();
        }

        public Task SendMail(IList<string> UserId, string Title, string Content, bool Html = true)
        {
            throw new NotImplementedException();
        }
    }
}
