using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class TeamMember
    {
        public string TeamId { get; set; }
        public Team Teams { get; set; }
        public string UserID { get; set; }
        public ApplicationUser Volunteers { get; set; }
    }
}
