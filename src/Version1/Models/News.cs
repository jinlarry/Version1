using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Version1.Models
{
    public class News
    {
        [Key]
        public string ID { get; set; }
        [MaxLength(50)][Display(Name ="News Type")]
        public string NewsType {get;set;}
        [MaxLength(200)][Display(Name ="News Title")]
        public string NewsTitle { get; set; }
        [Display(Name = "Created Time")]
        public DateTime CreateTime { get; set; }
        [Display(Name ="News")]
        public string NewsContent { get; set; }
        [Display(Name = "Show in Mainpage")] 
        public Boolean Selected { get; set; }
        [Display(Name ="Author")][MaxLength(50)]
        public string AuthorID { get; set; }
        [Display(Name ="News Image")]
        public string NewsImage{get;set;}
        [Display(Name = "Sensor")] [MaxLength(50)]
        public string SensorID { get; set; }
        [Display(Name = "Censored Time")]
        public DateTime CensorTime { get; set; }

        
    }
}
