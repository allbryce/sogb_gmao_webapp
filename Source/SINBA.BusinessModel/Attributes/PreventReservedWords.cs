using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PostSharp.Constraints;

namespace Sinba.BusinessModel.Attributes
{
    public class PreventReservedWords : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string word = value.ToString().Trim();

                if(Strings.ReservedWords.Split(',').Any(w => w.Equals(word, System.StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult(EntityCommonResource.errorReservedWord);
                }
            }
            return ValidationResult.Success;
        }
    }
    [Protected]
    class AspectTest
    {

    }
}
