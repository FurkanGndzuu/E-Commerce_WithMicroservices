namespace NotificationService.API.Abstractions
{
    public interface IMailService
    {
        Task SendMail(string UserId, string Title, string Content, bool Html = true);
        Task SendMail(IList<string> UserId, string Title, string Content, bool Html = true);
    }
}
