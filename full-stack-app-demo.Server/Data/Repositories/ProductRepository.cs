using full_stack_app_demo.Server.Data;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace full_stack_app_demo.Server.Data.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<ProductDto>> GetActiveProductsAsync()
    {
        return Context.Products
            .AsNoTracking()
            .Where(product => product.IsActive)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedDate = product.CreatedDate,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
                Category = product.Category == null
                    ? null
                    : new CategoryDto
                    {
                        Id = product.Category.Id,
                        Name = product.Category.Name,
                        Description = product.Category.Description,
                        IsActive = product.Category.IsActive
                    }
            })
            .ToListAsync();
    }

    public Task<ProductDto?> GetActiveProductByIdAsync(int id)
    {
        return Context.Products
            .AsNoTracking()
            .Where(product => product.IsActive && product.Id == id)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedDate = product.CreatedDate,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
                Category = product.Category == null
                    ? null
                    : new CategoryDto
                    {
                        Id = product.Category.Id,
                        Name = product.Category.Name,
                        Description = product.Category.Description,
                        IsActive = product.Category.IsActive
                    }
            })
            .FirstOrDefaultAsync();
    }

    public Task<Product?> GetActiveEntityByIdAsync(int id)
    {
        return Context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.IsActive && product.Id == id);
    }
}
