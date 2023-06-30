namespace NotificationService.API.Abstractions
{
    public interface IMailService
    {
        Task MailSender(string userId , string title, string body , bool isBodyHtmlTrue = true);
        Task MailSender(IList<string> userId, string title, string body, bool isBodyHtmlTrue = true);
    }
}
