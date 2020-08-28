using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Dto
{
    /// <summary>
    /// DTO pour transporter un booléen.
    /// </summary>
    /// <typeparam name="T">Type de la donnée transportée</typeparam>
    public class BoolDto : SimpleDto<bool>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolDto"/> class.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public BoolDto(bool value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolDto"/> class.
        /// </summary>
        public BoolDto()
        {
        }
        #endregion
    }
}
