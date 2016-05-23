using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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


            return View("RoleManage");
        }

        //[HttpPost]
        //public IActionResult RoleManage()
        //{


        //    return View();
        //}

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
