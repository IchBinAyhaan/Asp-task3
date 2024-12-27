using Asp_task4.Areas.Admin.Models.User;
using Asp_task4.Constants;
using Asp_task4.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp_task4.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = new List<UserVM>();

            foreach (var user in _userManager.Users.ToList())
            {
                if (!_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    users.Add(new UserVM
                    {
                        Id = user.Id,
                        Email = user.Email,
                        City = user.City,
                        Country = user.Country,
                        PhoneNumber = user.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(user).Result.ToList()
                    });
                }
            }

            var model = new UserIndexVM
            {
                Users = users
            };

            return View(model);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserCreateVM
            {
                Roles = _roleManager.Roles.Where(r => r.Name != "Admin").Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(UserCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                Email = model.Email,
                City = model.City,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            foreach (var roleId in model.RolesIds)
            {
                var role = _roleManager.FindByIdAsync(roleId).Result;
                if (role is null)
                {
                    ModelState.AddModelError("RoleIds", "Secdiyiniz rol movcud deyil");
                    return View(model);
                }

                var addToRoleResult = _userManager.AddToRoleAsync(user, role.Name).Result;
                if (!addToRoleResult.Succeeded)
                {
                    ModelState.AddModelError("RoleIds", "Secdiyiniz rol elave etmek mumkun olmadi");
                    return View(model);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        public IActionResult Update(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();

            List<string> roleIds = new List<string>();
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var userRole in userRoles)
            {
                var role = _roleManager.FindByNameAsync(userRole).Result;
                roleIds.Add(role.Id);
            }

            var model = new UserUpdateVM
            {
                Country = user.Country,
                City = user.City,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _roleManager.Roles.Where(r => r.Name != UserRoles.Admin.ToString()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList(),
                RolesIds = roleIds
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(string id, UserUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();

            user.Country = model.Country;
            user.City = model.City;
            user.PhoneNumber = model.PhoneNumber;
            if (model.NewPassword is not null)
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

            List<string> roleIds = new List<string>();
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var userRole in userRoles)
            {
                var role = _roleManager.FindByNameAsync(userRole).Result;
                roleIds.Add(role.Id);
            }

            var shouldBeAddedRoleIds = model.RolesIds.Except(roleIds).ToList();
            var shouldBeDeletedRoleIds = roleIds.Except(model.RolesIds).ToList();
            foreach (var roleId in shouldBeAddedRoleIds)
            {
                var role = _roleManager.FindByIdAsync(roleId).Result;
                if (role is null)
                {
                    ModelState.AddModelError("RoleIds", "Rol tapilmadi");
                    return View(model);
                }
                var result = _userManager.AddToRoleAsync(user, role.Name).Result;
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(model);
                }
            }
            foreach (var roleId in shouldBeDeletedRoleIds)
            {
                var role = _roleManager.FindByIdAsync(roleId).Result;
                if (role is null)
                {
                    ModelState.AddModelError("RoleIds", "Rol tapilmadi");
                    return View(model);
                }
                var result = _userManager.RemoveFromRoleAsync(user, role.Name).Result;
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View(model);
                }
            }

            var updateResult = _userManager.UpdateAsync(user).Result;
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();

            var result = _userManager.DeleteAsync(user).Result;
            if (!result.Succeeded) return NotFound();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}

