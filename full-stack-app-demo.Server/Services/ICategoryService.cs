using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.DTO;

namespace full_stack_app_demo.Server.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CategoryCreateRequest request);
}
