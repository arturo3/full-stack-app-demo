using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Controllers.Models.Requests;
using full_stack_app_demo.Server.Data.Repositories;
using full_stack_app_demo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace full_stack_app_demo.Server.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryRepository categoryRepository, ICategoryService categoryService)
    {
        _categoryRepository = categoryRepository;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var categories = await _categoryRepository.GetActiveCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryCreateRequest request)
    {
        var created = await _categoryService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }
}
