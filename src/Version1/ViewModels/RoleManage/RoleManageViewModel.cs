using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Version1.Models;

namespace Version1.ViewModels.RoleManage
{
    public class RoleManageViewModel
    {
        [Display(Name = "Roles: ")]
        public List<IdentityRole> Roles { get; set; }

        [Display(Name = "Users: ")]
        public List<ApplicationUser> Users { get; set; }

        public List<string> NewRoleNames { get; set; }

        public List<string> RemovedRoleNames { get; set; }
    }
}
