using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShopWeb.Models;
using VShopWeb.Roles;
using VShopWeb.Services.Contracts;

namespace VShopWeb.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        
        var result = await _productService.GetAllProduct(await GetAccessToken());

        if (result is null)
            return RedirectToAction("Login", "Home"); ;

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productVM, await GetAccessToken());

            if (result != null)
                return RedirectToAction(nameof(Index));
        } 
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

        }

        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

        var result = await _productService.FindProductById(id, await GetAccessToken());

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productVM, await GetAccessToken());

            if (result is not null)
                return RedirectToAction(nameof(Index));
        }
        return View(productVM);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await _productService.FindProductById(id, await GetAccessToken());

        if(result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productService.DeleteProductById(id, await GetAccessToken());

        if (!result)
            return View("Error");

        return RedirectToAction("Index");
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }

}
