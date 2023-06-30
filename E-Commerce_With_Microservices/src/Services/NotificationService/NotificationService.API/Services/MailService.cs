using NotificationService.API.Abstractions;
using System.Net.Mail;
using System.Net;
using IdentityService.API.Abstractions;
using static IdentityServer4.Models.IdentityResources;

namespace NotificationService.API.Services
{
    public class MailService : IMailService
    {
        readonly IUserService _userService;
        readonly IConfiguration _configuration;

        public MailService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task MailSender(string userId, string title, string body, bool isBodyHtmlTrue = true)
        {
          await  MailSender(new List<string> { userId }, title, body, isBodyHtmlTrue);
        }

        public async Task MailSender(IList<string> userId, string title, string body, bool isBodyHtmlTrue = true)
        {
           foreach(var item in userId)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(_configuration["Mail:userName"]);
                    message.To.Add(new MailAddress(await _userService.FindEmail(item)));
                    message.Subject = title;
                    message.IsBodyHtml = isBodyHtmlTrue;   
                    message.Body = body;
                    smtp.Port = 587;
                    smtp.Host = _configuration["Mail:host"];   
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_configuration["Mail:userName"], _configuration["Mail:password"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                 }
                catch (Exception e)
                {
             
                }
            }
        }
    }
}
