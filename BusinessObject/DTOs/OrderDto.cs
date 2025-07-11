using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Member is required")]
        public int MemberId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }
    }
}
