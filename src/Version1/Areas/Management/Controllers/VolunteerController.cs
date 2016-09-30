using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Version1.Models;
using Version1.ViewModels.RoleManage;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
namespace Version1.Controllers
{
    [Area("Management")]
  //  [Authorize(Roles = "manager")]
    public class VolunteerController : Controller
    {
        Version1.ViewModels.Account.VolunteerViewModel viewModel = new Version1.ViewModels.Account.VolunteerViewModel();
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment hostingEnv;
        public VolunteerController(IHostingEnvironment env, ApplicationDbContext context)
        {
            this.hostingEnv = env;
            _context = context;
        }

        // GET: Volunteer
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.OrderByDescending(p=>p.RegisterationDatetime).ToListAsync());
        }

        // GET: Volunteer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if(applicationUser == null)
            {
                return NotFound();
            }

            viewModel.viewTeams = _context.Teams.ToList();
            viewModel.viewVolunteers = applicationUser;
            viewModel.viewTeam_mumbers = _context.TeamMembers.ToList();
            return View(viewModel);
        }

        // GET: Volunteer/Create
        public IActionResult Create(string id)
        {
            if(id != null)
            {
                TempData["ppp"] = id;
            }
            return View();
        }

        // POST: Volunteer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,gender,AccessFailedCount,Age,ConcurrencyStamp,Email,EmailConfirmed,FirstName,LastName,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Portrait,RegisterationDatetime,UserState")] ApplicationUser applicationUser)
        {
            if(ModelState.IsValid)
            {
                
                //upload Newsletter image
                string ImageGUID = Guid.NewGuid().ToString();
                var file = Request.Form.Files[0];
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (filename!="")
                {
                    filename = ImageGUID + filename.Substring(filename.Length - 4, 4);
                    applicationUser.Portrait = filename;
                    filename = hostingEnv.WebRootPath + $@"\images\portrait\{filename}";
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Volunteer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if(applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: Volunteer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,gender,AccessFailedCount,Age,ConcurrencyStamp,Email,EmailConfirmed,FirstName,LastName,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,RegisterationDatetime,UserState")] ApplicationUser applicationUser)
        {
            if(id != applicationUser.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    string oldimagepath = applicationUser.Portrait;
                    Boolean IfNewimage = false;
                    //upload Newsletter image                   
                    if (Request.Form.Files.Count > 0)
                    {
                        string ImageGUID = Guid.NewGuid().ToString();
                        var file = Request.Form.Files[0];
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        if (filename != "")
                        {
                            filename = ImageGUID + filename.Substring(filename.Length - 4, 4);
                            applicationUser.Portrait = filename;
                            filename = hostingEnv.WebRootPath + $@"\images\portrait\{filename}";
                            using (FileStream fs = System.IO.File.Create(filename))
                            {
                                file.CopyTo(fs);
                                fs.Flush(); IfNewimage = true;
                            }
                        }
                            
                    }
                    _context.Update(applicationUser);
                    //delete old image
                    if (IfNewimage)
                    {
                        oldimagepath = hostingEnv.WebRootPath + $@"\images\portrait\{oldimagepath}";
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!ApplicationUserExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        // GET: Volunteer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if(applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Volunteer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            string oldimagepath = applicationUser.Portrait;
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            //delete old image
            oldimagepath = hostingEnv.WebRootPath + $@"\images\portrait\{oldimagepath}";
            if (System.IO.File.Exists(oldimagepath))
            {
                try
                {
                    System.IO.File.Delete(oldimagepath);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
