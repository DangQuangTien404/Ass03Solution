using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Validators
{
    public class NotPastDateAttribute : ValidationAttribute
    {
        public NotPastDateAttribute()
        {
            ErrorMessage = "Date cannot be in the past.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // optional dates allowed
            if (value is DateTime dt)
            {
                return dt.Date >= DateTime.Today;
            }
            return false;
        }
    }
}
