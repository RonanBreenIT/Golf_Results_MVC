using System;
using System.ComponentModel.DataAnnotations;

namespace Golf_Results_MVC.Models
{
    public class Comp_Result
    {
        [Key]
        public int CompResultID { get; set; } // More than one results for each comp i.e. Comp 1 results for each year 2020, 2019 etc..

        [Required]
        public int CompetitionID { get; set; }

        [Required]
        public int GolferID { get; set; }

        [DisplayFormat(NullDisplayText = "MC")]
        public string Position { get; set; } //? allows it to be null. If null will make missed cut.

        public int Season { get; set; }

        [Display(Name = "Golfer Score")]
        [DisplayFormat(NullDisplayText = "DNF")]
        public string GolferScore { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Golfer Golfer { get; set; }

        //public virtual ICollection<Golfer> Golfers { get; set; }

        //public Comp_Results()
        //{
        //    Golfers = new List<Golfer>();
        //}
    }
}