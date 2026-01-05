using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;

namespace full_stack_app_demo.Server.Data.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<CategoryDto>> GetActiveCategoriesAsync();
}
