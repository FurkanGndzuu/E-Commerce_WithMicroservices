using AutoMapper;
using CatalogService.API.Abstractions;
using CatalogService.API.Abstractions.Repositories.CatalogRepositories;
using CatalogService.API.Abstractions.Repositories.CategoryRepositories;
using CatalogService.API.DTOs;
using CatalogService.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SharedService.Responses;

namespace CatalogService.API.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryReadRepository  _categoryReadRepository;
        readonly ICategoryWriteRepository _categoryWriteRepository;
        readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> AddCategory(string categoryName)
        {
            await _categoryWriteRepository.AddAsync(new Models.Entities.Category()
            {
                Id = Guid.NewGuid(),
                Name = categoryName
            });
            await _categoryWriteRepository.SaveAsync();
            return Response<NoContent>.Success(StatusCode: StatusCodes.Status201Created);
        }

        public async Task<Response<List<CategoryDTO>>> GetAllCategories() => Response<List<CategoryDTO>>.Success(data: _mapper.Map<List<CategoryDTO>>(await _categoryReadRepository.GetAll().ToListAsync()) , StatusCode: StatusCodes.Status200OK);

        public async Task<Response<CategoryDTO>> GetCategoryById(string Id) => Response<CategoryDTO>.Success(data: _mapper.Map<CategoryDTO>(await _categoryReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(Id))), StatusCode: StatusCodes.Status200OK);

        public async Task<Response<NoContent>> RemoveCategoryById(string Id)
        {
            Category? category = await _categoryReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(Id));
            if (category is not null)
            {
                _categoryWriteRepository.Remove(category);
                await _categoryWriteRepository.SaveAsync();
                return Response<NoContent>.Success(StatusCode: StatusCodes.Status200OK);
            }
                
            return Response<NoContent>.Fail("Product is not Removed", StatusCodes.Status500InternalServerError);
        }

        public async Task<Response<NoContent>> UpdateCategory(CategoryDTO categoryDto)
        {
            Category? category = await _categoryReadRepository.GetSingleAsync(x => x.Id.ToString().Equals(categoryDto.Id));
            if(category is not null)
            {
                category.Name = categoryDto.Name;
                await _categoryWriteRepository.SaveAsync();
                return Response<NoContent>.Success(StatusCode: StatusCodes.Status204NoContent);
            }
            return Response<NoContent>.Fail("Product was not updated" , StatusCode : StatusCodes.Status500InternalServerError);
        }
    }
}
