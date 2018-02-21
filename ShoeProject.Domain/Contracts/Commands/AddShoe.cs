using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Commands
{
    /// <summary>
    /// the add shoe command
    /// </summary>
    public class AddShoeCommand : ICommand
    {
        /// <summary>
        /// shoe tracker dto
        /// </summary>
        public ShoeTrackerDto ShoeTrackerDto { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        public AddShoeCommand(ShoeTrackerDto shoeTrackerDto)
        {
            ShoeTrackerDto = shoeTrackerDto;
        }
        
    }
}
