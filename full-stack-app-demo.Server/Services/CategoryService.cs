using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;
using full_stack_app_demo.Server.Data.Repositories;

namespace full_stack_app_demo.Server.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateRequest request)
    {
        var entity = new Category
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _categoryRepository.AddAsync(entity);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
    }
}
