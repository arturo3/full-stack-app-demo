using full_stack_app_demo.Server.Data;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace full_stack_app_demo.Server.Data.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<CategoryDto>> GetActiveCategoriesAsync()
    {
        return Context.Categories
            .AsNoTracking()
            .Where(category => category.IsActive)
            .Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            })
            .ToListAsync();
    }
}
