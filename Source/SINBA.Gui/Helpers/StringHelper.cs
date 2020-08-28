using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Sinba.Gui.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Formats the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string FormatString(string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }
    }
}