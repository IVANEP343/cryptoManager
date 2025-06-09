using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoManager.Validation
{
    /// <summary>
    /// Valida que un DateTime no sea posterior a UtcNow.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotInFutureAttribute : ValidationAttribute
    {
        public NotInFutureAttribute()
        {
            ErrorMessage = "Date cannot be in the future.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is DateTime dt && dt > DateTime.UtcNow)
            {
                return new ValidationResult(ErrorMessage!, new[] { context.MemberName! });
            }
            return ValidationResult.Success;
        }
    }
}
