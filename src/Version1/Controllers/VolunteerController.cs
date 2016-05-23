using System;
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



// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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

        #region ================ Actions ================

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
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
                    return RedirectToAction("AllRoles");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult RoleManage()
        {

            return View("RoleManage");
        }



        public IActionResult ProfileManage()
        {
            return View();
        }

        public IActionResult TeamManage()
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

            bool isExist;
            IdentityResult result;

            if(!roleExist)
            {
                result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                return result.Succeeded;
            }
            else
            {
                isExist = true;
                return isExist;
            }
        }

        #endregion // End of Methods region


        //[HttpGet]
        //public IActionResult AllRoles(AllRolesViewModel viewModel)
        //{
        //    viewModel.Roles = _roleManager.Roles.ToList<IdentityRole>();

        //    return View(viewModel);
        //}



    }
}
