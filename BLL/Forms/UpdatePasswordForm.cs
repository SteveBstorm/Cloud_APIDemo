using System.ComponentModel.DataAnnotations;

namespace BLL.Forms
{
    public class UpdatePasswordForm
    {
        public int Id { get; set; }

        public string OldPassword { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmationPassword { get; set; }

    }
}
