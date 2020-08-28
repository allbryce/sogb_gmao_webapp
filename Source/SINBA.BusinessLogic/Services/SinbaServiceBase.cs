using Dev.Tools.Architecture.Service;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using System;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;

namespace Sinba.BusinessLogic.Services
{
    /// <summary>
    /// Classe de base des services métier de l'application Sinba
    /// </summary>
    [ServiceLog]
    [SinbaServiceExceptionHandler]
    public abstract class SinbaServiceBase : ServiceBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public ISinbaUnitOfWork UnitOfWork { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaServiceBase"/> class.
        /// </summary>
        public SinbaServiceBase(ISinbaUnitOfWork unitOfWork)
            : base()
        {
            this.UnitOfWork = unitOfWork;
        }
        #endregion

        #region Handlers
        /// <summary>
        /// Handler method for all methods
        /// Manage the errors progression from Exception to Errors string in DTO Base
        /// </summary>
        /// <param name="ex">The exception caught by the Exception system.</param>
        /// <param name="returnedType">Returned Type of the method that has caught this exception</param>
        /// <returns>
        /// The object returned by the method
        /// </returns>
        public override object ExceptionHandlerMethod(Dev.Tools.Exceptions.CaughtException ex, Type returnedType)
        {
            return base.ExceptionHandlerMethod(ex, returnedType);
        }
        #endregion

        
    }
}
