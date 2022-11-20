using System;
using System.Globalization;
using System.Windows.Controls;

namespace LabPictures.Domain
{
    public class MoreThenZeroRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if (age <= 0)
            {
                return new ValidationResult(false,
                  $"Please enter a value bigger then zero");
            }
            return ValidationResult.ValidResult;
        }
    }
}
