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
            CreateMap<Product, ProductDto>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<Product, CreatedProductDto>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<Product, UpdateProductDto>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            CreateMap<ProductDto, Product>(); 
            CreateMap<CreatedProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();


            CreateMap<Product, DeleteProductDto>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreatedCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap(); 
            CreateMap<Category, DeleteCategoryDto>().ReverseMap(); 
            #endregion
        }
    }
}
