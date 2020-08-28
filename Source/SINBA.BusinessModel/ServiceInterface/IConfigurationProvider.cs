using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.ServiceInterface
{
    /// <summary>
    /// Configuration information provider for database access.
    /// </summary>
    public interface IDataConfigurationProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }
    }
}
