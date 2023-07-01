using MassTransit;
using NotificationService.API.Abstractions;
using SharedService.Abstractions;

namespace NotificationService.API.Consumers
{
    public class OrderCompletedRequestEventConsumer : IConsumer<IOrderCompletedRequestEvent>
    {
        readonly IMailService _mailService;
        readonly IConfiguration _configuration;

        public OrderCompletedRequestEventConsumer(IMailService mailService, IConfiguration configuration)
        {
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<IOrderCompletedRequestEvent> context)
        {
            await _mailService.MailSender(context.Message.UserId, _configuration["Mails:CompletedTitle"], $"{context.Message.OrderId} Numaralı {_configuration["Mails:CompletedBody"]}");
        }
    }
}
