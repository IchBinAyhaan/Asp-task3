using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task4.Controllers
{
	[Authorize(Roles = "Director,HR")]
	public class VacancyController : Controller
	{	
		public IActionResult Index()
		{
			return View();
		}
	}
}
