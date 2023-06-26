using MassTransit;
using MongoDB.Driver;
using PaymentService.API.Models;
using PaymentService.API.Settings;
using SharedService.Abstractions;
using SharedService.Events;
using SharedService.Identity;

namespace PaymentService.API.Consumers
{
    public class StockReservedRequestPaymentEventConsumer : IConsumer<IStockReservedRequestPaymentEvent>
    {
        private readonly IMongoCollection<Wallet> _walletCollection;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedRequestPaymentEventConsumer(IDatabaseSettings databaseSettings, ISharedIdentityService identityService, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _walletCollection = database.GetCollection<Wallet>(databaseSettings.WalletCollectionName);
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<IStockReservedRequestPaymentEvent> context)
        {
           var wallet = await _walletCollection.Find(x => x.UserId == context.Message.UserId).FirstOrDefaultAsync();

            if(wallet is null)
            {
                wallet = new Wallet()
                {
                    UserId = context.Message.UserId,
                    Amount =0
                };
               await _walletCollection.InsertOneAsync(wallet);

                await _publishEndpoint.Publish(new PaymentFailedEvent(context.Message.CorrelationId)
                {
                    OrderItems = context.Message.OrderItems,
                    UserId = context.Message.UserId,    
                    Reason = "There is no enough money"
                });
            }
            else
            {
                if(wallet.Amount >= context.Message.TotalPrice)
                {
                    wallet.Amount -= context.Message.TotalPrice;

                   await _walletCollection.FindOneAndReplaceAsync(x => x.UserId == context.Message.UserId, wallet);

                    await _publishEndpoint.Publish(new PaymentCompletedEvent(context.Message.CorrelationId));
                }
                else
                {
                    await _publishEndpoint.Publish(new PaymentFailedEvent(context.Message.CorrelationId)
                    {
                        OrderItems = context.Message.OrderItems,
                        UserId = context.Message.UserId,
                        Reason = "There is no enough money"
                    });
                }
            }
        }
    }
}
