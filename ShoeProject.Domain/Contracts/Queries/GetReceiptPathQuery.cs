
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Queries
{
    /// <summary>
    /// class
    /// </summary>
    public class GetReceiptPathQuery : IQuery
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// constr
        /// </summary>
        /// <param name="id"></param>
        public GetReceiptPathQuery(int id)
        {
            Id = id;
        }
    }
}
