namespace full_stack_app_demo.Server.Data.ReadModels;

public class ProductFilterRow
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryDescription { get; set; }

    public bool? CategoryIsActive { get; set; }
}
