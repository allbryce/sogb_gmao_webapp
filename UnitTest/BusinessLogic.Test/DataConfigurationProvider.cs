namespace BusinessLogic.Test
{
    /// <summary>
    /// Configuration provider for data access.
    /// </summary>
    public class DataConfigurationProvider : Sinba.BusinessModel.ServiceInterface.IDataConfigurationProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString
        {
            get { return "SinbaContext"; }
        }
    }
}
