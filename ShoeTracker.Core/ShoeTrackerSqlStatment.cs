
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace shoeTracker.Core
{
    public class ShoeTrackerSqlStatment
    {
        /// <summary>
        /// An sql string
        /// </summary>
        public string sql { get; set; }

        /// <summary>
        /// Dictionary
        /// </summary>
        private IDictionary<string, object> parameters = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sql"></param>
        public ShoeTrackerSqlStatment(string sql)
        {
            this.sql = sql;
        }

        /// <summary>
        /// To add a parameter
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void AddParameter(string parameterName, object value)
        {
            this.parameters.Add(parameterName, value);
        }

        /// <summary>Returns the parameters of the sql statement.</summary>
        /// <returns>the parameters.</returns>
        public IDictionary<string, object> GetParameters()
        {
            return this.parameters;
        }
    }
}
