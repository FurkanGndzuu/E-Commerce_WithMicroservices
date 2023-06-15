using AutoMapper;
using CatalogService.API.Abstractions;
using CatalogService.API.Abstractions.Repositories.ProductRepositories;
using CatalogService.API.DTOs;
using CatalogService.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SharedService.Responses;

namespace CatalogService.API.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IStorageService _storageService;
        readonly IMapper _mapper;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IStorageService storageService, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> AddProductPhoto(IFormFileCollection files, string ProductId)
        {
            Product? product = await _productReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(ProductId));
            if(product is not null)
            {
                IList<string> AddedFiles = await _storageService.AddFile(files);
                foreach (var file in AddedFiles)
                    product.AllPictureUrl.Add(new Photo()
                    {
                        Id = Guid.NewGuid(),
                        photoUrl = file,
                        ProductId = product.Id.ToString(),
                        CurrentPhoto = false
                    });
                await _productWriteRepository.SaveAsync();
                return Response<NoContent>.Success(StatusCode: StatusCodes.Status201Created);
            }
            return Response<NoContent>.Fail("File is not added",StatusCode: StatusCodes.Status500InternalServerError);
        }

        public async Task<Response<NoContent>> CreateProduct(CreateProductDto product)
        {
            await _productWriteRepository.AddAsync(_mapper.Map<Product>(product));
            await _productWriteRepository.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> DeleteProduct(string Id)
        {
            Product? product = await _productReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(Id));
            if(product is not null)
            {
                _productWriteRepository.Remove(product);
                await _productWriteRepository.SaveAsync();
                return Response<NoContent>.Success(StatusCode: StatusCodes.Status200OK);
            }
            return Response<NoContent>.Fail("Product is not removed" , StatusCodes.Status500InternalServerError);
        }

        public async Task<Response<List<ProductDTO>>> GetAllProducts() => Response<List<ProductDTO>>.Success(data :  _mapper.Map<List<ProductDTO>>(await _productReadRepository.GetAll().ToListAsync()),StatusCode:StatusCodes.Status200OK);
        public async Task<Response<ProductDTO>> GetProductById(string id) => Response<ProductDTO>.Success(data :  _mapper.Map<ProductDTO>(await _productReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(id))),StatusCode:StatusCodes.Status200OK);

        public async Task<Response<NoContent>> UpdateProduct(UpdateProductDto updateProduct)
        {
            Product? product = await _productReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(updateProduct.Id));
            if(product is not null )
            {
                product.Price = updateProduct.Price;
                product.Description = updateProduct.Description;
                product.CategoryId = Guid.Parse(updateProduct.CategoryId);
                product.Name = updateProduct.Name;
                await _productWriteRepository.SaveAsync();
                return Response<NoContent>.Success(StatusCode: StatusCodes.Status200OK);
            }
            return Response<NoContent>.Fail("File is not added", StatusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
