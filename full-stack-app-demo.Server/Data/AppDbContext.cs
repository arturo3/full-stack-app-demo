using full_stack_app_demo.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace full_stack_app_demo.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(250);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(false);

            entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m);

            entity.Property(e => e.StockQuantity)
                .HasDefaultValue(0);

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(false);

            entity.Property(e => e.CategoryId)
                .IsRequired();
        });
    }
}
