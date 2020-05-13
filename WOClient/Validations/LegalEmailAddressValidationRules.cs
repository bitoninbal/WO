using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WOClient.Validations
{
    class LegalEmailAddressValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "");

            if (!(value is string)) return new ValidationResult(false, "");

            if (value.Equals(string.Empty)) return new ValidationResult(false, "");

            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (regex.IsMatch(value.ToString())) return ValidationResult.ValidResult;

            return new ValidationResult(false, "Illegal mail address.");
        }
    }
}
