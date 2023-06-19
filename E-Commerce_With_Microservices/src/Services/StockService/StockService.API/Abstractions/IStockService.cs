using SharedService.Responses;
using StockService.API.DTOs;

namespace StockService.API.Abstractions
{
    public interface IStockService
    {
        Task<Response<NoContent>> UpdateStock(StockDTO stock);
        Task<Response<NoContent>> AddStock(StockDTO stockDTO);
    }
}
