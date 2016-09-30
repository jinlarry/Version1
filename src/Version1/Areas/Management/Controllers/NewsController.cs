using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Version1.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using Version1.Services;
using Microsoft.Extensions.Options;
namespace Version1.Controllers
{
    [Area("Management")]
    public class NewsController : Controller
    {
        string ImageGarbage = "";
        string username;
        private IHostingEnvironment hostingEnv;
        private readonly ApplicationDbContext _context;
        private AppParameterSettings _settings;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            username = filterContext.HttpContext.User.Identity.Name;

        }

        public NewsController(IHostingEnvironment env, ApplicationDbContext context, IOptions<AppParameterSettings> settings)
        {
            this.hostingEnv = env;
            _settings = settings.Value;
            _context = context;
        }

        //News censor principle:Current user's role must match the value of AppParameterSettings": { "CensorUserRole"} in Appsetting.json 
        public async Task<IActionResult> CensorIndex(string id)
        {
            if (username == null)
            {
                return Content("Please log in and then access.");
            }
            else
            {
                var AuthorID = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                string pp="";  
                foreach (var item in _settings.CensorUserRole )
                {
                    if (item.ToString()!=null)
                    {
                        pp = pp +"'"+ item.ToString()+"'"+",";
                    }                      
                }
                if (pp!=null)
                {
                    pp = pp.Substring(0,pp.Length-1);
                }
                if (pp==null)
                {
                    return Content("System administrator hasn't set the censor role yet.Please contact to the admistrator.");
                }
                else
                {
                    var Sensorquery = _context.Users.FromSql("SELECT * FROM AspNetUsers where id='" + AuthorID + "' and id in(select a.UserId from AspNetUserRoles a,AspNetRoles b where a.RoleId=b.Id and b.NormalizedName in (" + pp + ")) ").ToList();
                    if (Sensorquery.Count > 0)
                    {
                        //It manifests that current user's role is enable to censor news
                        var newsview = new News_CensorIndexView();
                        newsview.CensoredNews = _context.News.Where(m => m.SensorID != null).OrderByDescending(p=>p.CensorTime).ToList();
                        newsview.UnCensoredNews = _context.News.Where(m => m.SensorID == null).OrderByDescending(p => p.CreateTime).ToList();
                        newsview.WhichTabActive = id;
                        return View(newsview);
                    }
                    else
                    {
                        return Content("Your user level is not enable to censor news.");
                    }
                }
                
             
            }
        }
        public async Task<IActionResult> Censor(string id)
        {
            if (username == null)
            {
                return Content("Please log in and then censor news.");
            }
            else
            {
                var ApproverID = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                _context.News.Single(p => p.ID == id).SensorID = ApproverID;
                _context.News.Single(p => p.ID == id).CensorTime = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("CensorIndex");
            }
           
        }
        public async Task<IActionResult> UndoCensor(string id)
        {
            if (username == null)
            {
                return Content("Please log in and then censor news.");
            }
            else
            {              
                _context.News.Single(p => p.ID == id).SensorID = null;
                _context.News.Single(p => p.ID == id).CensorTime = DateTime.Parse("0001-01-01 00:00:00.0000000");
                _context.SaveChanges();
                return RedirectToAction("CensorIndex");
            }

        }
        // GET: News
        public async Task<IActionResult> Index()
        {
            if (username == null)
            {
                return Content("Please log in and then access.");
            }
            else
            {
                var AuthorID = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                // return View(_context.News.Where(p => p.AuthorID == AuthorID).OrderByDescending(p=>p.CreateTime).ToList());
                List<News> pp = _context.News.OrderByDescending(p => p.CreateTime).ToList();
                return View(pp);
            }
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _context.News.Single(m => m.ID == id);
            var model_News = new Viewmodel_News();
            model_News.news = news;
            if (news.SensorID!=null && news.SensorID!="")
            {
                model_News.SensorName = _context.Users.Single(m => m.Id == news.SensorID).FirstName + "." + _context.Users.Single(m => m.Id == news.SensorID).LastName;
            }
            
            if (news == null)
            {
                return NotFound();
            }

            return View(model_News);
        }
        [HttpGet]
        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["NewsType"] = new SelectList(_context.News.Select(p => new { p.NewsType }), "NewsType", "NewsType");
            ViewData["CreateUser"] = username;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
               
            }
            if (disposing)
            {
                //if (this.configManager != null)
                //{
                //    this.configManager.Dispose();
                //    this.configManager = null;
            }
        }

        //    base.Dispose(disposing);
        //}
        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsTitle,NewsContent,NewsType,Selected,NewsImage")] News News)
        {
            var CreateAnews = new News();
            if (ModelState.IsValid)
            {
                var g = Guid.NewGuid();
                CreateAnews.ID = g.ToString();
                CreateAnews.CreateTime = DateTime.Now;
                //just for testing that using different methods to get post data 
                CreateAnews.NewsTitle = HttpContext.Request.Form["NewsTitle"];
                CreateAnews.NewsContent = HttpContext.Request.Form["NewsContent"].ToString();
                CreateAnews.NewsType = HttpContext.Request.Form["NewsType"].ToString();
                CreateAnews.Selected = News.Selected;
                // CreateAnews.NewsImage = News.NewsImage;

               
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    Guid ggg; ggg = Guid.NewGuid();
                    var file = Request.Form.Files[i];
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    if (filename!="")
                    {
                        filename = ggg.ToString() + filename.Substring(filename.Length - 4, 4);
                        CreateAnews.NewsImage = filename;
                        filename = hostingEnv.WebRootPath + $@"\images\News\{filename}";
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }

                }
                if (username != null)
                {
                    CreateAnews.AuthorID = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                }
                if (_settings.NewsAutoCensor)
                {
                    CreateAnews.CensorTime = CreateAnews.CreateTime;
                    CreateAnews.SensorID = CreateAnews.AuthorID;
                }
                _context.Add(CreateAnews);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(CreateAnews);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.SingleOrDefaultAsync(m => m.ID == id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CreateUser"] = username;
            ViewData["NewsType"] = new SelectList(_context.News.Select(p => new { p.NewsType }), "NewsType", "NewsType");
            //var Censorquery= (from a in _context.Users
            //                  join b in ( from c in _context.UserRoles join d in _context.Roles  on c.RoleId equals d.Id
            //                              where d.NormalizedName== "SYSTEM ADMINISTRATOR"
            //                              select new { RoleId = c.RoleId})
            //                  on a.Id equals b.RoleId
            //                  select new { Id = a.Id, UserName = a.UserName }).ToList();


            //------------------preparing censor person select list--------------------
            //var Censorquery = _context.Users.FromSql("SELECT Id ,FirstName,LastName FROM AspNetUsers where id in(select a.UserId from AspNetUserRoles a,AspNetRoles b where a.RoleId=b.Id and b.NormalizedName='" + _settings.CensorUserRole + "') ").Select(a => new { a.Id, a.FirstName, a.LastName }).ToList();
            //var Censorlisty = new List<Censor_selectlist>();
            //if (Censorquery!=null && Censorquery.Count>0)
            //{
            //     foreach (var item in Censorquery)
            //     {
            //       var Censoritem = new Censor_selectlist();
            //       Censoritem.Id = item.Id;
            //       Censoritem.name = item.FirstName + " " + item.LastName;
            //       Censorlisty.Add(Censoritem);
            //     }
            //    ViewData["CensorList"] = new SelectList(Censorlisty, "Id", "name");
            //}


            return View(news);
        }


        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,AuthorID,CensorTime,CreateTime,NewsContent,NewsImage,NewsTitle,NewsType,Selected,SensorID")] News news)
        {
            if (id != news.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string oldimagepath = news.NewsImage;
                    Boolean IfNewimage = false;
                    Guid ggg; ggg = Guid.NewGuid();
                    var file = Request.Form.Files[0];
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    if (filename!="")
                    {
                        filename = ggg.ToString() + filename.Substring(filename.Length - 4, 4);
                        news.NewsImage = filename;
                        filename = hostingEnv.WebRootPath + $@"\images\News\{filename}";
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                            IfNewimage = true;
                        }
                    }
                   

                    _context.Update(news);
                    //delete old image
                    if (IfNewimage)
                    {
                        oldimagepath = hostingEnv.WebRootPath + $@"\images\News\{oldimagepath}";
                        if (System.IO.File.Exists(oldimagepath) )
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                        
                    }
                    await _context.SaveChangesAsync();

                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _context.News.Single(m => m.ID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var news = _context.News.Single(m => m.ID == id);
            var imagepath = news.NewsImage;
            _context.News.Remove(news);

            await _context.SaveChangesAsync();
            if (imagepath != "")
            {
                try
                {
                    imagepath = hostingEnv.WebRootPath + $@"\images\News\{imagepath}";
                    System.IO.File.Delete(imagepath);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        private bool NewsExists(string id)
        {
            return _context.News.Any(e => e.ID == id);
        }
    }
    public class Viewmodel_News
    {
        public News news { get; set; }
        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }
    }
    public class sensor_selectlist
    {
        public string Id { get; set; }
        public string name { get; set; }
    }
    public class News_CensorIndexView
    {
        public  List<News> CensoredNews { get; set; }
        public  List<News> UnCensoredNews { get; set; }
        public string  WhichTabActive { get; set; }
    }
}
 
