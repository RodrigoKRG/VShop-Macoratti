using VShopWeb.Models;

namespace VShopWeb.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProduct(string token);
    Task<ProductViewModel> FindProductById(int id, string token);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token);
    Task<bool> DeleteProductById(int id, string token);
}
