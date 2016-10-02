using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Version1.ViewModels.Venue
{
    public class BookingViewModel
    {
        public string DayName { get; set; }

        public string DayNumber { get; set; }

        public string Month { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool IsPublicEvent { get; set; }

        public bool IsProvied { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int PhoneNum { get; set; }

        public string Email { get; set; }
    }
}
