﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ChatDefenders.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
			return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}