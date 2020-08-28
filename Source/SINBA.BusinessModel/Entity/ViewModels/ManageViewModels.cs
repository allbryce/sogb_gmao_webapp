using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sinba.BusinessModel.Entity
{
    public class IndexViewModel
    {
        [Display(Name = ResourceNames.Entity.Password, ResourceType = typeof(EntityColumnResource))]
        public bool HasPassword { get; set; }
        public bool HasLanguage { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class SetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.NewPassword, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.ConfirmPassword, ResourceType = typeof(EntityColumnResource))]
        [Compare(ResourceNames.Entity.NewPassword, ErrorMessageResourceName = ResourceNames.Error.ErrorPasswordConfirmation, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string ConfirmPassword { get; set; }
    }

    public class SetPasswordUserViewModel
    {
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.NewPassword, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.ConfirmPassword, ResourceType = typeof(EntityColumnResource))]
        [Compare(ResourceNames.Entity.NewPassword, ErrorMessageResourceName = ResourceNames.Error.ErrorPasswordConfirmation, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.OldPassword, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.NewPassword, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(SinbaConstants.NumericValues.Length.PasswordMax, ErrorMessageResourceName = ResourceNames.Error.ErrorFieldLength, ErrorMessageResourceType = typeof(EntityCommonResource), MinimumLength = SinbaConstants.NumericValues.Length.PasswordMin)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceNames.Entity.ConfirmPassword, ResourceType = typeof(EntityColumnResource))]
        [Compare(ResourceNames.Entity.NewPassword, ErrorMessageResourceName = ResourceNames.Error.ErrorPasswordConfirmation, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string ConfirmPassword { get; set; }
    }

    public class AccountInfoViewModel
    {
        [Display(Name = ResourceNames.Entity.Langue, ResourceType = typeof(EntityColumnResource))]
        public string CodeIso { get; set; }
    }
}