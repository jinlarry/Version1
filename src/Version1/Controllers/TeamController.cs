using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Version1.ViewModels.Team;

namespace Version1.Controllers
{
    public class TeamController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TeamDetail(string param)
        {
            return View();
        }
    }
}
