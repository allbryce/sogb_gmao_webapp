using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Dto
{
   public class ListItemDto : SinbaDtoBase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public ListItemDto(int _id, string code, string libelle)
        {
            this.Id = _id;
            this.Code = code;
            this.Libelle = libelle;
        }

        public ListItemDto()
        {
        }
    }    
}
