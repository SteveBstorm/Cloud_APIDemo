using System.ComponentModel.DataAnnotations;

namespace BLL.Forms
{
    public class RegisterForm
    {
        [EmailAddress]
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }


        [Required]
        [MaxLength(30)]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmationPassword { get; set; }


        [MinLength(3)]
        public string Pseudo { get; set; }
    }
}
