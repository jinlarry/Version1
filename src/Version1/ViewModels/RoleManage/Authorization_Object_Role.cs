using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Version1.Models;

namespace Version1.ViewModels.RoleManage
{
    //Configuration of Authorization Role and Authorization Objects

    public class Authorization_Object_Role_ViewModel
    {
        public List<Authorization_Object_Role> AuthorizationDone { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<Authorization_Object> Authorization_Object { get; set; }

    }
}
