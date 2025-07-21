using EVSWeb.Application.DTOs;
using EVSWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Application.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<bool> IsNameDuplicatedAsync(string name);
        Task<Guid> AddProductAsync(CreatedProductDto createdProductDto);
        Task UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto);
        Task DeleteProductAsync(Guid productId);
        Task UpdateNameProductAsync(Guid productId, string name);
    }
}
