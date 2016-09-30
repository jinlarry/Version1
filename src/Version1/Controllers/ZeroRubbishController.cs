using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Version1.Models;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Version1.Controllers
{
    public class coord
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
    [Authorize]
    public class ZeroRubbishController : Controller
    {
        private readonly ApplicationDbContext _context; private IHostingEnvironment hostingEnv;
        // GET: /<controller>/
        public ZeroRubbishController(IHostingEnvironment env, ApplicationDbContext context)
        {
            this.hostingEnv = env;
            _context = context;
        }
         
        public IActionResult Index()
        {

            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string routecolour = ""; int kkk;
            if (_context.ZeroRabbishRoute.Where(a => a.CreateUserID == UserID).Count() > 0)
            {
                routecolour = _context.ZeroRabbishRoute.Where(a => a.CreateUserID == UserID).First().PathColor;
            }
            else
            {
                var random = new Random();
                routecolour = String.Format("#{0:X6}", random.Next(0x1000000));
                //random generate a colour Hex Code 
                kkk = _context.ZeroRabbishRoute.Where(p => p.PathColor == routecolour).Count();
                while (kkk > 0)
                {
                    routecolour = String.Format("#{0:X6}", random.Next(0x1000000));
                    kkk = _context.ZeroRabbishRoute.Where(p => p.PathColor == routecolour).Count();
                }
            }
            ViewData["routecolour"] = routecolour;
            ViewData["userid"] = UserID;
            var aaa = _context.ZeroRabbishRoute.Include(p => p.Points).Include(p => p.user);
            return View(aaa) ;
            
        }

        [HttpPost]
         
        public IActionResult RouteDelete(DeleteRouteData Route )
        {
             
            string[] del = Route.RouteToDelete.Split(',').ToArray();
            string OperationorID = del[0];         
            string routeid = del[1];
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (routeid!="")
            {
                if (_context.ZeroRabbishRoute.Where(a => a.RouteID == routeid).First().CreateUserID == UserID)
                {
                    var routepointsdelete = _context.ZeroRabbishRoutePoint.Where(a => a.RouteID == routeid);
                    _context.ZeroRabbishRoutePoint.RemoveRange(routepointsdelete);
                    var routedelete = _context.ZeroRabbishRoute.Where(a => a.RouteID == routeid);
                    _context.ZeroRabbishRoute.RemoveRange(routedelete);
                    _context.SaveChanges();
                    
                    //return View(new { success = true, responseText = "Succeed to delete the cleaning route! " });
                }
                //else { return View(new { success = false, responseText = "You are't allowed to delete another person's cleaning route!" }); }
            }
            //else {   }
           return View();
    }

        [HttpPost]
        public IActionResult Save(ReceiveData postdata)
        {
            if (postdata.points!=null)
            {          
                        string Rid = Guid.NewGuid().ToString(); string UserID;
                        ZeroRabbishRoute ROUTE = new ZeroRabbishRoute();
                        ROUTE.RouteID = Rid;
                        ROUTE.PathColor = postdata.PathColor;
                        ROUTE.RouteNote = postdata.RouteNote;
                        UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        ROUTE.CreateUserID = UserID;
                        string username = _context.ApplicationUser.Where(p => p.Id == UserID).Single().FirstName + "." + _context.ApplicationUser.Where(p => p.Id == UserID).Single().LastName;
                        ROUTE.Createdate = DateTime.Now;
                
                        for (int i = 0; i < postdata.points.Count(); i++)
                        {
                            ZeroRabbishRoutePoint POINT = new ZeroRabbishRoutePoint();
                            POINT.RouteID = Rid;
                            POINT.PointID = Guid.NewGuid().ToString();
                            POINT.lat = postdata.points[i].lat;
                            POINT.lon = postdata.points[i].lon;
                            if (postdata.points[i].Type== "RANGENODE"|| postdata.points[i].Type == "ROUTEPOINT")
                            {
                                  POINT.PostalAddress = i.ToString();
                            }
                            else
                            {
                                  POINT.PostalAddress = postdata.points[i].PostalAddress;
                            }
                    
                            POINT.Title = username;
                            POINT.Text = "<b>Voluneer:<b>" + username + " selected the route at " + ROUTE.Createdate.ToString() + ". ";
                            POINT.Type = postdata.points[i].Type;
                            _context.Add(POINT);
                        }
                        _context.Add(ROUTE);
                        _context.SaveChanges();
            }
            return RedirectToAction("Index");  
        }
    }
    public class DeleteRouteData
    { public string RouteToDelete { get; set; } }
    public class ReceiveData
    {
        public string PathColor { get; set; }
        public string RouteNote { get; set; }
        public pointtemp[] points { get; set; }


    }
    
    public class pointtemp
    {
        public string PostalAddress { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

    }
    public class ZeroRubbishCreateMapView
    {
        public ZeroRabbishRoute ZRoute { get; set; }
        public List<ZeroRabbishRoutePoint> RoutePoints { get; set; }
    }

}