using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;

namespace ShoeTracker.Models
{
    public class HomePageModel : IQueryResult
    {
        /// <summary>
        /// Names
        /// </summary>
        public List<Names> Names { get; set; }

        /// <summary>
        /// Name drop down list
        /// </summary>
        [Display(Name = "Choose a name")]
        [Required(ErrorMessage = "Name is required")]
        public IEnumerable<SelectListItem> NameList { get; set; }

        /// <summary>
        /// Shoetype list
        /// </summary>
        public List<ShoeTypes> ShoeTypes { get; set; }

        /// <summary>
        /// Shoe type drop down list
        /// </summary>
        public IEnumerable<SelectListItem> ShoeTypeList { get; set; }

        /// <summary>
        /// Shoe tracker dto
        /// </summary>
        public ShoeTrackerDto ShoeTrackerDto { get; set; }

        /// <summary>
        /// List of shoes
        /// </summary>
        public List<ShoeTrackerDto> ShoeTrackerDtoList { get; set; }

        /// <summary>
        /// is delete
        /// </summary>
        public bool? isDelete { get; set; }

        /// <summary>
        /// is edit
        /// </summary>
        public bool isEdit { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// monthly budget
        /// </summary>
        public MonthlyBudget MonthlyBudget { get; set; }
    }
}