namespace BusinessObject.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
