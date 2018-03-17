using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeatureSetDemo.Models;
using System.Text;
using System.Threading;

namespace FeatureSetDemo.Controllers
{
    public class HomeController : Controller
    {
        private static List<String> leakContainer = new List<string>();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Section in app that simulates high CPU cost.";
            doCPUIntensiveTask(null);
            return View();
        }

        private static void doCPUIntensiveTask(Object stateInfo)
        {
            Random r = new Random();
            int randomComputation = 1;
            for (int i = 0; i < 1000000000; i++)
            {
                randomComputation ^= r.Next();
            }
        }

        public IActionResult Contact()
        {
           String message = "Section in application that simulates a memory leak .";

            for (int i = 0; i < 10000000; i++) {
                StringBuilder sb = new StringBuilder();
                sb.Append(leakContainer.Count().ToString());
                sb.Append('a', 1024);
                leakContainer.Add(sb.ToString());
            }
            ViewData["Message"] = message + " Currently leaking " + leakContainer.Count + " objects";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
