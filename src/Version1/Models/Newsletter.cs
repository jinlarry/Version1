using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Newsletter
    {
        [Key]
        public string NewsletterId { get; set; }
        [Display(Name ="Newsletter Name")]
        public string NewsletterName { get; set; }
        [Display(Name = "Publish Date")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        public string ImagePath { get; set; }
        [Display(Name = "Newsletter Detail")]
        public string Detail { get; set; }
    }
}
