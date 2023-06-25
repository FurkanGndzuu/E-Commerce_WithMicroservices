using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
using OrderService.Application.Repositories.Order;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, Response<OrderDTO>>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }

        public async Task<Response<OrderDTO>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
           var order =await _orderReadRepository.Table.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == request.Id);
            return Response<OrderDTO>.Success(_mapper.Map<OrderDTO>(order) , StatusCodes.Status200OK);  
        }
    }
}
