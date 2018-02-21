using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Repository
{
    /// <summary>
    /// Mappers class
    /// </summary>
    public class Mappers
    {
        /// <summary>
        /// Maps shoes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public ShoeTrackerDto MapShoe(SqlReaderWrapper data, int rowNum)
        {
            return new ShoeTrackerDto()
            {
                Date = data.GetDate("Date"),
                Description = data.GetString("Description"),
                Size = data.GetInt("Size"),
                Price = data.GetDecimal("Price"),
                ShoeTypeId = data.GetInt("ShoeTypeId"),
                ChildName = data.GetString("ChildName"),
                Link = data.GetString("Link"),
                Id = data.GetInt("Id")
            };
        }

        /// <summary>
        /// Maps shoes and description
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public ShoeTrackerDto MapShoeAndDescription(SqlReaderWrapper data, int rowNum)
        {
            return new ShoeTrackerDto()
            {
                Date = data.GetDate("Date"),
                Size = data.GetInt("Size"),
                Price = data.GetDecimal("Price"),
                ShoeTypeId = data.GetInt("ShoeTypeId"),
                ChildName = data.GetString("ChildName"),
                Link = data.GetString("Link"),
                Description = data.GetString("Description")
            };
        }

        /// <summary>
        /// Maps names
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public Names MapNames(SqlReaderWrapper data, int rowNum)
        {
            return new Names()
            {
                Name = data.GetString("ChildName")
            };
        }

        /// <summary>
        /// Maps Shoes types
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public ShoeTypes MapShoeTypes(SqlReaderWrapper data, int rowNum)
        {
            return  new ShoeTypes()
            {
                Description = data.GetString("Description"),
                ShoeTypeId = data.GetInt("id")
            };
        }

        /// <summary>
        /// map path
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public string MapReceiptPath(SqlReaderWrapper data, int rowNum)
        {
             return data.GetString("ReceiptPath");
        }

        public MonthlyBudget MapMonthlyBudget(SqlReaderWrapper data, int rowNum)
        {
            return new MonthlyBudget
            {
                FoodDollarAmount = data.GetInt("Food"),
                GasDollarAmount = data.GetInt("Gas"),
                ShoppingDollarAmount = data.GetInt("Shopping")
            };
        }
    }
}
