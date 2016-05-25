using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Team
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Team Leader")]
        public ApplicationUser TeamLeader { get; set; }

        public List<TeamsMembersJoin> TeamsMembersJoins { get; set; }

        //public List<Project> TeamProject { get; set; }
    }
}
