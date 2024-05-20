using ApiCatalog.Models;

namespace ApiCatalog.DTOs.Mappinggs
{
    public static class CategoryDTOMappingExtensions
    {
           // this methods will can be used like there was created inside "category" and "CategoryDTO" classes
     
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            if (category is null)
            {
                return null;
            }

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };
        }

        public static Category ToCategory(this CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                return null;
            }

            return new Category
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl
            };
        }

        public static IEnumerable<CategoryDTO> ToCategoryDTOList(this IEnumerable<Category> categories)
        {
    
            if (categories is null || !categories.Any())
            {
                return [];
            }

            return categories.Select(categoria => new CategoryDTO
            {
                CategoryId = categoria.CategoryId,
                Name = categoria.Name,
                ImageUrl = categoria.ImageUrl
            }).ToList();
        }
    }
}
