﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class ConsentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
    }
}