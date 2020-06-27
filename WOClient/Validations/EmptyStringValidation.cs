using System.Globalization;
using System.Windows.Controls;

namespace WOClient.Validations
{
    public class EmptyStringValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "");

            if (!(value is string)) return new ValidationResult(false, "");

            if (value.Equals(string.Empty)) return new ValidationResult(false, "Field cannot be empty.");

            return ValidationResult.ValidResult;
        }
    }
}
