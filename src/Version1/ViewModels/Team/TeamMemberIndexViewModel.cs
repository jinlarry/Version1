using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;
namespace Version1.ViewModels.Team
{
    public class TeamMemberIndexViewModel
    {
        public List<Version1.Models.Team> Teams { get; set; }

        public List<TeamMember> TeamMembers { get; set; }
    }
}
