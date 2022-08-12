using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repository;

namespace VShop.ProductApi.Services;

public class CategoryService : ICategoryService
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;  
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntity =  await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }
    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesProducts();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<CategoryDTO> GetCategorytById(int id)
    {
        var categoriesEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoriesEntity);
    }

    public async Task AddCategory(CategoryDTO category)
    {
        var categoriesEntity = _mapper.Map<Category>(category);
        await _categoryRepository.Create(categoriesEntity);
        category.CategoryId = categoriesEntity.CategoryId;
    }

    public async Task UpdateCategory(CategoryDTO category)
    {
        var categoriesEntity = _mapper.Map<Category>(category);
        await _categoryRepository.Update(categoriesEntity);
    }

    public async Task RemoveCategory(int id)
    {
        var categoriesEntity = _categoryRepository.GetById(id).Result; 
        await _categoryRepository.Delete(categoriesEntity.CategoryId);
    }
}
