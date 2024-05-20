using ApiCatalog.Models;
using AutoMapper;
namespace ApiCatalog.DTOs.Mappings;

public class ProductDTOMappingProfile : Profile
{
    public ProductDTOMappingProfile() 
    { 
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Category,CategoryDTO>().ReverseMap();
    }
}
