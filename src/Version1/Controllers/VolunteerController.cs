using System.Linq;
using Microsoft.AspNet.Mvc;
using Version1.ViewModels.Home;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;
using System.Collections.Generic;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Version1.Controllers
{
    public class VolunteerController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public VolunteerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult RoleManage()
        {
            //RoleManageViewModel viewModel = new RoleManageViewModel();

            //foreach (var role in _roleManager.Roles)
            //{
            //    viewModel.RoleNames.Add(role.Name);
            //}

            ////Assembly asm = Assembly.GetExecutingAssembly();

            //var assembly = typeof(Controller).GetTypeInfo().Assembly;


            ////var list = GetControllers();



            //foreach (var item in list)
            //{
            //    viewModel.ControllerNames.Add(nameof(item));
            //}

            return View("RoleManage");
        }

        [HttpPost]
        public IActionResult RoleManage(RoleManageViewModel viewModel)
        {


            return View();
        }

        public IActionResult ProfileManage()
        {
            return View();
        }

        public IActionResult TeamManage()
        {
            return View();
        }

        // Get all controllers
        //private IEnumerable<Type> GetControllers()
        //{
        //    return from t in typeof(Controller).Assembly.GetTypes()
        //           where typeof(Controller).IsAssignableFrom(t)
        //           where t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
        //           select t;
        //}

    }
}
