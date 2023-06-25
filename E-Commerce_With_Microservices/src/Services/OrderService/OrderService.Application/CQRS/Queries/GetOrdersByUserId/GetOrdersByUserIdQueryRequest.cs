using MediatR;
using OrderService.Application.DTOs;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Queries.GetOrdersByUserId
{
    public class GetOrdersByUserIdQueryRequest : IRequest<Response<List<OrderDTO>>>
    {
    }
}
