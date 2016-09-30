using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;
namespace Version1.ViewModels.Team
{
    public class TeamMemberViewModel
    {
        public List<Models.Team> Teams { get; set; }

        public List<ApplicationUser> Volunteers { get; set; }

        public List<TeamMember> Members { get; set; }
    }
}
