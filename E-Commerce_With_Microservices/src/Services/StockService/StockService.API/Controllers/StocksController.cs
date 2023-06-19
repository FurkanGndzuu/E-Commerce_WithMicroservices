using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockService.API.Abstractions;
using StockService.API.DTOs;

namespace StockService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> AddStock([FromBody] StockDTO stock) => Ok(await _stockService.AddStock(stock));

        [HttpPut]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> UpdateStock([FromBody] StockDTO stock) => Ok(await _stockService.UpdateStock(stock));
        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetAll() => Ok(await _stockService.GetAll());
    }
}
