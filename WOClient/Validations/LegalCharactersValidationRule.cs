using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WOClient.Validations
{
    public class LegalCharactersValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "");

            if (!(value is string)) return new ValidationResult(false, "");

            if (value.Equals(string.Empty)) return new ValidationResult(false, "");

            var regex = new Regex("^[a-zA-Z0-9]+$");

            if (regex.IsMatch(value.ToString())) return ValidationResult.ValidResult;

            return new ValidationResult(false, "User name must contain only English characters and numbers.");
        }
    }
}
