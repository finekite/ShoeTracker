using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using shoeTracker.Core;


namespace ShoeTracker.Core
{
    public class ShoeTrackerDatabaseRepo : IShoeTrackerDataRepository
    {
        /// <summary>
        /// A connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        private IDictionary<string, object> parameters = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        //[Inject]
        public ShoeTrackerDatabaseRepo(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Executes the stored procedure
        /// </summary>
        /// <param name="statement"></param>
        public int ExecuteStoredProced(ShoeTrackerSqlStatment statement)
        {
            int scope;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                this.PrepareSql(command, statement.sql, statement.GetParameters());
                scope = int.Parse(command.ExecuteScalar().ToString());
            }
            
            return scope;
        }

        /// <summary>
        /// Executes the stored procedure
        /// </summary>
        /// <param name="statement"></param>
        public void ExecuteStoredProcedWithoutScalar(ShoeTrackerSqlStatment statement)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                this.PrepareSql(command, statement.sql, statement.GetParameters());
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes with mapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statement"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProc<T>(ShoeTrackerSqlStatment statement, ShoeTrackerDatabaseRepo.MapRecord<T> mapper)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                this.PrepareSql(cmd, statement.sql, statement.GetParameters());
                SqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                    yield return mapper(new SqlReaderWrapper((IDataReader)reader), count++);
            }
        }

        /// <summary>
        /// Prepares sql statement
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public void PrepareSql(SqlCommand command, string sql, IDictionary<string, object> parameters)
        {
            command.CommandText = sql;
            command.Prepare();
            foreach (string key in parameters.Keys)
            {
                Object parameter = parameters[key];
                String parameterName = "@" + key;
                if (parameter == null)
                {
                    command.Parameters.AddWithValue(parameterName, (object)DBNull.Value);
                }
                else if (parameter is DateTime)
                {
                    command.Parameters.AddWithValue(parameterName,
                        (DateTime)parameter == DateTime.MinValue ? (object)DBNull.Value : parameter);
                }
                else if (parameter.GetType().IsGenericType &&
                         parameter.GetType().GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    DateTime? nullable = (DateTime?)parameter;
                    if (nullable.HasValue)
                    {
                        command.Parameters.AddWithValue(parameterName, nullable.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(parameterName, DBNull.Value);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue(parameterName, parameter);
                }
            }
        }

        /// <summary>
        /// Delegate used to map data from the database to an object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="data">the SqlReaderWrapper.</param>
        /// <param name="rownum">the row number.</param>
        /// <returns>the populated record.</returns>
        public delegate T MapRecord<T>(SqlReaderWrapper data, int rownum);
    }
}
