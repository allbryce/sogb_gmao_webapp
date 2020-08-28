using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.Resources.Resources.Entity
{
    public class EntityColumnResourceExtension : EntityColumnResource
    {
        public static string GetValue(string nom)
        {
            return ResourceManager.GetString(nom, Culture);
        }

    }
}
