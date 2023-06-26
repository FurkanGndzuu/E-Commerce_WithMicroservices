using MassTransit;
using MassTransit.Transports;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
using OrderService.Application.Repositories.Order;
using OrderService.Domain.AggregateModels;
using SharedService.Abstractions;
using SharedService.Events;
using SharedService.Identity;
using SharedService.Responses;
using SharedService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, SharedService.Responses.Response<CreateOrderCommandResponse>>
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly ISharedIdentityService _sharedIdentityService;

        public CreateOrderCommandHandler(ISendEndpointProvider sendEndpointProvider, ISharedIdentityService sharedIdentityService, IOrderWriteRepository orderWriteRepository)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _sharedIdentityService = sharedIdentityService;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<SharedService.Responses.Response<CreateOrderCommandResponse>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Address address = new Address(request.Address.Country, request.Address.City, request.Address.District, request.Address.Street, request.Address.HouseNumber);

            Order order = new Order(_sharedIdentityService.GetUserIdAsync(), address);

            var orderCreatedRequestEvent = new OrderCreatedRequestEvent()
            {
                BuyerId = _sharedIdentityService.GetUserIdAsync(),
                OrderId = order.Id,
                WalletId = request.walletId,
                TotalPrice = order.GetTotalPrice
            };

            request.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId , item.ProductName , item.ProductPrice , item.PictureUrl , item.ProductQuantity);
                orderCreatedRequestEvent.OrderItems.Add(new SharedService.Messages.OrderItemMessage()
                {
                    ProductId = item.ProductId,
                    Count = item.ProductQuantity
                });
            });

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitmqSettings.OrderSaga}"));

           await sendEndpoint.Send(orderCreatedRequestEvent);

            return SharedService.Responses.Response<CreateOrderCommandResponse>.Success(new CreateOrderCommandResponse(), StatusCodes.Status200OK);
        }
    }
}

