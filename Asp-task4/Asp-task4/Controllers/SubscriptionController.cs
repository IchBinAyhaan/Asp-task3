using Asp_task4.Data; 
using Asp_task4.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task4.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            var subscriber = new SubscriberVM
            {
                Email = email,
                IsSubscribed = true
            };

            _context.Subscribers.Add(subscriber);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}