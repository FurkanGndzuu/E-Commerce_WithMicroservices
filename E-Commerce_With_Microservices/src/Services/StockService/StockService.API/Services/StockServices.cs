using MongoDB.Driver;
using SharedService.Responses;
using StockService.API.Abstractions;
using StockService.API.DTOs;
using StockService.API.Models;
using StockService.API.Settings;

namespace StockService.API.Services
{
    public class StockServices : IStockService
    {
        private readonly IMongoCollection<Stock> _stockCollection;

        public StockServices(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _stockCollection = database.GetCollection<Stock>(databaseSettings.StockCollectionName);
                 }
        public async Task<Response<NoContent>> UpdateStock(StockDTO stockDTO)
        {
            Stock stock = new Stock()
            {
                ProductId = stockDTO.ProductId,
                ProductName = stockDTO.ProductName,
                StockQuantity = stockDTO.StockQuantity, 
            };

          var result =  await _stockCollection.FindOneAndReplaceAsync(x => x.ProductId.Equals(stockDTO.ProductId), stock);
            if (result != null)
                return Response<NoContent>.Success(StatusCodes.Status200OK);
            return Response<NoContent>.Fail("Stock is not updated", StatusCodes.Status500InternalServerError);
        }
        public async Task<Response<NoContent>> AddStock(StockDTO stockDTO)
        {
            Stock stock = new Stock()
            {
                ProductId = stockDTO.ProductId,
                ProductName = stockDTO.ProductName,
                StockQuantity = stockDTO.StockQuantity,
            };

            await _stockCollection.InsertOneAsync(stock);
         return Response<NoContent>.Success(StatusCodes.Status200OK);
        }
    }
}
