using AutoMapper;
using EVSWeb.Application.DTOs;
using EVSWeb.Domain.Entities;

namespace EVSWeb.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Products
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreatedProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreatedCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap(); 
            #endregion
        }
    }
}
