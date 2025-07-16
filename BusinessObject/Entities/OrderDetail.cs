using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        // Composite PK must be configured in your DbContext Fluent API
        public int OrderId   { get; set; }
        public int ProductId { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order  Order  { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
    }
}
