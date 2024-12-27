using Asp_task4.Areas.Admin.Models.Account;
using Asp_task4.Entities;
using Asp_task4.Utilities.EmailHandler.Abstract;
using Asp_task4.Utilities.EmailHandler.Models;
using Asp_task4.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Asp_task4.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManage,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManage;
            _roleManager = roleManager;
           
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email ve ya sifre yalnisdir");
                return View(model);

            }
            if(_userManager.IsInRoleAsync(user,"Admin").Result)
            {
                ModelState.AddModelError(string.Empty, "Email ve ya sifre yalnisdir");
                return View(model);
            }
            var result = _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email ve ya sifre yalnisdir");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                Redirect(model.ReturnUrl);

            return RedirectToAction("index", "home");
        }   
    }
}
