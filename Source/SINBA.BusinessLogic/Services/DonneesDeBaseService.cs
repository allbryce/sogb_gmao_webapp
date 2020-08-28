using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Transactions;
using System.Data.Entity;
using Sinba.BusinessModel.Enums;
using static Sinba.Resources.DatabaseConstants;
using Newtonsoft.Json;
using Dev.Tools.Common;
using Sinba.Resources.Resources.Entity;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sinba.BusinessLogic.Services
{
    /// <summary>
    /// Classe de base des services métier de l'application Sinba
    /// </summary>
    [ServiceLog]
    [SinbaServiceExceptionHandler]
    public class DonneesDeBaseService : SinbaServiceBase, IDonneesDeBaseService
    {


        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RightManagementService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public DonneesDeBaseService(ISinbaUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
        #endregion

        #region Methods


        #endregion

    }
}
