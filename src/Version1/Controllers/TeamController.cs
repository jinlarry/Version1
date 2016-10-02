using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Version1.Models;
using Version1.ViewModels.Team;

namespace Version1.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(FronTeamViewModel viewModel)
        {
            var teams = viewModel.Teams;

            foreach(var team in _context.Teams)
            {
                teams.Add(team);
            }

            return View(viewModel);
        }

        //public IActionResult TeamDetail(string param)
        //{
        //    return View();
        //}

        public IActionResult TeamDetail()
        {
            return View();
        }

    }
}
