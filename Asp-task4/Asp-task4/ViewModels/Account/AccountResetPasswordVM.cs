using System.ComponentModel.DataAnnotations;

namespace Asp_task4.ViewModels.Account
{
    public class AccountResetPasswordVM
    {
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [Compare(nameof(Password), ErrorMessage = "Sifre ile tesdiq sifresi uygun deyil")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
