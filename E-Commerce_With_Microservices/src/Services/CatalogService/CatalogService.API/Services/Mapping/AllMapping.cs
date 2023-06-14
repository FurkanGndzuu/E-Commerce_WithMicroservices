using AutoMapper;
using CatalogService.API.DTOs;
using CatalogService.API.Models.Entities;

namespace CatalogService.API.Services.Mapping
{
    public class AllMapping : Profile
    {
        public AllMapping()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product , CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
