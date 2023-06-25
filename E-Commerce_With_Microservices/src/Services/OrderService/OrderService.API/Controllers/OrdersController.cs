using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.CQRS.Queries;
using OrderService.Application.CQRS.Queries.GetOrderById;
using OrderService.Application.CQRS.Queries.GetOrdersByUserId;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediatr;

        public OrdersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [Authorize(Policy = "ReadAdmin")]
        public async Task<IActionResult> GetOrders(GetOrderQueryRequest request) => Ok(await _mediatr.Send(request));
        [HttpGet]
        [Authorize(Policy = "ReadAdmin")]
        public async Task<IActionResult> GetOrderById(GetOrderByIdQueryRequest request) => Ok(await _mediatr.Send(request));
        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetOrderByUserId(GetOrdersByUserIdQueryRequest request) => Ok(await _mediatr.Send(request));
    }
}
