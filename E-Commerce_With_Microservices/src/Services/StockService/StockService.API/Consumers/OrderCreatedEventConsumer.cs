using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using SharedService.Abstractions;
using SharedService.Events;
using SharedService.Settings;
using StockService.API.Models;
using StockService.API.Settings;

namespace StockService.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
    {
        private readonly IMongoCollection<Stock> _stockCollection;
        readonly IMapper _mapper;
        readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(IDatabaseSettings databaseSettings, IMapper mapper, IPublishEndpoint publishEndpoint = null)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _stockCollection = database.GetCollection<Stock>(databaseSettings.StockCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            IList<bool> result = new List<bool>();
            context.Message.OrderItems.ForEach(async item =>
            {
               var resultStock = await _stockCollection.Find(x => x.ProductId == item.ProductId && x.StockQuantity >= item.Count).FirstOrDefaultAsync();
                if (resultStock != null)
                    result.Add(true);
                else
                    result.Add(false);
            });

            if(result.All(x => x == true))
            {
                context.Message.OrderItems.ForEach(async item =>
                {
                    var resultStock = await _stockCollection.Find(x => x.ProductId == item.ProductId && x.StockQuantity >= item.Count).FirstOrDefaultAsync();
                    resultStock.StockQuantity -= item.Count;
                    if (resultStock != null)
                        await _stockCollection.FindOneAndReplaceAsync(x => x.ProductId == resultStock.ProductId, resultStock);

                    });

                StockReservedEvent stockReservedEvent = new StockReservedEvent(context.Message.CorrelationId)
                {
                    OrderItems = context.Message.OrderItems,
                };
                await _publishEndpoint.Publish(stockReservedEvent);
            }
            else
            {
                StockNotReservedEvent stockNot = new StockNotReservedEvent(context.Message.CorrelationId)
                {
                    OrderItems = context.Message.OrderItems,
                    FailMessage = "Stock is not reserved"
                };
                await _publishEndpoint.Publish(stockNot);
            }
        }
    }
}
