using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WOClient.Validations
{
    public class LengthValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)  return new ValidationResult(false, "");

            if (!(value is string)) return new ValidationResult(false, "");

            if (value.Equals(string.Empty)) return new ValidationResult(false, "");

            if (value.ToString().Length > Min && value.ToString().Length < Max) return ValidationResult.ValidResult;

            return new ValidationResult(false, "Illegal length for user name.");
        }
    }
}
