using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
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


        #region ================ Action: TeamManage ================

        [HttpGet]
        public IActionResult TeamManage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeamManage(TeamManageViewModel viewModel)
        {
            return View();
        }

        #endregion


        #region ================ Action: EditTeam ================

        // Team/EditTeam/<Team ID>
        [HttpGet]
        public IActionResult EditTeam(string param)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditTeam(EditTeamViewModel viewModel)
        {
            return View();
        }

        #endregion
    }
}
