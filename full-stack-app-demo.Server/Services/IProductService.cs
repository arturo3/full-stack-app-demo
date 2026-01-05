using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.DTO.Requests;

namespace full_stack_app_demo.Server.Services;

public interface IProductService
{
    Task<ProductDto?> CreateAsync(ProductCreateRequest request);

    Task<ProductDto?> UpdateAsync(int id, ProductUpdateRequest request);

    Task<bool> SoftDeleteAsync(int id);
}
