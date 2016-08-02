using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Version1.Models;
using Version1.ViewModels.Team;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Version1.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "manager")]
    public class TeamManageController : Controller

    {
        private readonly ApplicationDbContext _context;
        TeamMemberViewModel memberViewModel = new TeamMemberViewModel();
        TeamMemberIndexViewModel memberIndexViewModel = new TeamMemberIndexViewModel();
        TeamDetailViewModel teamDetailViewModel = new TeamDetailViewModel();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // return;
            if(ModelState.Count == 0)
            {
                var areaname = filterContext.ActionDescriptor.RouteConstraints.Where(i => i.RouteKey == "area").First().RouteValue.ToString();
                var controllerName = filterContext.ActionDescriptor.RouteConstraints.Where(i => i.RouteKey == "controller").First().RouteValue.ToString();
                var actionName = filterContext.ActionDescriptor.Name;
                // var userId = _userManager.GetUserAsync(HttpContext.User).Id.ToString();
                var userId = "";
                var FullControllerName = areaname + "/" + controllerName;
                var username = filterContext.HttpContext.User.Identity.Name;
                if(username == null)
                {
                    filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                    return;
                }
                string itemrole;
                List<string> aa = new List<string>();
                if(username == "")
                { filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." }; return; }
                else if(username != "")
                {
                    //1.retrieving the role of the user from DB
                    userId = _context.Users.Where(aaa => aaa.UserName == username).First().Id.ToString();
                    foreach(var item in _context.UserRoles.Where(u => u.UserId == userId).ToList())
                    {
                        aa.Add(item.RoleId.ToString());
                    }
                    if(aa == null)
                    {
                        filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                        return;
                    }
                    else if(aa.Count == 0)
                    {
                        filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                        return;
                    }
                    if(_context.Authorization_Object.Where(N => N.ObjectType.ToUpper() == "FUNCTION").Where(N => N.ActionName.ToUpper() == actionName.ToUpper()).Where(N => N.FullControllerName.ToUpper() == FullControllerName.ToUpper()).Count() > 0)
                    {
                        //2.retrieving the role related to the ACTION from DB
                        var Object_ID = _context.Authorization_Object.Where(N => N.ObjectType.ToUpper() == "FUNCTION").Where(N => N.ActionName.ToUpper() == actionName.ToUpper()).Where(N => N.FullControllerName.ToUpper() == FullControllerName.ToUpper()).FirstOrDefault().ID.ToString();
                        List<Authorization_Object_Role> bb = _context.Authorization_Object_Role.Where(p => p.Authorization_Object_ID.ToString() == Object_ID).ToList();
                        //3.contrast the two roles,find if they are consistent 
                        if(bb == null)
                        {
                            filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                            return;
                        }
                        else if(bb.Count == 0)
                        {
                            filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                            return;
                        }
                        else
                        {
                            foreach(var item in bb)
                            {
                                itemrole = item.RoleID.ToString();
                                if(aa.Contains(itemrole) == true)
                                {
                                    filterContext.Result = filterContext.Result;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    filterContext.Result = new ContentResult { Content = @"Sorry you don't have any aothrization." };
                    return;
                }
            }
        }
        public TeamManageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            memberIndexViewModel.Teams = await _context.Teams.ToListAsync();
            memberIndexViewModel.TeamMembers = _context.TeamMembers.ToList();
            return View(memberIndexViewModel);
            // return View(await _context.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public IActionResult Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            teamDetailViewModel.TeamId = id;
            teamDetailViewModel.TeamName = _context.Teams.Where(m => m.TeamId == id).First().TeamName;
            var memberlist = (from a in _context.TeamMembers
                              join b in _context.ApplicationUser on a.UserID equals b.Id
                              where a.TeamId == id
                              select new { volunteer_id = b.Id, volunteer_firstname = b.FirstName, volunteer_lastname = b.LastName }).Distinct();
            if(_context.Teams.Where(m => m.TeamId == id) != null)
            {
                if(_context.Teams.Where(m => m.TeamId == id).Count() > 0)
                {
                    teamDetailViewModel.LeaderId = _context.Teams.Where(m => m.TeamId == id).First().TeamLeaderID;
                    if(teamDetailViewModel.LeaderId != null)
                    {
                        teamDetailViewModel.LeaderFirstName = _context.ApplicationUser.Where(a => a.Id == teamDetailViewModel.LeaderId).First().FirstName;
                        teamDetailViewModel.LeaderLastName = _context.ApplicationUser.Where(a => a.Id == teamDetailViewModel.LeaderId).First().LastName;
                        teamDetailViewModel.LeaderContact = _context.ApplicationUser.Where(a => a.Id == teamDetailViewModel.LeaderId).First().PhoneNumber;
                        teamDetailViewModel.LeaderPortrait = _context.ApplicationUser.Where(a => a.Id == teamDetailViewModel.LeaderId).First().Portrait;
                    }

                }
            }

            List<TeamMemberDetailViewModel> dd = new List<TeamMemberDetailViewModel>();
            foreach(var item in memberlist)
            {
                TeamMemberDetailViewModel pp = new TeamMemberDetailViewModel();
                pp.VolunteerId = item.volunteer_id.ToString();
                pp.FirstName = item.volunteer_firstname.ToString();
                pp.LastName = item.volunteer_lastname.ToString();

                dd.Add(pp);

            }

            teamDetailViewModel.TeamMemberDetails = dd;


            if(teamDetailViewModel == null)
            {
                return NotFound();
            }

            return View(teamDetailViewModel);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team Team)
        {
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            Team.TeamId = g.ToString();
            
            if(ModelState.IsValid)
            {
                _context.Add(Team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var Team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            if(Team == null)
            {
                return NotFound();
            }
            return View(Team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Team Team)
        {
            if(id != Team.TeamId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(Team);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!TeamExists(Team.TeamId))
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
            return View(Team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var Team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            if(Team == null)
            {
                return NotFound();
            }

            return View(Team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var Team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            _context.Teams.Remove(Team);
            var members = _context.TeamMembers.Where(n => n.TeamId == id);
            _context.TeamMembers.RemoveRange(members);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(string id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }

        public ActionResult TeamMember(string id)
        {
            if(id == "")
            {
                memberViewModel.Teams = _context.Teams.ToList();
            }
            if(id == null)
            {
                memberViewModel.Teams = _context.Teams.ToList();
            }
            else
            {
                TempData["tempTeamid"] = id;
                memberViewModel.Teams = _context.Teams.Where(a => a.TeamId == id).ToList();
            }

            memberViewModel.Volunteers = _context.ApplicationUser.ToList();
            memberViewModel.Members = _context.TeamMembers.ToList();

            return View("TeamMember", memberViewModel);


        }

        [HttpPost]
        public ActionResult Update(TeamMemberViewModel viewMolde)
        {
            var all = from c in _context.TeamMembers select c;
            string pp = "";
            if(ModelState.IsValid)
            {
                if(TempData["tempTeamid"] != null)
                {
                    pp = TempData["tempTeamid"].ToString();
                }
                if(pp == null)
                {
                    all = from c in _context.TeamMembers select c;
                }
                if(pp == "")
                {
                    all = from c in _context.TeamMembers select c;
                }
                else
                {
                    all = from c in _context.TeamMembers where (c.TeamId == pp) select c;
                }

                if(viewMolde.Members != null)
                {
                    foreach(var item in viewMolde.Members)
                    {
                        _context.TeamMembers.Add(new TeamMember { TeamId = item.TeamId, UserID = item.UserID });
                    }
                }

                _context.TeamMembers.RemoveRange(all);
                _context.SaveChanges();
                // TempData["Message"] = num.ToString() + " Change have finished!";
                TempData["tempTeamid"] = "";
            }
            // return Content("<script language='javascript' type='text/javascript'>alert('Hello world!');</script>");

            return RedirectToAction("Index");
        }

        public IActionResult NewLeader(string id)
        {
            var viewmodel = new NewLeaderViewModel();
            //  string Sql = "select Id,FirstName,LastName,PhoneNumber from AspNetUsers";           
            // var temp = _context.ApplicationUser.FromSql(Sql).ToList();
            var temp = _context.ApplicationUser.Select(a => new { a.Id, a.FirstName, a.LastName, a.PhoneNumber }).ToList();
            var Leaderlist = new List<Leader>();
            TempData["Teamid"] = id;
            foreach(var item in temp)
            {
                var sm = new Leader();
                sm.LeaderId = item.Id;
                sm.FirstName = item.FirstName;
                sm.LastName = item.LastName;
                sm.Contact = item.PhoneNumber;
                Leaderlist.Add(sm);
            }
            viewmodel.TeamId = id;
            viewmodel.Leaders = Leaderlist;
            return View("NewLeader", viewmodel);
        }

        [HttpPost]
        public ActionResult ConfirmLeader(NewLeaderViewModel NewLeader)
        {
            _context.Teams.Where(a => a.TeamId == NewLeader.TeamId).First().TeamLeaderID = NewLeader.LeaderId;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = NewLeader.TeamId });

        }
    }
}

