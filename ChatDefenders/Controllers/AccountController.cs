using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ChatDefenders.Controllers
{
    public class AccountController : Controller
    {
		// Challenges with a redirect to the discord login page.
		public IActionResult Login() =>
			Challenge(new AuthenticationProperties { RedirectUri = "/" });
	}
}