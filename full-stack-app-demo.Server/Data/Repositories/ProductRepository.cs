using full_stack_app_demo.Server.Data;
using full_stack_app_demo.Server.Data.Entities;

namespace full_stack_app_demo.Server.Data.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
