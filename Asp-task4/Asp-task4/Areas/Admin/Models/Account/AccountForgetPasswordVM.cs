using System.ComponentModel.DataAnnotations;

namespace Asp_task4.Areas.Admin.Models.Account
{
    public class AccountForgetPasswordVM
    {
        [Required(ErrorMessage ="Mail daxil edilmelidir")]
        public int Email { get; set; }
    }
}
