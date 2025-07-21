using AutoMapper;
using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Categories;
using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Domain.Entities;
using EVSWeb.Domain.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _repoCategory;
        public IMapper _mapper { get; }
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._repo = productRepository;
            this._repoCategory = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddProductAsync(CreatedProductDto createdProductDto)
        {
            var product = new Product
            {
                Code = createdProductDto.Code,
                Name = createdProductDto.Name,
                Description = createdProductDto.Description,
                Weight = createdProductDto.Weight,
                SellPoints = createdProductDto.SellPoints,
                Qtde = createdProductDto.Qtde,
                Coast = createdProductDto.Coast,
                Price = createdProductDto.Price,
                IsAtive = createdProductDto.IsAtive
            };


            product.Category = await _repoCategory.GetCategoryByIdAsync(createdProductDto.CategoryId);
            if (product.Category == null)
                throw new KeyNotFoundException("A chave informada não corresponde a uma Categoria válida", new Exception($"O Identificador {createdProductDto.CategoryId} não existe em Categories"));

            product.CreatedBy = null; // #todo: GetConnectedUser
            product.CreatedAt = DateTime.Now;
            product.UpdatedBy = product.CreatedBy;
            product.UpdatedAt = DateTime.Now;

            await _repo.AddProductAsync(product);
            return product.Id;
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException(ProductMessages.PRODUCT_NOTFOUND_BYID(productId));
            await _repo.DeleteProductAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllProductsAsync();
            var data = _mapper.Map<IEnumerable<ProductDto>>(products);
            return data;
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            var produto = await _repo.GetProductByIdAsync(productId);
            return produto == null ? null : _mapper.Map<ProductDto>(produto);
        }

        public Task<bool> IsNameDuplicatedAsync(string name)
        {
            return _repo.IsNameDuplicateAsync(name);
        }

        public async Task UpdateNameProductAsync(Guid productId, string name)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            product.Name = name;
            product.UpdatedBy = null; // #todo: GetConnectedUser
            product.UpdatedAt = DateTime.Now;
            await _repo.UpdateProductAsync(product);
        }

        public async Task UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            if (product is null) throw new KeyNotFoundException(ProductMessages.PRODUCT_NOTFOUND);
            _mapper.Map(updateProductDto, product);
            product.UpdatedBy = null; // #todo: GetConnectedUser
            product.UpdatedAt = DateTime.Now;
            await _repo.UpdateProductAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            var data = await _repo.GetProductsByCategoryIdAsync(categoryId);
            if (data == null || !data.Any())
                return new List<ProductDto>();
            else
                return _mapper.Map<IEnumerable<ProductDto>>(data);
        }
    }
}