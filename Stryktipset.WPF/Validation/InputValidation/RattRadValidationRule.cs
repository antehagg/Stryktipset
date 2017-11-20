using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stryktipset.WPF.Validation.InputValidation
{
    public class RattRadValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, "Value cannot be coverted to string.");

            if(strValue.ToLower() == "1" || strValue.ToLower() == "x" || strValue.ToLower() == "2")
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Value should be 1, X or 2");
        }
    }
}
