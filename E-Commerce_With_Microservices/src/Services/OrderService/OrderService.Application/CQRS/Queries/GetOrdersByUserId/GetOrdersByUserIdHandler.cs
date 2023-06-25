using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
using OrderService.Application.Repositories.Order;
using SharedService.Identity;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Queries.GetOrdersByUserId
{
    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQueryRequest, Response<List<OrderDTO>>>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IMapper _mapper;
        readonly ISharedIdentityService _sharedIdentityService;

        public GetOrdersByUserIdHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetOrdersByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderReadRepository.Table.Where(x => x.UserId.Equals(_sharedIdentityService.GetUserIdAsync)).ToListAsync();
            return Response<List<OrderDTO>>.Success(_mapper.Map<List<OrderDTO>>(orders) , StatusCodes.Status200OK);
        }
    }
}
