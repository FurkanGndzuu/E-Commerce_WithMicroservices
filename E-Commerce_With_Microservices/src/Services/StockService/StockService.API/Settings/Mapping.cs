using AutoMapper;
using StockService.API.DTOs;
using StockService.API.Models;

namespace StockService.API.Settings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<Stock  , StockDTO>().ReverseMap();
        }
    }
}
