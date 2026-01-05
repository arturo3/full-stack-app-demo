using full_stack_app_demo.Server.Data.DTO;
using full_stack_app_demo.Server.Data.DTO.Requests;
using full_stack_app_demo.Server.Data.Repositories;
using full_stack_app_demo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace full_stack_app_demo.Server.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public ProductsController(IProductRepository productRepository, IProductService productService)
    {
        _productRepository = productRepository;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await _productRepository.GetActiveProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productRepository.GetActiveProductByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateRequest request)
    {
        var created = await _productService.CreateAsync(request);

        if (created is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] ProductUpdateRequest request)
    {
        var updated = await _productService.UpdateAsync(id, request);

        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var deleted = await _productService.SoftDeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent(); // Should return 200?
    }
}
