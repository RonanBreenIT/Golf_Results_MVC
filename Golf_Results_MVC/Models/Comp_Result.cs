using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Golf_Results_MVC.Models
{
    public class Comp_Result
    {
        [Key]
        public int CompResultID { get; set; } // More than one results for each comp i.e. Comp 1 results for each year 2020, 2019 etc..

        [Required]
        public int CompetitionID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Dates")]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM}", ApplyFormatInEditMode = true)]
        public string FullDate
        {
            get
            { 
                StringBuilder sb = new StringBuilder("");
                int startdateday = StartDate.Day;
                int endDateday = EndDate.Day;
                string DatesMonth = StartDate.ToString("MMM");
                int DatesYear = StartDate.Year;
                sb.Append(startdateday + "-" + endDateday);
                sb.Append(" " + DatesMonth + " " + DatesYear);
                return sb.ToString(); ;
            }
        }

        [Required]
        public int GolferID { get; set; }

        [DisplayFormat(NullDisplayText = "MC")]
        public string Position { get; set; } //If null will make missed cut.

        [Range(2019, 2100, ErrorMessage = "Season can only be inputted from 2019 onwards")] // Can increase from 2100 in time
        public int Season { get; set; }

        [Display(Name = "Golfer Score")]
        [DisplayFormat(NullDisplayText = "DNF")]
        public string GolferScore { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Golfer Golfer { get; set; }

        // Not using as yet where Comp_Result holds a collection of Golfers but may revisit as would make sense for Comp_result create page. 
        //public virtual ICollection<Golfer> Golfers { get; set; } // wont initialise db if uncommented

        //public Comp_Results()
        //{
        //    Golfers = new List<Golfer>();
        //}
    }
}