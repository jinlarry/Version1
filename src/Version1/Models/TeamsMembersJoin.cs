using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class TeamsMembersJoin
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string TeamId { get; set; }
        public Team Team { get; set; }
    }
}
