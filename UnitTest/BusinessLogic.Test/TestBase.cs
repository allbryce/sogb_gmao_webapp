using Sinba.BusinessModel.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace BusinessLogic.Test
{
    /// <summary>
    /// Classe de base pourfaciliter l'écriture des tests unitaires
    /// </summary>
    public abstract class TestBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ioc container.
        /// </summary>
        /// <value>
        /// The ioc container.
        /// </value>
        protected IUnityContainer IOCContainer
        {
            get;
            set;
        }


        /// <summary>
        /// Gets the unit of work from IOC Container.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public ISinbaUnitOfWork UnitOfWork
        {
            get
            {
                return IOCContainer.Resolve<ISinbaUnitOfWork>();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// Initialise le container d'injection
        /// </summary>
        protected TestBase()
        {
            this.IOCContainer = new UnityContainer().LoadConfiguration();

            IOCContainer.RegisterInstance<Sinba.BusinessModel.ServiceInterface.IDataConfigurationProvider>(new DataConfigurationProvider());
        }
        #endregion
    }
}