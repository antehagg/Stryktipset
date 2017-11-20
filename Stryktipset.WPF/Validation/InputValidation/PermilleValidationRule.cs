using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stryktipset.WPF.Validation.InputValidation
{
    public class PermilleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null && string.IsNullOrEmpty(value.ToString()))
                    return new ValidationResult(false, "Field cannot be empty.");

                var intValue = Convert.ToInt32(value);
                if(intValue >= 1 && intValue <= 99)
                    return new ValidationResult(true, "");

                return new ValidationResult(false, "Has to be between 1 and 99");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }
}
