using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<Order  , OrderDTO>().ReverseMap();
                CreateMap<OrderItem , OrderItemDTO>().ReverseMap();
        }
    }
}
