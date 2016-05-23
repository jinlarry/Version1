using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Version1.ViewModels.Account
{
    public class RoleManageViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }
}