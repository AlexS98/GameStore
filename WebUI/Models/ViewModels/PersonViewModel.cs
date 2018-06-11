using GameStore.StoreDomain.Abstract;
using GameStore.StoreDomain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels
{
    public class PersonViewModel
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("Password", ErrorMessage = "The New Password and Confirm New Password fields did not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public IPerson ToIPerson()
        {
            IPerson person;
            if (Role == UserRole.User) person = new User();
            else person = new Admin(); 

            person.Nickname = Nickname;
            person.Email = Email;
            person.Password = Password;
            person.Role = Role;
            return person;
        }
    }
}