using BasketService.API.DTOs;
using SharedService.Responses;

namespace BasketService.API.Abstractions
{
    public interface IBasketService
    {
       Task<Response<BasketDTO>> GetBasket(string userId);
        Task<Response<bool>> AddItem(AddItemDTO item);
        Task<Response<bool>> UpdateItem(AddOrUpdateOrDeleteId item);
        Task<Response<bool>> DeleteItem(AddOrUpdateOrDeleteId item);
    }
}
