using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Version1.Models;
using Version1.ViewModels.Volunteer;

namespace Version1.Controllers
{
    public class VolunteerController : Controller
    {
        #region ================ variables ================

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        #endregion

        // Constructor
        public VolunteerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /volunteer/RoleManage
        [HttpGet]
        public IActionResult RoleManage(RoleManageViewModel viewModel)
        {
            viewModel.Roles = _roleManager.Roles.ToList<IdentityRole>();

            return View("RoleManage", viewModel);
        }

        #region ================ Action: CreateRole ================

        [HttpGet]
        public IActionResult CreateRole()
        {
            CreateRoleViewModel viewModel = new CreateRoleViewModel();
            return View("CreateRole", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var isSuccess = await CreateRole(viewModel.RoleName);
                if(isSuccess)
                {
                    // If all good, return to AllRoles view.
                    return RedirectToAction("RoleManage");
                }
            }
            // Return to "CreateRole" if anything wrong.
            return View("CreateRole");
        }

        #endregion // CreateRole

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

                    if(isInRole)
                    {
                        checkVal.Add(true);
                    }
                    else
                    {
                        checkVal.Add(false);
                    }
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
            }

            return RedirectToAction("EditRoles");
        }

        #endregion // EditRoles

        #region ================ Action: DeleteRoles ================

        [HttpGet]
        //public IActionResult DeleteRoles()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult DeleteRoles()
        {
            return View();
        }

        #endregion // DeleteRoles

        [HttpGet]
        public IActionResult ProfileManage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TeamManage()
        {
            return View();
        }


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
