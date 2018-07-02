using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatDefenders.Models;
using ChatDefenders.Data;
using System.Security.Claims;
using static System.Diagnostics.Debug;

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
			var userAccount = Account.GetByUserIdentity((ClaimsIdentity) User.Identity);
			var model = new IndexViewModel(userAccount);
			return View(model);
		}
		public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
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
