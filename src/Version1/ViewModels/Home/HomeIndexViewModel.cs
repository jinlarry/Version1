using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version1.Models;

namespace Version1.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<Events> Events { get; set; }

        public HomeIndexViewModel()
        {
            Events = new List<Models.Events>();
        }
    }
    
}
