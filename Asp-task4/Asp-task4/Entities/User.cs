using Microsoft.AspNetCore.Identity;

namespace Asp_task4.Entities
{
    public class User : IdentityUser
    {
        public string Country {  get; set; }
        public string City { get; set; }
    }
}
