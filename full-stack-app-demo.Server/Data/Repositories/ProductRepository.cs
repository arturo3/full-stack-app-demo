using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;
using full_stack_app_demo.Server.Data.ReadModels;
using Microsoft.Data.SqlClient;
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

    public async Task<List<ProductDto>> GetProductsByFilter(ProductFilterQuery query)
    {
        var parameters = new[]
        {
            new SqlParameter("@SearchTerm", query.SearchTerm ?? (object)DBNull.Value),
            new SqlParameter("@CategoryId", query.CategoryId ?? (object)DBNull.Value),
            new SqlParameter("@MinPrice", query.MinPrice ?? (object)DBNull.Value),
            new SqlParameter("@MaxPrice", query.MaxPrice ?? (object)DBNull.Value),
            new SqlParameter("@InStock", query.InStock ?? (object)DBNull.Value),
            new SqlParameter("@SortBy", query.SortBy ?? (object)DBNull.Value),
            new SqlParameter("@SortOrder", query.SortOrder ?? (object)DBNull.Value),
            new SqlParameter("@PageNumber", query.PageNumber ?? (object)DBNull.Value),
            new SqlParameter("@PageSize", query.PageSize ?? (object)DBNull.Value)
        };

        var rows = await Context.Set<ProductFilterRow>()
            .FromSqlRaw(
                "EXEC dbo.GetProductsByFilterPaginated @SearchTerm, @CategoryId, @MinPrice, @MaxPrice, @InStock, @SortBy, @SortOrder, @PageNumber, @PageSize",
                parameters)
            .ToListAsync();

        return rows.Select(row => new ProductDto
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                Price = row.Price,
                StockQuantity = row.StockQuantity,
                CreatedDate = row.CreatedDate,
                IsActive = row.IsActive,
                CategoryId = row.CategoryId,
                Category = row.CategoryName is null && row.CategoryDescription is null && row.CategoryIsActive is null
                    ? null
                    : new CategoryDto
                    {
                        Id = row.CategoryId,
                        Name = row.CategoryName,
                        Description = row.CategoryDescription,
                        IsActive = row.CategoryIsActive ?? false
                    }
            })
            .ToList();
    }
}
