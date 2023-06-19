using BasketService.API.Abstractions;
using BasketService.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{UserId}")]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> GetBasket([FromRoute]string UserId) => Ok(await _basketService.GetBasket(UserId));
        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> AddItem([FromBody] AddItemDTO item) => Ok(await _basketService.AddItem(item));

        [HttpPut]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> UpdateItem([FromBody] AddOrUpdateOrDeleteId item) => Ok(await _basketService.UpdateItem(item));
        [HttpDelete]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> DeleteItem([FromBody] AddOrUpdateOrDeleteId item) => Ok(await _basketService.DeleteItem(item));
    }
}
