using System.Globalization;
using System.Windows.Controls;

namespace WOClient.Validations
{
    public class ComboBoxValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "You must select an item.");

            return ValidationResult.ValidResult;
        }
    }
}
