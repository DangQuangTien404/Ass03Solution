using System.ComponentModel.DataAnnotations;
using BusinessObject.Validators;

namespace BusinessObject.DTOs
{
    public class MemberDto
    {
        public int MemberId { get; set; }

        [Required, GmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
