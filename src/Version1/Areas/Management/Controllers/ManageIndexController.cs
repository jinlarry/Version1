using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Version1.Areas.Management.Controllers
{
    [Area("Management")]
    public class ManageIndexController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<MIviewmodel_Chart>  chart= new List<MIviewmodel_Chart>() ;
            chart.Add(new MIviewmodel_Chart() { x="2014",y=27}  );
            chart.Add(new MIviewmodel_Chart() { x = "2013", y = 17 });
            chart.Add(new MIviewmodel_Chart() { x = "2015", y = 88 });
            chart.Add(new MIviewmodel_Chart() { x = "2016", y =60 });
            MIviewmodel  viewmodel = new   MIviewmodel();
            viewmodel.chartdata = JsonConvert.SerializeObject(chart);
            return View(viewmodel);
        }
    }
    public class MIviewmodel 
    {
        public string chartdata { get; set; }
    }
    public class MIviewmodel_Chart
    {
        public String x { get; set; }
        public int y { get; set; }

    }
}

