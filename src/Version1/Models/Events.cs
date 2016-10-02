using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Events
    {
        public string event_ID { get; set; }
        [Display(Name = "Title")]
        public string event_name { get; set; }
        [Display(Name = "Activity Address")]
        public string event_address { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Activity Date")]
        public DateTime event_datetime { get; set; }
        [Display(Name = "Activity Profile")]
        public string event_profile { get; set; }
        public string event_picture { get; set; }
        public string teamid { get; set; }
    }
}
