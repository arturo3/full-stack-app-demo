using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.Entities;
using full_stack_app_demo.Server.Data.Repositories;

namespace full_stack_app_demo.Server.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> CreateAsync(ProductCreateRequest request)
    {
        var entity = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CreatedDate = DateTime.UtcNow,
            IsActive = request.IsActive,
            CategoryId = request.CategoryId
        };

        await _productRepository.AddAsync(entity);
        await _productRepository.SaveChangesAsync();

        return await _productRepository.GetActiveProductByIdAsync(entity.Id)
            ?? new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity,
                CreatedDate = entity.CreatedDate,
                IsActive = entity.IsActive,
                CategoryId = entity.CategoryId
            };
    }

    public async Task<ProductDto?> UpdateAsync(int id, ProductUpdateRequest request)
    {
        var entity = await _productRepository.GetActiveEntityByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.StockQuantity = request.StockQuantity;
        entity.IsActive = request.IsActive;
        entity.CategoryId = request.CategoryId;

        _productRepository.Update(entity);
        await _productRepository.SaveChangesAsync();

        return await _productRepository.GetActiveProductByIdAsync(entity.Id)
            ?? new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity,
                CreatedDate = entity.CreatedDate,
                IsActive = entity.IsActive,
                CategoryId = entity.CategoryId
            };
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _productRepository.GetActiveEntityByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.IsActive = false;
        _productRepository.Update(entity);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}
