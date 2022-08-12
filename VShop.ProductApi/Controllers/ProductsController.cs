using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ProductDTO>> Get()
    {
        var productsDto = await _productService.GetProducts();

        if (productsDto is null)
            return NotFound("Categories not found");

        return Ok(productsDto);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var categoriesDto = await _productService.GetProductById(id);

        if (categoriesDto == null)
            return NotFound("Product not found");

        return Ok(categoriesDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productsDto)
    {
        if (productsDto == null)
            return BadRequest("Invalid Data");

        await _productService.AddProduct(productsDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productsDto.Id }, productsDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ProductDTO productsDto)
    {
        if (productsDto == null)
            return BadRequest();

        await _productService.UpdateProduct(productsDto);

        return Ok(productsDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var productsDto = await _productService.GetProductById(id);

        if (productsDto == null)
            return NotFound("Product not found");

        await _productService.RemoveProduct(id);

        return Ok(productsDto);
    }

}
