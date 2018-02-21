using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeProject.Domain.Contracts.Commands;
using ShoeProject.Domain.Contracts.Queries;
using ShoeProject.Domain.DTO;
using ShoeProject.Domain.Repository;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Handlers.CommandHandlers
{
    /// <summary>
    /// ShoeTrackerCommandHandler class
    /// </summary>
    public class ShoeTrackerCommandHandler : ICommandHandler<AddShoeCommand>,
                                             ICommandHandler<DeleteShoeCommand>,
                                             ICommandHandler<AddReceiptPathCommand>,
                                             ICommandHandler<EditShoeCommand>,
                                             ICommandHandler<AddMonthlyBudgetCommand>
    {
        /// <summary>
        /// the shoe trakcer repo
        /// </summary>
        private readonly IShoeTrackerRepository shoeTrackerRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="shoeTrackerRepository"></param>
        public ShoeTrackerCommandHandler(IShoeTrackerRepository shoeTrackerRepository)
        {
            this.shoeTrackerRepository = shoeTrackerRepository;
        }

        /// <summary>
        /// handler for adding a shoe
        /// </summary>
        /// <param name="command"></param>
        public int HandleCommand(AddShoeCommand command)
        {
            return shoeTrackerRepository.AddShoe(command.ShoeTrackerDto);
        }


        public int HandleCommand(AddReceiptPathCommand command)
        {
            return this.shoeTrackerRepository.AddReceiptPath(command.Id, command.Path);
        }

        public int HandleCommand(DeleteShoeCommand command)
        {
            return this.shoeTrackerRepository.DeleteShoe(command.Id);
        }

        public int HandleCommand(EditShoeCommand command)
        {
           return this.shoeTrackerRepository.EditShoe(command.ShoeTrackerDto);
        }

        public int HandleCommand(AddMonthlyBudgetCommand command)
        {
            return shoeTrackerRepository.InsertMonthlyBudget(command.Category, command.Amount);
        }
    }
}
