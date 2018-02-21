
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Commands
{
    /// <summary>
    /// Edit shoe query
    /// </summary>
    public class EditShoeCommand : ICommand
    {
        /// <summary>
        /// shoetracker dto
        /// </summary>
        public ShoeTrackerDto ShoeTrackerDto;

        /// <summary>
        /// const
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        public EditShoeCommand(ShoeTrackerDto shoeTrackerDto)
        {
            ShoeTrackerDto = shoeTrackerDto;
        }
    }
}
