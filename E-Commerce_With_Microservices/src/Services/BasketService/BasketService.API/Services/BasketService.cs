using BasketService.API.Abstractions;
using BasketService.API.DTOs;
using SharedService.Responses;
using System.Text.Json;

namespace BasketService.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> AddItem(AddItemDTO item)
        {
            var basket = await _redisService.GetDb().StringGetAsync(item.UserId);
            if (String.IsNullOrEmpty(basket))
            {
                BasketDTO basketDTO = new BasketDTO()
                {
                    UserId = item.UserId,
                };
                basketDTO.BasketItems.Add(new()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    ProductPrice = item.ProductPrice,

                });
                await _redisService.GetDb().StringSetAsync(item.UserId, JsonSerializer.Serialize<BasketDTO>(basketDTO));
                return Response<bool>.Success(true , StatusCodes.Status200OK);
            }
            BasketDTO basketDT = JsonSerializer.Deserialize<BasketDTO>(basket);
            basketDT.BasketItems.Add(new()
            {
                ProductPrice = item.ProductPrice,
                ProductId = item.ProductId,
                ProductName= item.ProductName,
                Quantity = item.Quantity,
            });
            await _redisService.GetDb().StringSetAsync(item.UserId, JsonSerializer.Serialize<BasketDTO>(basketDT));
            return Response<bool>.Success(true, StatusCodes.Status200OK);
        }

        public async Task<Response<bool>> DeleteItem(AddOrUpdateOrDeleteId item)
        {
            var basket = await _redisService.GetDb().StringGetAsync(item.UserId);
            BasketDTO basketDTO = JsonSerializer.Deserialize<BasketDTO>(basket);
            basketDTO.BasketItems.Remove(basketDTO.BasketItems.FirstOrDefault(x => x.ProductId.Equals(item.ProductId)));
            await _redisService.GetDb().StringSetAsync(item.UserId, JsonSerializer.Serialize<BasketDTO>(basketDTO));
            return Response<bool>.Success(true, StatusCodes.Status200OK);
        }

        public async Task<Response<BasketDTO>> GetBasket(string userId)
        {
          var basket =  await _redisService.GetDb().StringGetAsync(userId);
            if (String.IsNullOrEmpty(basket))
            {
                BasketDTO dto = new BasketDTO()
                {
                    UserId = userId,
                };
                await _redisService.GetDb().StringSetAsync(userId , JsonSerializer.Serialize<BasketDTO>(dto));
                return Response<BasketDTO>.Success(dto, StatusCodes.Status200OK);
            }
            return Response<BasketDTO>.Success(JsonSerializer.Deserialize<BasketDTO>(basket), StatusCodes.Status200OK);
        }

    
        public async Task<Response<bool>> UpdateItem(AddOrUpdateOrDeleteId item)
        {
            var basket = await _redisService.GetDb().StringGetAsync(item.UserId);
            BasketDTO basketDTO = JsonSerializer.Deserialize<BasketDTO>(basket);
            basketDTO.BasketItems.FirstOrDefault(x => x.ProductId.Equals(item.ProductId)).Quantity++;
            await _redisService.GetDb().StringSetAsync(item.UserId, JsonSerializer.Serialize<BasketDTO>(basketDTO));
            return Response<bool>.Success(true, StatusCodes.Status200OK);
        }
    }
}
