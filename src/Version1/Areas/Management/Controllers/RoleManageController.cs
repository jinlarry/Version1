﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Version1.Models;
using Version1.ViewModels.RoleManage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
 
namespace Version1.Controllers
{
    [Area("Management")]
  //  [Authorize(Roles = "manager")]
    public class RoleManageController : Controller
    {
        #region ================ variables ================

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.DisplayName;
            var controllerName = filterContext.Controller.ToString();
        }
        // Constructor
        public RoleManageController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: /Role/
        // Main page of role controller
        public IActionResult Index()
        {
            return View();
        }


        #region  ================ Action: RoleManage ================

        // GET: /volunteer/RoleManage
        // Create and delete roles.
        [HttpGet]
        public IActionResult RoleManage()
        {
            RoleManageViewModel viewModel = new RoleManageViewModel();
            viewModel.Roles = new List<IdentityRole>();
            viewModel.Roles = _roleManager.Roles.ToList<IdentityRole>();

            return View("RoleManage", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleManage(RoleManageViewModel viewModel)
        {
            IdentityRole role;

            if(ModelState.IsValid)
            {
                if(viewModel.NewRoleNames != null)
                {
                    // create new roles
                    foreach(var name in viewModel.NewRoleNames)
                    {
                        if(name != null)
                        {
                            await CreateRole(name);
                        }
                    }
                }

                if(viewModel.RemovedRoleNames != null)
                {
                    // remove roles
                    foreach(var name in viewModel.RemovedRoleNames)
                    {
                        role = await _roleManager.FindByNameAsync(name);
                        await _roleManager.DeleteAsync(role);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        #endregion


        #region ================ Action: EditRoles ================

        // Get: /Volunteer/EditRoles
        [HttpGet]
        public async Task<IActionResult> EditRoles()
        {
            EditRolesViewModel viewModel = new EditRolesViewModel();

            // Get all roles and users.
            viewModel.Roles = _roleManager.Roles.ToList();
            viewModel.Users = _userManager.Users.ToList();

            viewModel.UserCheckValueDict = new Dictionary<ApplicationUser, List<bool>>();

            foreach(var user in viewModel.Users)
            {
                List<bool> checkVal = new List<bool>();

                foreach(var role in viewModel.Roles)
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, role.Name);

                    checkVal.Add((isInRole == true) ? true : false);
                }
                viewModel.UserCheckValueDict.Add(user, checkVal);
            }
            return View("EditRoles", viewModel);
        }

        // Post: /Volunteer/EditRoles
        [HttpPost]
        public async Task<IActionResult> EditRoles(EditRolesViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                foreach(var item in viewModel.PostBackDict)
                {
                    var userId = item.Key;
                    var flagList = item.Value; // The list of boolean. Indicates that if user should in role or not.
                    var roleNames = viewModel.RoleNames;
                    var user = await _userManager.FindByIdAsync(userId); // Get user by its id.

                    if(user != null)
                    {
                        for(int i = 0; i < flagList.Count; i++)
                        {
                            var roleName = roleNames[i];
                            // If user is in role.
                            var isUserInRole = await _userManager.IsInRoleAsync(user, roleName);

                            if(flagList[i])
                            {
                                // If the flag is true and the user is already in the role, continue to next loop.
                                if(isUserInRole)
                                {
                                    continue;
                                }

                                // If the flag is true and the user is not in the role, add it to role.
                                await _userManager.AddToRoleAsync(user, roleName);

                            }
                            else
                            {
                                // If the flag is false and the user is already in the role, remove it form the role.
                                if(isUserInRole)
                                {
                                    await _userManager.RemoveFromRoleAsync(user, roleName);
                                }

                                // If the flag is false and the user is not in the role, continue to next loop.
                                continue;
                            }
                        }
                    }
                }
                return RedirectToAction("RoleManage");
            }
            return View();
        }

        #endregion // EditRoles


        #region ================ Action: ProfileManage ================

        [HttpGet]
        public IActionResult ProfileManage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProfileManage(ProfileManageViewModel viewModel)
        {
            return View();
        }
        #endregion


        #region ================ Methods ================

        // Call this method to assign role to volunteer.
        private async Task<bool> AssignRole(string userId, string roleName)
        {
            bool isSueecss = false;

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if(roleExist)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if(user != null)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    isSueecss = true;
                }
            }
            return isSueecss;
        }

        // Call this method to create role.
        private async Task<bool> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            IdentityResult result;

            if(!roleExist)
            {
                result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                return result.Succeeded;
            }
            else
            {
                return roleExist;
            }
        }

        #endregion // End of Methods region
    }
}
