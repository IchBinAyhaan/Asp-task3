using System.ComponentModel.DataAnnotations;

namespace Asp_task4.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mutler daxil edilmelidir")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [Compare(nameof(Password), ErrorMessage = "Sifre ile tesdiq sifresi uygun deyil")]
        public string ConfirmPassword { get; set; }
    }
}
