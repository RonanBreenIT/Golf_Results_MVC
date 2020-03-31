using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Golf_Results_MVC.Models
{
    public class Golfer
    {
        public int ID { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Golfer Name")]
        public string FullName
        {
            get
            {
                return Firstname + " " + Surname;
            }
        }

        public virtual ICollection<Comp_Result> Comp_Result { get; set; }
    }
}