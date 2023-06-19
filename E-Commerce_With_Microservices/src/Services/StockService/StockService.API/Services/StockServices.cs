using AutoMapper;
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
        readonly IMapper _mapper;

        public StockServices(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _stockCollection = database.GetCollection<Stock>(databaseSettings.StockCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<NoContent>> UpdateStock(StockDTO stockDTO)
        {
            Stock stock = _mapper.Map<Stock>(stockDTO);

          var result =  await _stockCollection.FindOneAndReplaceAsync(x => x.ProductId.Equals(stock.ProductId), stock);
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

        public async Task<Response<List<StockDTO>>> GetAll()
        {
            var stocks = await _stockCollection.Find(x => true).ToListAsync();
            return Response<List<StockDTO>>.Success(_mapper.Map<List<StockDTO>>(stocks), StatusCodes.Status200OK);            
        }
    }
}
