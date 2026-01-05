namespace full_stack_app_demo.Server.Controllers.Models.Requests;

public class CategoryCreateRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}
