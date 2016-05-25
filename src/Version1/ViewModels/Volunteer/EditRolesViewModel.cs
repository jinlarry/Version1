using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Version1.Models;
using System;

namespace Version1.ViewModels.Volunteer
{
    public class EditRolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }

        [Display(Name = "All Users")]
        public List<ApplicationUser> Users { get; set; }

        // For both get and post.
        // Store user as key, bool as key's value. True is in the role.
        public Dictionary<ApplicationUser, List<bool>> UserCheckValueDict { get; set; }

        // For post.
        public List<string> RoleNames { get; set; }
        public Dictionary<string, List<bool>> PostBackDict { get; set; }


    }
}
