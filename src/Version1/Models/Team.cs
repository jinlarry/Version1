using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;

namespace Version1.Models
{
    public class Team
    {
        [Key]
        public string TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        public ApplicationUser TeamLeader { get; set; }

        [Display(Name = "Team Leader")]
        public string TeamLeaderName { get; set; }

        public List<ApplicationUser> Members { get; set; }
    }
}
