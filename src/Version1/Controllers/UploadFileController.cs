using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Version1.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Version1.Controllers
{

    public class UploadFileController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private readonly ApplicationDbContext _context;
        public UploadFileController(IHostingEnvironment env, ApplicationDbContext context)
        {
            this.hostingEnv = env;
            _context = context;
        }

        [HttpPost]
        public IActionResult UploadPortraitFileAjax(string id)
        {
            long size = 0;
            var OFile = "";
            var files = Request.Form.Files;
            Guid ggg;
            ggg = Guid.NewGuid();
            foreach(var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');

                // filename = "newfile"+filename.Substring(filename.Length - 4, 4);
                filename = ggg.ToString() + filename.Substring(filename.Length - 4, 4);
                OFile = filename;
                if(id == "Management/TeamManage/Create" || id == "Management/TeamManage/Edit")
                {
                    filename = hostingEnv.WebRootPath + $@"\images\Teams\{filename}";
                }
                else if(id == "Management/Events/Create" || id == "Management/Events/Edit")
                {
                    filename = hostingEnv.WebRootPath + $@"\images\Events\{filename}";
                }
                else
                {
                    filename = hostingEnv.WebRootPath + $@"\images\portrait\{filename}";
                }

                size += file.Length;
                using(FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();

                }
            }
            if(id == "CREATE")
            {
                return RedirectToRoute(new
                {
                    area = "Management",
                    controller = "Volunteer",
                    action = "Create",
                    id = OFile
                });
            }
            else if(id == "EDIT")
            {
                _context.ApplicationUser.Where(a => a.Id == TempData["volunteerid"].ToString()).First().Portrait = OFile;
                _context.SaveChanges();
                return RedirectToRoute(new
                {
                    area = "Management",
                    controller = "Volunteer",
                    action = "Details",
                    id = TempData["volunteerid"]
                });
            }
            else
            {
                if(id == "Management/Events/Create")
                {
                    TempData["photoaddress"] = OFile;
                    return RedirectToRoute(new
                    {
                        area = "Management",
                        controller = "Events",
                        action = "Create",
                    });
                }
                else if(id == "Management/Events/Edit")
                {
                    TempData["photoaddress"] = OFile;
                    _context.Events.Where(a => a.event_ID == TempData["eventID"].ToString()).First().event_picture = OFile;
                    _context.SaveChanges();
                    return RedirectToRoute(new
                    {
                        area = "Management",
                        controller = "Events",
                        action = "Edit",
                        id = TempData["eventID"]
                    });
                }
                else if(id == "Management/TeamManage/Create")
                {
                    TempData["photoaddress"] = OFile;
                    return RedirectToRoute(new
                    {
                        area = "Management",
                        controller = "TeamManage",
                        action = "Create",
                    });
                }
                else if(id == "Management/TeamManage/Edit")
                {
                    TempData["photoaddress"] = OFile;
                    _context.Teams.Where(a => a.TeamId == TempData["TeamID"].ToString()).First().TeamImage = OFile;
                    _context.SaveChanges();
                    return RedirectToRoute(new
                    {
                        area = "Management",
                        controller = "TeamManage",
                        action = "Details",
                        id = TempData["TeamID"]
                    });
                }

                return View();
            }

            //   string message = $"{files.Count} file(s) /{ size}bytes uploaded successfully!";
            // return Json(message);
        }
        // GET: /<controller>/
        public IActionResult Index(string id, string volunteerid, string eventID, string TeamID)
        {
            TempData["action"] = id;
            if(volunteerid != null)
            {
                TempData["volunteerid"] = volunteerid;
            }
            if(eventID != null)
            {
                TempData["eventID"] = eventID;
            }
            if(TeamID != null)
            {
                TempData["TeamID"] = TeamID;
            }
            return View();
        }
        public IActionResult UploadImage()
        {
            return View();
        }

    }
}

