using Asp_task4.Data;
using Asp_task4.Utilities.EmailHandler.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace Asp_task4.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public SubscriptionController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var subscribers = _context.Subscribers.Where(s => s.IsSubscribed).ToList(); 
            return View(subscribers);
        }

        [HttpPost]
        public IActionResult SendDiscountEmails(int discountPercentage)
        {
            var subscribers = _context.Subscribers.Where(s => s.IsSubscribed).ToList(); 

            foreach (var subscriber in subscribers)
            {
                var message = new Message
                {
                    To = new List<string> { subscriber.Email },
                    Subject = "Endirim E-poçtu",
                    Content = $"Sevgili Abunəçi, bu gün {discountPercentage}% endirim əldə edə bilərsiniz!"
                };

                _emailService.SendMessage(message); 
            }

            return RedirectToAction("Index");
        }
    }
}
