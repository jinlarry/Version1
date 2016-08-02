using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.ViewModels.Team
{
    public class TeamDetailViewModel
    {
        public string TeamId { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        public string LeaderId { get; set; }

        [Display(Name = "First Name")]
        public string LeaderFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LeaderLastName { get; set; }

        [Display(Name = "Contact")]
        public string LeaderContact { get; set; }

        [Display(Name = "Portrait")]
        public string LeaderPortrait { get; set; }
        public List<TeamMemberDetailViewModel> TeamMemberDetails { get; set; }
    }
}
