using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.ViewModels.Team
{
    public class FronTeamViewModel
    {
        public List<Version1.Models.Team> Teams { get; set; }

        public FronTeamViewModel()
        {
            Teams = new List<Models.Team>();

            
        }
    }
}
