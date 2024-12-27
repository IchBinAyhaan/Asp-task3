using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asp_task4.Areas.Admin.Models.User
{
    public class UserUpdateVM
    {
        public UserUpdateVM()
        {
            RolesIds = new List<string>();
        }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mutler daxil edilmelidir")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "Sifre ile tesdiq sifresi uygun deyil")]
        public string? ConfirmNewPassword { get; set; }
        public List<SelectListItem>? Roles { get; set; }
        public List<string>? RolesIds { get; set; }
    }
}
