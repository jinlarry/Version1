using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Version1.Models
{
    public class BookingOrder
    {
        [Key]
        public int OrderId { get; set; }

        public string DayName { get; set; }

        public string DayNumber { get; set; }

        public bool isPublicEvent { get; set; }

        public bool isProved { get; set; }

        public DateTime OrderDate { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public int VenueID { get; set; }

        public Venue Venue { get; set; }
    }
}
