
using System.ComponentModel.DataAnnotations;

namespace ApiCatalog.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The name has to be maximum 20 letters")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The description has to be maximum {1} characters")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Has to be bettwen {1} and {2}")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
