using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int? CategoryId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string Weight { get; set; } = string.Empty;

        [Required]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }
    }
}
