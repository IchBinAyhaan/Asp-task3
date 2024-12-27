using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task4.Controllers
{
    [Authorize(Roles = "Director,Seller")]
    public class SaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
