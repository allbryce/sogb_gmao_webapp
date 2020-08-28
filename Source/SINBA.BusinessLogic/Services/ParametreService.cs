using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Sinba.BusinessLogic.Services
{
    /// <summary>
    /// Implémentation du service Parametre
    /// </summary>
    [ServiceLog]
    [SinbaServiceExceptionHandler]
    public class ParametreService : SinbaServiceBase, IParametreService
    {
        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametreService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ParametreService(ISinbaUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        #endregion

        #region Methods
        #endregion
    }
}
