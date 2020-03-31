using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Golf_Results_MVC.Models
{
    public class Comp_Result 
    {
        [Key]
        public int CompResultID { get; set; } // More than one results for each comp i.e. Comp 1 results for each year 2020, 2019 etc..

        public int CompetitionID { get; set; }

        public int GolferID { get; set; }

        public int? Position { get; set; } //? allows it to be null. If null will make missed cut

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int? GolferScore { get; set; } 

        public virtual Competition Competition { get; set; }

        public virtual Golfer Golfer { get; set; }

        //public virtual ICollection<Golfer> Golfers { get; set; }

        //public Comp_Results()
        //{
        //    Golfers = new List<Golfer>();
        //}
    }
}