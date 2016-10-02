using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Version1.ViewModels.Venue;
using Version1.Models;

namespace Version1.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Booking()
        {
            return View();
        }

        // GET: /<controller>/DatePicking
        [HttpGet]
        public IActionResult DatePicking()
        {
            return View();
        }

        // Post: /<controller>/DatePicking
        [HttpPost]
        public IActionResult DatePicking(BookingViewModel viewModel)
        {
            return View("ContactDetail", viewModel);
        }

        // GET: /<controller>/ContactDetail
        [HttpGet]
        public IActionResult ContactDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactDetail(BookingViewModel viewModel)
        {
            BookingOrder order = new BookingOrder();

            order.DayName = viewModel.DayName;
            order.DayNumber = viewModel.DayNumber;
            order.StartTime = int.Parse(viewModel.StartTime);
            order.EndTime = int.Parse(viewModel.EndTime);

            _context.BookingOrders.Add(order);
            _context.SaveChanges();

            return View();
        }
    }
}
