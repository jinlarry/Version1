using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;

namespace Version1.ViewModels.Account
{
    public class VolunteerViewModel
    {
        public List<Version1.Models.Team> viewTeams { get; set; }
        public ApplicationUser viewVolunteers { get; set; }
        public List<TeamMember> viewTeam_mumbers { get; set; }
    }
}
