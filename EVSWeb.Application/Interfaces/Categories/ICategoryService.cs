using EVSWeb.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Application.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId);
        Task<bool> IsNameDuplicatedAsync(string name);
        Task AddCategoryAsync(CreatedCategoryDto createdCategoryDto);
        Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(Guid categoryId);
    }
}
