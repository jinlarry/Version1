using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Version1.Models;

namespace Version1.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "manager")]
    public class Authorization_ObjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Authorization_ObjectController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Authorization_Object
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authorization_Object.ToListAsync());
        }

        // GET: Authorization_Object/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization_Object = await _context.Authorization_Object.SingleOrDefaultAsync(m => m.ID == id);
            if (authorization_Object == null)
            {
                return NotFound();
            }

            return View(authorization_Object);
        }

        // GET: Authorization_Object/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authorization_Object/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Area("Management")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ObjectName,ObjectType,ActionName,FullControllerName,ObjectDescription")] Authorization_Object authorization_Object)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorization_Object);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(authorization_Object);
        }

        // GET: authorization_Object/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization_Object = await _context.Authorization_Object.SingleOrDefaultAsync(m => m.ID == id);
            if (authorization_Object == null)
            {
                return NotFound();
            }
            return View(authorization_Object);
        }

        // POST: authorization_Object/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ObjectName,ObjectType,ActionName,FullControllerName,ObjectDescription")]Authorization_Object authorization_Object)
        {
            if (id != authorization_Object.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorization_Object);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!authorization_ObjectExists(authorization_Object.ID))
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
            return View(authorization_Object);
        }

        // GET: authorization_Object/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization_Object = await _context.Authorization_Object.SingleOrDefaultAsync(m => m.ID == id);
            if (authorization_Object == null)
            {
                return NotFound();
            }

            return View(authorization_Object);
        }

        // POST: authorization_Object/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorization_Object = await _context.Authorization_Object.SingleOrDefaultAsync(m => m.ID == id);
            _context.Authorization_Object.Remove(authorization_Object);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool authorization_ObjectExists(int id)
        {
            return _context.Authorization_Object.Any(e => e.ID == id);
        }
    }
}
