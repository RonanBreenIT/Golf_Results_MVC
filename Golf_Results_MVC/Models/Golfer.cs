using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Golf_Results_MVC.Models
{
    public class Golfer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name"), StringLength(80, MinimumLength = 1, ErrorMessage = "Firstname cannot be longer than 50 characters or null.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Surname"), StringLength(80, MinimumLength = 0, ErrorMessage = "Surname cannot be longer than 50 characters or null.")]
        public string Surname { get; set; }

        [Display(Name = "Golfer Name")]
        public string FullName
        {
            get
            {
                return Firstname + " " + Surname;
            }
        }

        public virtual ICollection<Comp_Result> Comp_Result { get; set; } // This needed to display all their results
    }
}