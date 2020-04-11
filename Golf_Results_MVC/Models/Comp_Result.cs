using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Golf_Results_MVC.Models
{
    public class Comp_Result
    {
        [Key]
        public int CompResultID { get; set; } // More than one results for each comp i.e. Comp 1 results for each year 2020, 2019 etc..

        [Required]
        public int CompetitionID { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-YYYY}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-YYYY}", ApplyFormatInEditMode = true)]
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

        [Required]
        public int GolferID { get; set; }

        [DisplayFormat(NullDisplayText = "MC")]
        public string Position { get; set; } //? allows it to be null. If null will make missed cut.

        [Range(2019, 2050, ErrorMessage = "Season can only be inputted from 2019 onwards")]
        public int Season { get; set; }

        [Display(Name = "Golfer Score")]
        [DisplayFormat(NullDisplayText = "DNF")]
        public string GolferScore { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Golfer Golfer { get; set; }

        //public virtual ICollection<Golfer> Golfers { get; set; } // wont initialise db if uncommented

        //public Comp_Results()
        //{
        //    Golfers = new List<Golfer>();
        //}
    }
}