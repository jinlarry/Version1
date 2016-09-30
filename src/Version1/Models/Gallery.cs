using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Gallery
    {
        [Key]
        [MaxLength(50)]
        public string PhotoID { get; set; }
        [Display(Name ="Album Name")]
        [MaxLength(150)]
        public string Album   { get; set; }
        [Display(Name = "Photo Title")]
        [MaxLength(450)]
        public string PhotoTitle { get; set; }
        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }
        [Display(Name = "Photo Size")]
        public float PhotoSize { get; set; }
        [Display(Name = "Appending Time")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Author User")]
        [MaxLength(150)]
        public string Author { get; set; }
        
        public ApplicationUser Authorinfo { get; set; }
    }
}

