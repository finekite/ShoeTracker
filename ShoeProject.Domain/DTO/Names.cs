using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeTracker.Core;

namespace ShoeProject.Domain.DTO
{
    /// <summary>
    /// Names class
    /// </summary>
    public class Names : IQueryResult
    {
        /// <summary>
        /// Name property
        /// </summary>
        public string Name { get; set; }
    }
}
