using AutoMapper;
using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Categories;
using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Domain.Entities;
using EVSWeb.Domain.Messages;

namespace EVSWeb.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        public async Task AddCategoryAsync(CreatedCategoryDto createdCategoryDto)
        {
            var category = _mapper.Map<Category>(createdCategoryDto);
            category.CreatedBy = null; // #todo: GetConnectedUser
            category.CreatedAt = DateTime.Now;
            category.UpdatedBy = category.CreatedBy;
            category.UpdatedAt = DateTime.Now;

            await _repo.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _repo.GetCategoryByIdAsync(categoryId);
            if (category == null) throw new KeyNotFoundException(CategoryMessages.CATEGORY_NOTFOUND_BYID(categoryId));
            await _repo.DeleteCategoryAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var dados = await _repo.GetAllCategoriesAsync();
            var data = _mapper.Map<IEnumerable<CategoryDto>>(dados);
            return data;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId)
        {
            var dado = await _repo.GetCategoryByIdAsync(categoryId);
            return dado == null ? null : _mapper.Map<CategoryDto>(dado);
        }

        public async Task<bool> IsNameDuplicatedAsync(string name)
        {
            return await _repo.IsNameDuplicateAsync(name);
        }

        public async Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _repo.GetCategoryByIdAsync(categoryId);
            if (category == null) throw new KeyNotFoundException(CategoryMessages.CATEGORY_NOTFOUND_BYID(categoryId));
            category = _mapper.Map(updateCategoryDto, category);
            category.UpdatedBy = null; // #todo: GetConnectedUser
            category.UpdatedAt = DateTime.Now;
            await _repo.UpdateCategoryAsync(category);
        }
    }
}
