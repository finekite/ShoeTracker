using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using shoeTracker.Core;
using ShoeProject.Domain.Constants;
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;


namespace ShoeProject.Domain.Repository
{
    /// <summary>
    /// Shoe tracker repo class
    /// </summary>
    public class ShoeTrackerRepository : Mappers, IShoeTrackerRepository
    {
        /// <summary>
        /// Shoe tracker repository
        /// </summary>
        private ShoeTrackerDatabaseRepo shoeTrackerDatabase;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public ShoeTrackerRepository(ShoeTrackerDatabaseRepo databaseRepo)
        {
            shoeTrackerDatabase = databaseRepo;
        }

        /// <summary>
        /// Adds a shoe to the db
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        public int AddShoe(ShoeTrackerDto shoeTrackerDto)
        {
            ShoeTrackerSqlStatment statement = new ShoeTrackerSqlStatment(StoredProcedureConstants.AddShoeProductStoredProcedure);
            statement.AddParameter("ShoeTypeId", shoeTrackerDto.ShoeTypeId);
            statement.AddParameter("Date", shoeTrackerDto.Date);
            statement.AddParameter("Childname", shoeTrackerDto.ChildName);
            statement.AddParameter("Price", shoeTrackerDto.Price);
            statement.AddParameter("Size", shoeTrackerDto.Size);
            statement.AddParameter("Link", shoeTrackerDto.Link);
            return shoeTrackerDatabase.ExecuteStoredProced(statement);
        }

        /// <summary>
        /// Adds receipt path
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int AddReceiptPath(int id, string path)
        {
            ShoeTrackerSqlStatment statement = new ShoeTrackerSqlStatment(StoredProcedureConstants.AddReceiptPath);
            statement.AddParameter("Id", id);
            statement.AddParameter("path", path);
            return shoeTrackerDatabase.ExecuteStoredProced(statement);
        }

        /// <summary>
        /// Get all shoes
        /// </summary>
        /// <returns></returns>
        public List<ShoeTrackerDto> GetAllShoes()
        {
            ShoeTrackerSqlStatment statement = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetAllShoes);
            return shoeTrackerDatabase.ExecuteStoredProc(statement, MapShoe).ToList();
        }

        /// <summary>
        /// Get shoe by child name
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        public List<ShoeTrackerDto> GetShoesByChildName(string childName)
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetShoeByChildName);
            statment.AddParameter("ChildName", childName);
            return shoeTrackerDatabase.ExecuteStoredProc(statment, MapShoeAndDescription).ToList();
        }

        /// <summary>
        /// Gets all names
        /// </summary>
        /// <returns></returns>
        public List<Names> GetNames()
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetAllNames);
            return shoeTrackerDatabase.ExecuteStoredProc(statment, MapNames).ToList();
        }

        /// <summary>
        /// Gets All Shoe Types
        /// </summary>
        /// <returns></returns>
        public List<ShoeTypes> GetAllShoeTypes()
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetAllShoeTypes);
            return shoeTrackerDatabase.ExecuteStoredProc(statment, MapShoeTypes).ToList();
        }

        /// <summary>
        /// Deletes a shoe
        /// </summary>
        /// <param name="id"></param>
        public int DeleteShoe(int id)
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.DeleteShoe);
            statment.AddParameter("Id", id);
            return shoeTrackerDatabase.ExecuteStoredProced(statment);
        }

        /// <summary>
        /// Gets shoe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ShoeTrackerDto> GetShoeById(int id)
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetShoeById);
            statment.AddParameter("Id", id);
            List<ShoeTrackerDto> shoeTracker =  shoeTrackerDatabase.ExecuteStoredProc(statment, MapShoe).ToList();
            return shoeTracker;
        }

        /// <summary>
        /// edit shoe
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        /// <returns></returns>
        public int EditShoe(ShoeTrackerDto shoeTrackerDto)
        {
            ShoeTrackerSqlStatment statement = new ShoeTrackerSqlStatment(StoredProcedureConstants.EditShoe);
            statement.AddParameter("ShoeTypeId", shoeTrackerDto.ShoeTypeId);
            statement.AddParameter("Date", shoeTrackerDto.Date);
            statement.AddParameter("Childname", shoeTrackerDto.ChildName);
            statement.AddParameter("Price", shoeTrackerDto.Price);
            statement.AddParameter("Size", shoeTrackerDto.Size);
            statement.AddParameter("Link", shoeTrackerDto.Link);
            statement.AddParameter("Id", shoeTrackerDto.Id);
            return shoeTrackerDatabase.ExecuteStoredProced(statement);
        }

        /// <summary>
        /// get path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetReceiptPath(int id)
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetReceiptPath);
            statment.AddParameter("Id", id);
            List<string> path =  shoeTrackerDatabase.ExecuteStoredProc(statment, MapReceiptPath).ToList();
            return path;
        }

        /// <summary>
        /// gets the onthly budget
        /// </summary>
        /// <returns></returns>
        public MonthlyBudget GetMonthlyBudget()
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.GetMonthlyBudgetQuery);
            MonthlyBudget monthlyBudget = shoeTrackerDatabase.ExecuteStoredProc(statment, MapMonthlyBudget).FirstOrDefault();
            return monthlyBudget;    
        }

        /// <summary>
        /// insert budget
        /// </summary>
        /// <param name="food"></param>
        /// <param name="gas"></param>
        /// <param name="shopping"></param>
        /// <returns></returns>
        public int InsertMonthlyBudget(string category, int amount)
        {
            ShoeTrackerSqlStatment statment = new ShoeTrackerSqlStatment(StoredProcedureConstants.InsertMonthlyBudget);
            category =  category.ToLower();
            if (category == "food")
            {
                statment.AddParameter("food", amount);
            }
            else if (category == "gas")
            {
                statment.AddParameter("gas", amount);
            }
            else
            {
                statment.AddParameter("shopping", amount);
            }
            int scalar = shoeTrackerDatabase.ExecuteStoredProced(statment);
            return scalar;
        }
    }

}


