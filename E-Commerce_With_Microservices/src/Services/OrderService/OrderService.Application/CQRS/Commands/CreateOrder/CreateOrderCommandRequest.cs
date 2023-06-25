using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.AggregateModels;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Commands.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<Response<CreateOrderCommandResponse>>
    {
        public AddressDTO? Address { get; set; }
        public string walletId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
