using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Golf_Results_MVC.Models
{
    public class Competition
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name"), StringLength(80, MinimumLength = 1, ErrorMessage = "Competition name cannot be longer than 50 characters or null.")]
        public string Name { get; set; }
        public virtual ICollection<Comp_Result> Comp_Result { get; set; } // This needed to display all their results
    }
}