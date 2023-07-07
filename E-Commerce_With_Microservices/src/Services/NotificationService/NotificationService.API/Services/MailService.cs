using NotificationService.API.Abstractions;
using SharedService.Identity;
using System.Net;
using System.Net.Mail;

namespace NotificationService.API.Services
{
    public class MailService : IMailService
    {
    
        readonly IConfiguration _configuration;
        //readonly ISharedIdentityService _sharedIdentityService;

        public MailService(IConfiguration configuration)
        {

            _configuration = configuration;
            //_sharedIdentityService = sharedIdentityService;
        }

        public async Task MailSender(string userId, string title, string body, bool isBodyHtmlTrue = true)
        {
          await  MailSender(new List<string> { userId }, title, body, isBodyHtmlTrue);
        }

        public async Task MailSender(IList<string> userId, string title, string body, bool isBodyHtmlTrue = true)
        {
           foreach(var item in userId)
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(_configuration["Mail:username"]);
                msg.To.Add(/*_sharedIdentityService.GetUserEmail()*/"furkangndz93@gmail.com");
                msg.Subject = title;
                msg.Body = body;
                //msg.Priority = MailPriority.High;
                msg.IsBodyHtml = isBodyHtmlTrue;


                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_configuration["Mail:username"], _configuration["Mail:password"]);
                    client.Host = _configuration["Mail:host"];
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(msg);
                }
            }
        }
    }
}
