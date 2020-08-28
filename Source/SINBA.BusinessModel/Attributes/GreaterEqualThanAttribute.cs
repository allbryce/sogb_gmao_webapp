using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;

namespace Sinba.BusinessModel.Attributes
{
    public sealed class GreaterEqualThanAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string testedPropertyName;

        public GreaterEqualThanAttribute(string testedPropertyName)
        {
            this.testedPropertyName = testedPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo propertyTestedInfo = validationContext.ObjectType.GetProperty(testedPropertyName);

            if (propertyTestedInfo == null) return new ValidationResult(string.Format(Resources.Resources.Entity.EntityCommonResource.errorProprieteIntrouvable, testedPropertyName));

            object propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decValue) && decimal.TryParse(propertyTestedValue.ToString(), out decimal decTestedPropertyValue))
            {
                if (decValue >= decTestedPropertyValue)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(string.Format(Resources.Resources.Entity.EntityCommonResource.errorGreatherThan, validationContext.DisplayName));
            }
            else if (DateTime.TryParse(value.ToString(), out DateTime dateValue) && DateTime.TryParse(propertyTestedValue.ToString(), out DateTime dateTestedPropertyValue))
            {
                if (dateValue >= dateTestedPropertyValue)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(string.Format(Resources.Resources.Entity.EntityCommonResource.errorGreatherThan, validationContext.DisplayName));
            }

            return new ValidationResult(Resources.Resources.Entity.EntityCommonResource.errorFormatDesDonneesInvalides);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "greaterequalthan"
            };
            rule.ValidationParameters["testedpropertyname"] = testedPropertyName;
            yield return rule;
        }
    }
}
