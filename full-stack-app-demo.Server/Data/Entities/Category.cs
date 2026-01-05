namespace full_stack_app_demo.Server.Data.Entities;

public class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
