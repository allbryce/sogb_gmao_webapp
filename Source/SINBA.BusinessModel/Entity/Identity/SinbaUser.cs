using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Entity
{
    public class SinbaUser : IdentityUser
    {
        #region Constructors
        public SinbaUser()
        {
            this.UserRights = new List<UserRight>();
            this.SiteUtilisateurs = new List<SiteUtilisateur>();
        }
        #endregion

        #region Custom Properties

        public long? IdProfil { get; set; }

        [Display(Name = ResourceNames.Entity.Nom, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Nom { get; set; }

        [Display(Name = ResourceNames.Entity.Prenom, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string Prenom { get; set; }

        [ForeignKey("IdProfil")]
        public virtual Profil Profil { get; set; }
        public virtual ICollection<UserRight> UserRights { get; set; }
        public virtual ICollection<SiteUtilisateur> SiteUtilisateurs { get; set; }

        [NotMapped]
        [Display(Name = ResourceNames.Entity.AdminSite, ResourceType = typeof(EntityColumnResource))]
        public bool AdminSite { get; set; }

        [NotMapped]
        [Display(Name = ResourceNames.Entity.Sites, ResourceType = typeof(EntityColumnResource))]
        public string ListeSite {
            get
            {
                var valeur = "";
                if(SiteUtilisateurs.Count>0)
                {
                    valeur = string.Join(" ; ", SiteUtilisateurs.Select(p => p.IdSite).ToArray());
                }
                return valeur;
            }
        }

        #endregion

        #region Methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<SinbaUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
    }
}
