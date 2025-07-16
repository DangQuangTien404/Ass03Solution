using System.ComponentModel.DataAnnotations;
using BusinessObject.Validators;

namespace BusinessObject.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Member is required")]
        public int MemberId { get; set; }

        [Required]
        [NotPastDate]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [NotPastDate]
        public DateTime? RequiredDate { get; set; }

        [NotPastDate]
        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }
    }
}
