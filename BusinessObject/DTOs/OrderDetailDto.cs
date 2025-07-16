using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class OrderDetailDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        // Discount is entered as a percentage (0-100)
        [Range(0, 100)]
        public float Discount { get; set; }

        public decimal Total => UnitPrice * Quantity * (1 - (decimal)Discount / 100);
    }
}
