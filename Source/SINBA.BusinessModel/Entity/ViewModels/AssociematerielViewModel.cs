using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Entity.ViewModels
{
    
    public class AssociematerielViewModel
    {
        public long? MaterielAssocieId { get; set; }
        public DateTime DateInstallation { get; set; }
        public long MaterielId { get; set; }
        public DateTime? DateRetrait { get; set; }
        public string LibelleMaterielAssocie { get; set;}
        public string LibelleMateriel { get; set; }
    }
}
