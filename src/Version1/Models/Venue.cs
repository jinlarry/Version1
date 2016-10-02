using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        public string VenueName { get; set; }

        public string Location { get; set; }

        public double Price { get; set; }

        public int OrderForeignKey { get; set; }

        public List<BookingOrder> BookingOrders { get; set; }
    }
}
