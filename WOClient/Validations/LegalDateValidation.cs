using System;
using System.Globalization;
using System.Windows.Controls;

namespace WOClient.Validations
{
    public class LegalDateValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Field cannot be empty.");

            if (!(value is DateTime)) return new ValidationResult(false, "Value must be a legal date.");

            var date = (DateTime)value;

            if (date.Date < DateTime.Now.Date) return new ValidationResult(false, "You must enter valid date.");

            return ValidationResult.ValidResult;
        }
    }
}
