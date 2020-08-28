using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.BusinessModel.Dto
{
    /// <summary>
    /// Defines a dto that owns a list of T
    /// </summary>
    /// <typeparam name="T">the type of object contained by the List</typeparam>
    public class ListDto<T> : SimpleDto<IEnumerable<T>>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDto{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ListDto(IEnumerable<T> value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDto{T}"/> class.
        /// </summary>
        public ListDto()
        {
        }
        #endregion
    }
}
