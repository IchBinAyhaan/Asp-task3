﻿using Asp_task4.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PB401_PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<User> _userManager;    
        public DashboardController(UserManager<User> userManager)
        {
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            return View();
        }
    }
}
