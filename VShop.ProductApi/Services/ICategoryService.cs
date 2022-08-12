using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
    Task<CategoryDTO> GetCategorytById(int id);
    Task AddCategory(CategoryDTO category);
    Task UpdateCategory(CategoryDTO category);
    Task RemoveCategory(int id);
}
