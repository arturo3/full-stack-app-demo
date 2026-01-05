namespace full_stack_app_demo.Server.Data.DTO.Requests;

public class CategoryCreateRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}
