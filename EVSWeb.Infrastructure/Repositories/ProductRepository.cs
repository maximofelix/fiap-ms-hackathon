using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Domain.Entities;
using EVSWeb.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EVSContext _evsContext;
        public ProductRepository(EVSContext evsContext)
        {
            this._evsContext = evsContext;
        }

        public async Task AddProductAsync(Product product)
        {
            _evsContext.Products.Add(product);
            await _evsContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            product.IsActive = false; // Soft delete
            _evsContext.Products.Update(product);
            await _evsContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _evsContext.Products.Where(o => o.IsActive).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _evsContext.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetProductByNameAsync(string name)
        {
            return await _evsContext.Products.Where(o => o.Name.Contains(name) && o.IsActive).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await _evsContext.Products.Where(o => o.Category.Id == categoryId && o.IsActive).ToListAsync();
        }

        public async Task<bool> IsNameDuplicateAsync(string name)
        {
            return await _evsContext.Products.AnyAsync(o => o.Name == name);
        }

        public async Task UpdateNameAsync(Guid productId, string name)
        {
            if (!await IsNameDuplicateAsync(name))
                throw new Exception(ProductMessages.PRODUCT_DUPLICATEDNAME);
            
            var product = await GetProductByIdAsync(productId);
            if (product == null) 
                throw new KeyNotFoundException(ProductMessages.PRODUCT_NOTFOUND);
            product.Name = name;
            _evsContext.Products.Update(product);
            await _evsContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _evsContext.Products.Update(product);
            await _evsContext.SaveChangesAsync();
        }
    }
}
