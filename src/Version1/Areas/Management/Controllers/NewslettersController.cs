using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Version1.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Version1.Controllers
{
    [Area("Management")]
    public class NewslettersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment hostingEnv;
        public NewslettersController(IHostingEnvironment env, ApplicationDbContext context)
        {
            this.hostingEnv = env;
            _context = context;
        }

        // GET: Newsletters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newsletters.OrderByDescending(p=>p.PublishDate).ToListAsync());
        }

        // GET: Newsletters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.SingleOrDefaultAsync(m => m.NewsletterId == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // GET: Newsletters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newsletters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsletterId,Detail,ImagePath,NewsletterName,PublishDate")] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                var newid = Guid.NewGuid();
                newsletter.NewsletterId = newid.ToString();
                //upload Newsletter image
                string ImageGUID = Guid.NewGuid().ToString();
                var file = Request.Form.Files[0];
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (filename!="")
                {
                    filename = ImageGUID + filename.Substring(filename.Length - 4, 4);
                    newsletter.ImagePath = filename;
                    filename = hostingEnv.WebRootPath + $@"\images\Newsletters\{filename}";
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
               
                _context.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newsletter);
        }

        // GET: Newsletters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.SingleOrDefaultAsync(m => m.NewsletterId == id);
            if (newsletter == null)
            {
                return NotFound();
            }
            return View(newsletter);
        }

        // POST: Newsletters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NewsletterId,Detail,ImagePath,NewsletterName,PublishDate")] Newsletter newsletter)
        {
            if (id != newsletter.NewsletterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string oldimagepath = newsletter.ImagePath;
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
                            newsletter.ImagePath = filename;
                            filename = hostingEnv.WebRootPath + $@"\images\Newsletters\{filename}";
                            using (FileStream fs = System.IO.File.Create(filename))
                            {
                                file.CopyTo(fs);
                                fs.Flush(); IfNewimage = true;
                            }
                        }

                    }

                    _context.Update(newsletter);
                    //delete old image
                    if (IfNewimage)
                    {
                        oldimagepath = hostingEnv.WebRootPath + $@"\images\Newsletters\{oldimagepath}";
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterExists(newsletter.NewsletterId))
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
            return View(newsletter);
        }

        // GET: Newsletters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.SingleOrDefaultAsync(m => m.NewsletterId == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // POST: Newsletters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var newsletter = await _context.Newsletters.SingleOrDefaultAsync(m => m.NewsletterId == id);
            string oldimagepath = newsletter.ImagePath;
            _context.Newsletters.Remove(newsletter);
            await _context.SaveChangesAsync();
            //delete old image
            oldimagepath = hostingEnv.WebRootPath + $@"\images\Newsletters\{oldimagepath}";
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

        private bool NewsletterExists(string id)
        {
            return _context.Newsletters.Any(e => e.NewsletterId == id);
        }
    }
}
