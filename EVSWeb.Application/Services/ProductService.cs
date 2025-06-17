using AutoMapper;
using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Domain.Entities;
using EVSWeb.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public IMapper _mapper { get; }
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddProductAsync(CreatedProductDto createdProductDto)
        {
            var product = _mapper.Map<Product>(createdProductDto);
            product.CreatedBy = null; // #todo: GetConnectedUser
            product.CreatedAt = DateTime.Now;
            product.UpdatedBy = product.CreatedBy;
            product.UpdatedAt = DateTime.Now;

            await _productRepository.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) new KeyNotFoundException(ProductMessages.PRODUCT_NOTFOUND_BYID (productId));
            await _productRepository.DeleteProductAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var data = _mapper.Map<IEnumerable<ProductDto>>(products);
            return data;
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            var produto = await _productRepository.GetProductByIdAsync(productId);
            return produto == null ? null : _mapper.Map<ProductDto>(produto);
        }

        public Task<bool> IsNameDuplicatedAsync(string name)
        {
            return _productRepository.IsNameDuplicateAsync(name);
        }

        public async Task UpdateNameProductAsync(Guid productId, string name)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            product.Name = name;
            product.UpdatedBy = null; // #todo: GetConnectedUser
            product.UpdatedAt = DateTime.Now;
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product is null) new KeyNotFoundException(ProductMessages.PRODUCT_NOTFOUND);
            _mapper.Map(updateProductDto, product);
            product.UpdatedBy = null; // #todo: GetConnectedUser
            product.UpdatedAt = DateTime.Now;
            await _productRepository.UpdateProductAsync(product);
        }
    }
}
