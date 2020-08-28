using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Sinba.Resources.Classes
{
    /// <summary>
    /// Class to read Application Settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Strings the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A string value.</returns>
        public static string StringValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Doubles the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A double value.</returns>
        public static double DoubleValue(string key)
        {
            double value;
            double.TryParse(StringValue(key), out value);
            return value;
        }
    }
}
