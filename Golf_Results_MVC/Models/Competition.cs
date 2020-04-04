using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Golf_Results_MVC.Models
{
    public class Competition
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1, ErrorMessage = "Competition name cannot be longer than 50 characters or null.")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Dates")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        public string FullDate
        {
            get
            {
                return StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
            }
        }

        public virtual ICollection<Comp_Result> Comp_Result { get; set; }

    }
}