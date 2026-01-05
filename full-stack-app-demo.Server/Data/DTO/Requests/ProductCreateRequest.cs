namespace full_stack_app_demo.Server.Data.DTO.Requests;

public class ProductCreateRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
}
