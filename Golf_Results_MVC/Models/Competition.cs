using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Golf_Results_MVC.Models
{
    public class Competition
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Comp_Result> Comp_Result { get; set; }

    }
}