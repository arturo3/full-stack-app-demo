namespace full_stack_app_demo.Server.Controllers.Models.Requests;

public class ProductUpdateRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public int CategoryId { get; set; }
}
