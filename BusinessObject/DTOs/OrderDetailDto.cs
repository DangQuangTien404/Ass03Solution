using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        // Discount is entered as a percentage (0-100)
        [Range(0, 100)]
        public float Discount { get; set; }

        public decimal Total => UnitPrice * Quantity * (1 - (decimal)Discount / 100);
    }
}
