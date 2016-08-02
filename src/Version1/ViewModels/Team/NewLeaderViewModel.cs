using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;

namespace Version1.ViewModels.Team
{
    public class NewLeaderViewModel
    {
        public string TeamId { get; set; }

        public List<Leader> Leaders { get; set; }

        public string LeaderId { get; set; }
    }
}
