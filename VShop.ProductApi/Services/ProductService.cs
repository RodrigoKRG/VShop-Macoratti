using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repository;

namespace VShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task AddProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<Product>(product);
        await _productRepository.Create(productEntity);
        product.CategoryId = productEntity.CategoryId;
    }

    public async Task UpdateProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<Product>(product);
        await _productRepository.Update(productEntity);
    }

    public async Task RemoveProduct(int id)
    {
        var productEntity = _productRepository.GetById(id).Result;
        await _productRepository.Delete(productEntity.Id);
    }
}
