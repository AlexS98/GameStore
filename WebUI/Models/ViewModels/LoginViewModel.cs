using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}