using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessObject.Validators
{
    public class GmailAddressAttribute : ValidationAttribute
    {
        private static readonly EmailAddressAttribute _emailValidator = new();

        public GmailAddressAttribute()
        {
            ErrorMessage = "Email must be a valid Gmail address.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not string email)
                return false;

            if (!_emailValidator.IsValid(email))
                return false;

            email = email.Trim();
            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                return false;

            var localPart = email[..^10]; // remove '@gmail.com'
            if (localPart.Length < 6)
                return false;

            return Regex.IsMatch(localPart, @"^[A-Za-z0-9](\.?[A-Za-z0-9])*$");
        }
    }
}
