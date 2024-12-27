using Asp_task4.Constants;
using Asp_task4.Entities;
using Microsoft.AspNetCore.Identity;

namespace Asp_task4
{
	public static class DbInitializer
	{
		public static void Seed(UserManager<User> userManager,RoleManager<IdentityRole>roleManager)
		{
			AddRoles(roleManager);
			AddAdmin(userManager,roleManager);
		}
		private static void AddRoles(RoleManager<IdentityRole> roleManager)
		{
			foreach (var role in Enum.GetValues<UserRoles>())
			{
				if (!roleManager.RoleExistsAsync(role.ToString()).Result)
				{
					_ = roleManager.CreateAsync(new IdentityRole
					{
						Name = role.ToString(),
					}).Result;

				}
			}

		}
		private static void AddAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (userManager.FindByEmailAsync("admin@app.com").Result is null)
			{
				var user = new User
				{
					UserName = "admin@app.com",
					Email = "admin@app.com",
					City = "Baku",
					Country = "Azerbaijan",
				};
				var result = userManager.CreateAsync(user, "Admin123!").Result;
				if (!result.Succeeded)
					throw new Exception("Admin elave etmek mumkun olmadi");

				var role = roleManager.FindByNameAsync("Admin").Result;
				if (role?.Name is null)
					throw new Exception("Admin rolu tapilmadi");

				var addToRoleResult = userManager.AddToRoleAsync(user, role.Name).Result;
				if (!addToRoleResult.Succeeded)
					throw new Exception("Istifadeciye admin rolunu elave etmek mumkun olmadi");

			}
		}
	}
}
