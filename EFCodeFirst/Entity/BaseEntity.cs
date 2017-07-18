using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCodeFirst.Entity
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}
