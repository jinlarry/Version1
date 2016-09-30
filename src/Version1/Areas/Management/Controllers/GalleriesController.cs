using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Version1.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Version1.Controllers
{  [Area("Management")]
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment hostingEnv;
        public GalleriesController(IHostingEnvironment env, ApplicationDbContext context)
        {
            this.hostingEnv = env;
            _context = context;    
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
           // _context.Gallery.Include(p=>p.Authorinfo)
            return View(await _context.Gallery.Include(p => p.Authorinfo).ToListAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.PhotoID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }
        [HttpGet]
        // GET: Galleries/Create
        public IActionResult Create(string a)
        {
            ViewData["AlbumSelectList"] = new SelectList(_context.Gallery.Select(p => new { p.Album }),  "Album", "Album");
            
            return View();
        }

         

        //http://www.binaryintellect.net/articles/f1cee257-378a-42c1-9f2f-075a3aed1d98.aspx
       
        [HttpPost]
        public IActionResult CreateGallery()
        {
             //long size = 0;
            var files = Request.Form.Files;
            string albumtext= Request.Form["Photo_Ablum"];
            string titletext= Request.Form["Photo_Title"];
            if (files.Count>0)
            {            
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];        
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        if (filename != "")
                        {                    
                            string PhotoGUID = Guid.NewGuid().ToString();
                            Gallery gallery = new Gallery();
                            gallery.PhotoID = PhotoGUID;
                            gallery.Album = albumtext;
                            gallery.PhotoTitle = titletext;
                            gallery.Author = User.FindFirst(ClaimTypes.NameIdentifier).Value;//Asp.net core get current user's ID
                            gallery.CreateDate = DateTime.Now;
                            filename = PhotoGUID.ToString() + filename.Substring(filename.Length - 4, 4);
                            gallery.PhotoPath = filename;
                            gallery.PhotoSize = file.Length;
                            filename = hostingEnv.WebRootPath + $@"\images\Gallery\{filename}";
                           // size += file.Length;
                            using (FileStream fs = System.IO.File.Create(filename))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                            _context.Add(gallery);
                        }              
                    }            
                    _context.SaveChanges();
                    return RedirectToAction("Index");
            }
            return  View();
        }
        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["AlbumEdit"] = new SelectList(_context.Gallery.Select(p => new { p.Album }), "Album", "Album");
            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.PhotoID == id);
            if (gallery == null)
            {
                return NotFound();
            }
            
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhotoID,Album,Author,CreateDate,PhotoPath,PhotoSize,PhotoTitle")] Gallery gallery)
        {
            if (id != gallery.PhotoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string oldimagepath = gallery.PhotoPath;
                    Boolean IfNewimage = false;
                    gallery.Author= User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var file = Request.Form.Files[0];
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    if (filename != "")
                    {
                        gallery.CreateDate = DateTime.Now;
                        filename = gallery.PhotoID+ filename.Substring(filename.Length - 4, 4);
                        gallery.PhotoPath = filename;
                        gallery.PhotoSize = file.Length;                        
                        filename = hostingEnv.WebRootPath + $@"\images\Gallery\{filename}";
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                            IfNewimage = true;
                        }
                    }

                    _context.Update(gallery);
                     
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.PhotoID))
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
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.PhotoID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.PhotoID == id);
            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryExists(string id)
        {
            return _context.Gallery.Any(e => e.PhotoID == id);
        }
    }
}
public class myfile
{
    public string filename { get; set; }
    public IFormFile file { get; set; } 
}
