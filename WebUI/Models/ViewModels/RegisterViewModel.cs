using StoreDomain.Abstract;
using StoreDomain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        string Email { get; set; }
        [Required]
        string Password { get; set; }
        [Required]
        UserRole Role { get; set; }

        public IPerson ToIPerson()
        {
            IPerson person;
            if (Role == UserRole.User)
                person = new User();
            else
                person = new Admin(); 
            person.Nickname = Nickname;
            person.Email = Email;
            person.Password = Password;
            person.Role = Role;
            return person;
        }
    }
}