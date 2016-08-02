using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Team
    {
        public string TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }
        public string TeamLeaderID { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        public string TeamDescription { get; set; }

        public string TeamImage { get; set; }
    }
}
