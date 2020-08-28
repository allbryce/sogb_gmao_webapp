using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace Sinba.BusinessModel.Entity
{
    /// <summary>
    /// Class Fonction (Liste)
    /// </summary>
    public partial class Fonction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fonction"/> class.
        /// </summary>
        public Fonction()
        {
            FonctionActions = new List<FonctionAction>();
        }

        [NotMapped]
        public string CodeHidden { get; set; }

        [NotMapped]
        public bool IsUsed { get; set; }

        [Display(Name = ResourceNames.Entity.Code, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Remote(SinbaConstants.Actions.IsCodeUsed, SinbaConstants.Controllers.Fonction, ErrorMessageResourceName = ResourceNames.Error.ErrorCodeExists, ErrorMessageResourceType = typeof(EntityCommonResource), AdditionalFields = DbColumns.CodeHidden)]
        public string Code { get; set; }

        [Display(Name = ResourceNames.Entity.MenuPath, ResourceType = typeof(EntityColumnResource))]
        public string MenuPath { get; set; }

        [Display(Name = ResourceNames.Entity.SA, ResourceType = typeof(EntityColumnResource))]
        public Nullable<bool> SuperAdmin { get; set; }

        [Display(Name = ResourceNames.Entity.Log, ResourceType = typeof(EntityColumnResource))]
        public Nullable<bool> Log { get; set; }

        public virtual ICollection<FonctionAction> FonctionActions { get; set; }

        #region Token
        [NotMapped]
        private List<string> fonctionActionsToken;

        [NotMapped]
        [Display(Name = ResourceNames.Entity.Actions, ResourceType = typeof(EntityColumnResource))]
        public List<string> FonctionActionsToken
        {
            get
            {
                if (FonctionActions.Any())
                {
                    fonctionActionsToken = FonctionActions.Select(fa => fa.CodeAction).ToList();
                }
                return fonctionActionsToken;
            }
            set
            {
                fonctionActionsToken = value;
                if (fonctionActionsToken.Count > 0)
                {
                    FonctionActions = new List<FonctionAction>();
                    foreach (string codeAction in fonctionActionsToken)
                    {
                        FonctionActions.Add(new FonctionAction()
                        {
                            CodeFonction = this.Code,
                            CodeAction = codeAction
                        });
                    }
                }
            }
        }
        #endregion
    }
}
