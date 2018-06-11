using GameStore.StoreDomain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public class User : IPerson
    {
        public User() { }
        public int UserId { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }

        public int GetPersonId()
        {
            return UserId;
        }
    }
}
