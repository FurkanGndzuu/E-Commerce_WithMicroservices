﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.CQRS.Commands.CreateOrder;
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

        [HttpGet("all-orders")]
        [Authorize(Policy = "ReadAdmin")]
        public async Task<IActionResult> GetOrders(GetOrderQueryRequest request) => Ok(await _mediatr.Send(request));
        [HttpGet]
        [Authorize(Policy = "ReadAdmin")]
        public async Task<IActionResult> GetOrderById(GetOrderByIdQueryRequest request) => Ok(await _mediatr.Send(request));
        [HttpGet("user-orders")]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetOrderByUserId(GetOrdersByUserIdQueryRequest request) => Ok(await _mediatr.Send(request));
        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request) => Ok(await _mediatr.Send(request));
    }
}
