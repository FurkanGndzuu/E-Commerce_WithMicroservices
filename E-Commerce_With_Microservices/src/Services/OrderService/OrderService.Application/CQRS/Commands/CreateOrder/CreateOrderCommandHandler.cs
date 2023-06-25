using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
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
        readonly ISharedIdentityService _sharedIdentityService;

        public CreateOrderCommandHandler(ISendEndpointProvider sendEndpointProvider, ISharedIdentityService sharedIdentityService)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _sharedIdentityService = sharedIdentityService;
        }

        public Task<SharedService.Responses.Response<CreateOrderCommandResponse>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Address address = new Address(request.Address.Country, request.Address.City, request.Address.District, request.Address.Street, request.Address.HouseNumber);
            Order order = new Order(_sharedIdentityService.GetUserIdAsync(), address);
            

        }
    }
}

