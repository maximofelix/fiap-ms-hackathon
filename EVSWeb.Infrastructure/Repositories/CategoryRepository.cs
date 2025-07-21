using EVSWeb.Application.Interfaces.Categories;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EVSContext _evsContext;
        public CategoryRepository(EVSContext evsContext)
        {
            this._evsContext = evsContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _evsContext.Categories.Add(category);
            await _evsContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            var id = category.Id;
            if (_evsContext.Categories.Any(o => o.Id == id))
                throw new Exception(CategoryMessages.CATEGORY_VIOLATIONFK_PRODUTO);
            category.IsActive = false; // Soft delete
            _evsContext.Categories.Update(category);
            await _evsContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _evsContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _evsContext.Categories.FindAsync(categoryId);
        }

        public async Task<bool> IsNameDuplicateAsync(string name)
        {
            return await _evsContext.Categories.AnyAsync(o => o.Name == name);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _evsContext.Categories.Update(category);
            await _evsContext.SaveChangesAsync();
        }
    }
}
