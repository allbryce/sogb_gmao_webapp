using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Entity.ViewModels
{
    public class ComposantCaracteristiqueViewModel
    {
        public ComposantCaracteristiqueViewModel()
        {
            Caracteristiques = new HashSet<PossederCaracteristiques>();
        }
        public ComposantCaracteristiqueViewModel(ComposerMateriel composerMateriel)
        {
            Caracteristiques = new HashSet<PossederCaracteristiques>();

            MaterielId = composerMateriel.MaterielId;
            Quantite = composerMateriel.Quantite;
            Plafond = composerMateriel.Plafond;
            ComposantId = composerMateriel.ComposantId;
            foreach(var item in composerMateriel.PossederCaracteristiques)
            {
                Caracteristiques.Add(new PossederCaracteristiques() {
                    ComposantId = item.ComposantId,
                    MaterielId = item.MaterielId,
                    DateInsertion = item.DateInsertion,
                    UniteId = item.UniteId,
                    Valeur = item.Valeur,
                    CaracteristiqueComposantId = item.CaracteristiqueComposantId
                });
            }
        }
        public long MaterielId { get; set; }
        public int? Quantite { get; set; }
        public int? Plafond { get; set; }
        public DateTime DateInsertion { get; set; }
        public long ComposantId { get; set; }
        public ICollection<PossederCaracteristiques> Caracteristiques { get; set; }
        public string CaracteristiqueComposantString { get; set; }

        public ComposerMateriel ToComposerMateriel()
        {
            var composermateriel =  new ComposerMateriel()
            {
                MaterielId = this.MaterielId,
                ComposantId = this.ComposantId,
                Quantite = this.Quantite,
                Plafond = this.Plafond,
                DateInsertion = this.DateInsertion
            };
            composermateriel.PossederCaracteristiques = string.IsNullOrEmpty(CaracteristiqueComposantString) ?
            new List<PossederCaracteristiques>() : JsonConvert.DeserializeObject<PossederCaracteristiques[]>(CaracteristiqueComposantString).ToList();
            foreach (var item in composermateriel.PossederCaracteristiques)
            {
                item.ComposantId = this.ComposantId;
                item.MaterielId = this.MaterielId;
                item.DateInsertion = this.DateInsertion;
            }
            return composermateriel;
        }

    }
}
