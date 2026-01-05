using full_stack_app_demo.Server.Data.Repositories;

namespace full_stack_app_demo.Server.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
}
