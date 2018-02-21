using System.Collections.Generic;
using ShoeProject.Domain.DTO;

namespace ShoeProject.Domain.Repository
{
    /// <summary>
    /// The shoe tracker interface
    /// </summary>
    public interface IShoeTrackerRepository
    {
        /// <summary>
        /// adds a shoe
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        int AddShoe(ShoeTrackerDto shoeTrackerDto);

        /// <summary>
        /// Gets all shoes
        /// </summary>
        /// <returns></returns>
        List<ShoeTrackerDto> GetAllShoes();

        /// <summary>
        /// Gets shoes by child name
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        List<ShoeTrackerDto> GetShoesByChildName(string childName);

        /// <summary>
        /// Gets all names
        /// </summary>
        /// <returns></returns>
        List<Names> GetNames();

        /// <summary>
        /// Gets all shoe types
        /// </summary>
        /// <returns></returns>
        List<ShoeTypes> GetAllShoeTypes();

        /// <summary>
        /// Deletes a shoe
        /// </summary>
        /// <param name="id"></param>
        int DeleteShoe(int id);

        /// <summary>
        /// gets shoe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ShoeTrackerDto> GetShoeById(int id);

        /// <summary>
        /// Add receipt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        int AddReceiptPath(int id, string path);

        /// <summary>
        ///  edit shoe
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        /// <returns></returns>
        int EditShoe(ShoeTrackerDto shoeTrackerDto);

        /// <summary>
        /// path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<string> GetReceiptPath(int id);

        /// <summary>
        /// get monthly budget
        /// </summary>
        /// <returns></returns>
        MonthlyBudget GetMonthlyBudget();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int InsertMonthlyBudget(string category, int amount);

    }
}