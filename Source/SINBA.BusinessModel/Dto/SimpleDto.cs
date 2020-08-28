using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Dto
{
    /// <summary>
    /// DTO simple pour transporter une donnée simple dans le DTO
    /// </summary>
    /// <typeparam name="T">Type de la donnée transportée</typeparam>
    public class SimpleDto<T> : SinbaDtoBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDto{T}"/> class.
        /// </summary>
        public SimpleDto() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDto{T}"/> class.
        /// </summary>
        /// <param name="value">The data.</param>
        public SimpleDto(T value) : this()
        {
            this.Value = value;
        }
        #endregion
    }
}
