using MassTransit;
using NotificationService.API.Abstractions;
using SharedService.Abstractions;

namespace NotificationService.API.Consumers
{
    public class OrderRequestFailedEventConsumer : IConsumer<IOrderRequestFailedEvent>
    {
        readonly IMailService _mailService;

        public OrderRequestFailedEventConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<IOrderRequestFailedEvent> context)
        {
            await _mailService.MailSender(context.Message.UserId, "Sipariş Tamamlanamadı", $"Siparişiniz {context.Message.FailedMessage} Hatası Yüzünden Tanımlanmadı ve Para Cüzdan Hesabınızdan Çekilmedi <br/> İYİ GÜNLER DİLERİZ...");
        }
    }
}
