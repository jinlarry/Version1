using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Version1.Models;

namespace Version1.ViewModels.Volunteer
{
    public class RoleManageViewModel
    {
        [Display(Name = "Roles: ")]
        public List<IdentityRole> Roles { get; set; }
        [Display(Name = "Users: ")]
        public List<ApplicationUser> Users { get; set; }
        public RoleManageViewModel()
        {
            Roles = new List<IdentityRole>();
        }
    }
}
