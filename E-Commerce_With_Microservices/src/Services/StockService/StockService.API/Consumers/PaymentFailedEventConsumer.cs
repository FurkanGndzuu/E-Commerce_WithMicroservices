using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using SharedService.Abstractions;
using StockService.API.Models;
using StockService.API.Settings;

namespace StockService.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<IPaymentFailedEvent>
    {
        private readonly IMongoCollection<Stock> _stockCollection;
        readonly IMapper _mapper;
    

        public PaymentFailedEventConsumer(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _stockCollection = database.GetCollection<Stock>(databaseSettings.StockCollectionName);
            _mapper = mapper;
         }
        public async Task Consume(ConsumeContext<IPaymentFailedEvent> context)
        {
        if(context.Message.OrderItems.Any())
                context.Message.OrderItems.ForEach(async item =>
                {
                    var orderitem = await _stockCollection.Find(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();
                    orderitem.StockQuantity += item.Count;
                    await _stockCollection.FindOneAndReplaceAsync(x => x.ProductId == item.ProductId, orderitem);
                });

        }
    }
}
