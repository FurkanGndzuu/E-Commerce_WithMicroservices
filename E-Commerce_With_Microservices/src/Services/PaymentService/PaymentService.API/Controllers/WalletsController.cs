using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.API.Abstractions;
using PaymentService.API.DTOs;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        readonly IPaymentService _paymentService;

        public WalletsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetWallet() => Ok(await _paymentService.GetWallet());
        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> LoadMoney(LoadMoneyDTO dto) => Ok(await _paymentService.LoadMoney(dto));
    }
}
