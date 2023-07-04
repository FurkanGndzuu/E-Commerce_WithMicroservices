using BasketService.API.Abstractions;
using BasketService.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Identity;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        readonly IBasketService _basketService;
        readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> GetBasket() => Ok(await _basketService.GetBasket(_sharedIdentityService.GetUserIdAsync()));
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
