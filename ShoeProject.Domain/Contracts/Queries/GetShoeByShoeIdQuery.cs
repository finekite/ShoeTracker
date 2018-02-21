

using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Queries
{
    /// <summary>
    /// The GetShoeByShoeIdQuery
    /// </summary>
    public class GetShoeByShoeIdQuery : IQuery
    {
        /// <summary>
        /// the shoe id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// construcot
        /// </summary>
        /// <param name="id"></param>
        public GetShoeByShoeIdQuery(int id)
        {
            this.Id = id;
        }

    }
}
