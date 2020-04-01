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

        public int? Position { get; set; } //? allows it to be null. If null will make missed cut

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Year { get; set; }

        public int? GolferScore { get; set; }

        public bool Major { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Golfer Golfer { get; set; }

        //public virtual ICollection<Golfer> Golfers { get; set; }

        //public Comp_Results()
        //{
        //    Golfers = new List<Golfer>();
        //}
    }
}