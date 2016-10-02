using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Version1.ViewModels.Home;
using Version1.Models;

namespace Version1.Controllers
{
    public class HomeController : Controller
    {  //密码：e3e3``  

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if(filterContext != null)
        //    {
        //        var actionName = filterContext.ActionDescriptor.Name;
        //        var username = filterContext.HttpContext.User.Identity.Name;
        //        var controllerName = filterContext.Controller.ToString();

        //        if(username != "")
        //        {
        //            //1.retrieving the role of the user from DB
        //            //2.retrieving the role related to the ACTION from DB
        //            //3.contrast the two roles,find if they are consistent 
        //            filterContext.Result = filterContext.Result;
        //        }
        //        else
        //        {
        //            filterContext.Result = new ContentResult { Content = @"You don't have authority！" };

        //        }
        //    }

        //}
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(HomeIndexViewModel viewModel)
        {
            foreach(var e in _dbContext.Events)
            {
                viewModel.Events.Add(e);
            }

            return View(viewModel);
        }
        public IActionResult ZeroRubbish( )
        {      
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
