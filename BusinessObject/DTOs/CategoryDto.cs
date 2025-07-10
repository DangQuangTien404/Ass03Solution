using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;
    }
}
