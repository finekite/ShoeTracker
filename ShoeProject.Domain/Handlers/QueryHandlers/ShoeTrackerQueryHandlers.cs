using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeProject.Domain.Contracts.Queries;
using ShoeProject.Domain.DTO;
using ShoeProject.Domain.Repository;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Handlers.QueryHandlers
{
    /// <summary>
    /// The ShoeTrackerQueryHandlers class
    /// </summary>
    public class ShoeTrackerQueryHandlers : IQueryHandler<GetNamesQuery, List<Names>>,
                                            IQueryHandler<GetAllShoeTypesQuery, List<ShoeTypes>>,
                                            IQueryHandler<GetAllShoesQuery, List<ShoeTrackerDto>>,
                                            IQueryHandler<GetShoeByChildNameQuery, List<ShoeTrackerDto>>,
                                            IQueryHandler<GetShoeByShoeIdQuery, ShoeTrackerDto>,
                                            IQueryHandler<GetReceiptPathQuery, List<string>>,
                                            IQueryHandler<GetMonthlyBudgetQuery, MonthlyBudget>
    {
        /// <summary>
        /// The shoetracker repo
        /// </summary>
        private readonly IShoeTrackerRepository shoeTrackerRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoeTracker"></param>
        public ShoeTrackerQueryHandlers(IShoeTrackerRepository shoeTracker)
        {
            shoeTrackerRepository = shoeTracker;
        }

        /// <summary>
        /// the retrive method to get all names
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Names> Retrieve(GetNamesQuery query)
        {
            return shoeTrackerRepository.GetNames();
        }

        /// <summary>
        /// get all shoe types handler method
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ShoeTypes> Retrieve(GetAllShoeTypesQuery query)
        {
            return shoeTrackerRepository.GetAllShoeTypes();
        }

        /// <summary>
        /// Gets all the shoes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ShoeTrackerDto> Retrieve(GetAllShoesQuery query)
        {
            return shoeTrackerRepository.GetAllShoes();
        }

        /// <summary>
        /// Gets all shoes by childs name
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ShoeTrackerDto> Retrieve(GetShoeByChildNameQuery query)
        {
            return shoeTrackerRepository.GetShoesByChildName(query.Name);
        }

        /// <summary>
        /// gets the shoe by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ShoeTrackerDto Retrieve(GetShoeByShoeIdQuery query)
        {
            return shoeTrackerRepository.GetShoeById(query.Id).FirstOrDefault();
        }

        /// <summary>
        /// get path
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string> Retrieve(GetReceiptPathQuery query)
        {
            return shoeTrackerRepository.GetReceiptPath(query.Id);
        }

        public MonthlyBudget Retrieve(GetMonthlyBudgetQuery query)
        {
            return shoeTrackerRepository.GetMonthlyBudget();
        }
    }
}
