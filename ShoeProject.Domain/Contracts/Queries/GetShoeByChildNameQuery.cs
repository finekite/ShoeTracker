using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Queries
{
    /// <summary>
    /// GetShoeByChildNameQuery class
    /// </summary>
    public class GetShoeByChildNameQuery : IQuery
    {
        /// <summary>
        /// Childs name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public GetShoeByChildNameQuery(string name)
        {
            Name = name;
        }
    }
}
