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
        public string TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
