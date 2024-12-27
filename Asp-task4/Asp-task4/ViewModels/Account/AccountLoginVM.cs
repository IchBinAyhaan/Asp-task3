using System.ComponentModel.DataAnnotations;

namespace Asp_task4.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mutleq daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
