using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ShoeProject.Domain.DTO
{
    public class ShoeTrackerDto 
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Shoe type
        /// </summary>
        public int ShoeTypeId { get; set; }

        /// <summary>
        /// Date of purchase
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Childs name
        /// </summary>
        public string ChildName { get; set; }

        /// <summary>
        /// Shoe price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Shoe size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Link to shoe
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Shoe type description
        /// </summary>
        public string Description { get; set; }


        public HttpPostedFileBase File { get; set; }
    }
}
