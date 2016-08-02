using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Version1.Models;
using Version1.ViewModels.RoleManage;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Version1.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "manager")]
    public class Authorization_Object_RoleController : Controller
    {
        //private UserManager<ApplicationUser> _userManager;
        //private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        ViewModels.RoleManage.Authorization_Object_Role_ViewModel viewModel = new ViewModels.RoleManage.Authorization_Object_Role_ViewModel();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // return;
            if(ModelState.Count == 0)
            {
                var areaname = filterContext.ActionDescriptor.RouteConstraints.Where(i => i.RouteKey == "area").First().RouteValue.ToString();
                var controllerName = filterContext.ActionDescriptor.RouteConstraints.Where(i => i.RouteKey == "controller").First().RouteValue.ToString();
                var actionName = filterContext.ActionDescriptor.Name;
                // var userId = _userManager.GetUserAsync(HttpContext.User).Id.ToString();
                var userId = "";
                var FullControllerName = areaname + "/" + controllerName;
                var username = filterContext.HttpContext.User.Identity.Name;
                if(username == null)
                {
                    filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                    return;
                }
                string itemrole;
                List<string> aa = new List<string>();
                if(username == "")
                { filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." }; return; }
                else if(username != "")
                {
                    //1.retrieving the role of the user from DB
                    userId = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                    foreach(var item in _context.UserRoles.Where(u => u.UserId == userId).ToList())
                    {
                        aa.Add(item.RoleId.ToString());
                    }
                    if(aa == null)
                    {
                        filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                        return;
                    }
                    else if(aa.Count == 0)
                    {
                        filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                        return;
                    }
                    if(_context.Authorization_Object.Where(N => N.ObjectType.ToUpper() == "FUNCTION").Where(N => N.ActionName.ToUpper() == actionName.ToUpper()).Where(N => N.FullControllerName.ToUpper() == FullControllerName.ToUpper()).Count() > 0)
                    {
                        //2.retrieving the role related to the ACTION from DB
                        var Object_ID = _context.Authorization_Object.Where(N => N.ObjectType.ToUpper() == "FUNCTION").Where(N => N.ActionName.ToUpper() == actionName.ToUpper()).Where(N => N.FullControllerName.ToUpper() == FullControllerName.ToUpper()).FirstOrDefault().ID.ToString();
                        List<Authorization_Object_Role> bb = _context.Authorization_Object_Role.Where(p => p.Authorization_Object_ID.ToString() == Object_ID).ToList();
                        //3.contrast the two roles,find if they are consistent 
                        if(bb == null)
                        {
                            filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                            return;
                        }
                        else if(bb.Count == 0)
                        {
                            filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                            return;
                        }
                        else
                        {
                            foreach(var item in bb)
                            {
                                itemrole = item.RoleID.ToString();
                                if(aa.Contains(itemrole) == true)
                                {
                                    filterContext.Result = filterContext.Result;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                    return;
                }
            }
        }
        public Authorization_Object_RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authorization_Object_Role
        public IActionResult Index()
        {
            viewModel.AuthorizationDone = _context.Authorization_Object_Role.ToList();
            viewModel.Authorization_Object = _context.Authorization_Object.ToList();
            viewModel.Roles = _context.Roles.ToList();
            return View("Index", viewModel);
        }

        [HttpGet]
        public ActionResult Update()
        {

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Update(Authorization_Object_Role_ViewModel gg)
        {
            if(ModelState.IsValid)
            {

                var all = from c in _context.Authorization_Object_Role select c;
                if(gg.AuthorizationDone != null)
                {
                    foreach(var item in gg.AuthorizationDone)
                    {
                        _context.Authorization_Object_Role.Add(new Authorization_Object_Role { RoleID = item.RoleID, Authorization_Object_ID = item.Authorization_Object_ID });
                    }
                }

                _context.Authorization_Object_Role.RemoveRange(all);
                int num = _context.SaveChanges();
                TempData["Message"] = num.ToString() + " Change have finished!";
                //TempData.Keep("Message");
            }
            // return Content("<script language='javascript' type='text/javascript'>alert('Hello world!');</script>");
            return RedirectToAction("Index");

        }
    }
}
