using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Version1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get; set; }

        public List<TeamMember> TeamMembers { get; set; }

        public string Portrait { get; set; }
    }

}

