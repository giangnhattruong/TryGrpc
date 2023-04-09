using Microsoft.AspNetCore.Mvc;
using ProductGrpcService.Database;
using ProductGrpcService.Domain.Models;
using ProductGrpcService.Services;

namespace ProductGrpcService.Controllers;

[Route("api/v1.0/products")]
[Produces("application/json")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        return Ok(await _productService.ListAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductModel productModel)
    {
        var rs = await _productService.CreateAsync(productModel);
        return Ok(rs);
    }
}