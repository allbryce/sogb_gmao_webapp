using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sinba.BusinessModel.Entity
{
    public class ForgotViewModel
    {
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = ResourceNames.Entity.UserName, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string UserName { get; set; }

        [Display(Name = ResourceNames.Entity.Password, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string Password { get; set; }

        [Display(Name = ResourceNames.Entity.RememberMe, ResourceType = typeof(EntityColumnResource))]
        public bool? RememberMe { get; set; }

        [Display(Name = ResourceNames.Entity.Site, ResourceType = typeof(EntityColumnResource))]
        //[Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string IdSite { get; set; }

        public string ReturnUrl { get; set; }
   }

    public class RegisterViewModel
    {
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.Password, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.ConfirmPassword, ResourceType = typeof(EntityColumnResource))]
        [Compare(ResourceNames.Entity.Password, ErrorMessageResourceName = ResourceNames.Error.ErrorPasswordConfirmation, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.Password, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.ConfirmPassword, ResourceType = typeof(EntityColumnResource))]
        [Compare(ResourceNames.Entity.Password, ErrorMessageResourceName = ResourceNames.Error.ErrorPasswordConfirmation, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Email { get; set; }
    }
}
