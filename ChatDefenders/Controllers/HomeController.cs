using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatDefenders.Models;
using ChatDefenders.Data;

namespace ChatDefenders.Controllers
{
    public class HomeController : Controller
    {
		private readonly PostContext _context;

		public HomeController(PostContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {
			foreach(var p in _context.Posts)
			{
				Debug.WriteLine(p.PostAuthor.Name);
			}

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
