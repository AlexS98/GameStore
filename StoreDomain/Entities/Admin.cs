using GameStore.StoreDomain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebUI")]
namespace GameStore.StoreDomain.Entities
{
    public class Admin : IPerson
    {
        public Admin() { }
        public int AdminId { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }

        public int GetPersonId()
        {
            return AdminId;
        }
    }
}
