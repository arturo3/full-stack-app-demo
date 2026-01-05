using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.DTO;

namespace full_stack_app_demo.Server.Services;

public interface IProductService
{
    Task<ProductDto?> CreateAsync(ProductCreateRequest request);

    Task<ProductDto?> UpdateAsync(int id, ProductUpdateRequest request);

    Task<bool> SoftDeleteAsync(int id);
}
