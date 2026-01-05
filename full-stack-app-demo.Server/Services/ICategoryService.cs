using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.DTO.Requests;

namespace full_stack_app_demo.Server.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CategoryCreateRequest request);
}
