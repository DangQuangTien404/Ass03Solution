using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class SalesReportDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}
