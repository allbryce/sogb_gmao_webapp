using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Enums
{

    public struct Cultures
    {
        public const string Palmier = "P";
        public const string Hevea = "H";
    }

    public struct TypePersonne
    {
        public const string PersonnePhysique = "P";
        public const string PersonneMorale = "M";
    }

    public struct FormatFichier
    {
        public const string XLSX = "xlsx";
        public const string XLS = "xls";
    }

    public struct TypeLogImport
    {
        public const string CentreDeCout = "CentreDeCout";
        public const string Article = "Article";
        public const string Destination = "Destination";
        public const string Rubrique = "Rubrique";
        public const string CentreDeCoutSite = "CentreDeCoutSite";
    }

    public enum TypeNormePeriode :short
    {
        Regie = 28001,
        Soustraitance = 28002,
        Interimat = 28003
    }

}
