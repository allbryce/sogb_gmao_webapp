using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Sinba.BusinessModel.Entity
{
    public partial class Profil
    {
        public Profil()
        {
            SinbaUsers = new List<SinbaUser>();
            ProfilRights = new List<ProfilRight>();
        }

        public long Id { get; set; }

        [Display(Name = ResourceNames.Entity.Nom, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Remote(SinbaConstants.Actions.IsNameUsed, SinbaConstants.Controllers.Profil, ErrorMessageResourceName = ResourceNames.Error.ErrorNomExists, ErrorMessageResourceType = typeof(EntityCommonResource), AdditionalFields = DbColumns.Id)]
        public string Nom { get; set; }

        public virtual ICollection<SinbaUser> SinbaUsers { get; set; }

        public virtual ICollection<ProfilRight> ProfilRights { get; set; }

        [NotMapped]
        public List<ProfilRightViewModel> GroupedRights
        {
            get
            {
                List<ProfilRightViewModel> lst = new List<ProfilRightViewModel>();
                Dictionary<string, List<string>> rights = new Dictionary<string, List<string>>();
                foreach (ProfilRight pr in ProfilRights)
                {
                    if (!rights.ContainsKey(pr.CodeFonction))
                    {
                        rights.Add(pr.CodeFonction, new List<string>());
                    }

                    if (!rights[pr.CodeFonction].Contains(pr.CodeAction))
                    {
                        rights[pr.CodeFonction].Add(pr.CodeAction);
                    }
                }
                foreach (KeyValuePair<string, List<string>> kvp in rights)
                {
                    lst.Add(new ProfilRightViewModel()
                    {
                        CodeFonction = kvp.Key,
                        Actions = string.Join(Strings.TokenTextSeparator, kvp.Value)
                    });
                }
                return lst;
            }
        }

        [NotMapped]
        public bool IsUsed
        {
            get
            {
                return ProfilRights.Count > 0 || SinbaUsers.Count > 0;
            }
        }
    }
}
