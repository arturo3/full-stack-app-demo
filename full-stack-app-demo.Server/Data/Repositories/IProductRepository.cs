using full_stack_app_demo.Server.Data.Entities;
using full_stack_app_demo.Server.Data.DTO;

namespace full_stack_app_demo.Server.Data.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<ProductDto>> GetActiveProductsAsync();

    Task<ProductDto?> GetActiveProductByIdAsync(int id);

    Task<Product?> GetActiveEntityByIdAsync(int id);
}
