using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;

namespace full_stack_app_demo.Server.Data.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<ProductDto>> GetActiveProductsAsync();

    Task<ProductDto?> GetActiveProductByIdAsync(int id);

    Task<Product?> GetActiveEntityByIdAsync(int id);

    Task<List<ProductDto>> GetProductsByFilter(ProductFilterQuery query);
}
