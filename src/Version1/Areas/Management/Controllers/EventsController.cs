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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.event_ID == id);
            if(events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("event_ID,event_address,event_datetime,event_name,event_picture,event_profile")] Events events)
        {
            if(ModelState.IsValid)
            {
                var g = Guid.NewGuid();
                events.event_ID = g.ToString();
                //   events.event_picture = TempData["photoaddress"].ToString();
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.event_ID == id);
            if(events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Events events)
        {
            if(id != events.event_ID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    if(TempData["photoaddress"] != null)
                    {
                        events.event_picture = TempData["photoaddress"].ToString();
                    }

                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!EventsExists(events.event_ID))
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
            return View(events);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.event_ID == id);
            if(events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var events = await _context.Events.SingleOrDefaultAsync(m => m.event_ID == id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventsExists(string id)
        {
            return _context.Events.Any(e => e.event_ID == id);
        }
    }
}
