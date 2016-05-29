using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;

namespace Version1.ViewModels.Volunteer
{
    public class TeamManageViewModel
    {
        [Display(Name = "Team Names")]
        public List<string> TeamNames { get; set; }

        [Display(Name = "Team Leader")]
        public ApplicationUser TeamLeader { get; set; }

    }
}
