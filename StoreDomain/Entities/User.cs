using StoreDomain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace StoreDomain.Entities
{
    class User : IPerson
    {
        public int UserId { get; set; }
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
            throw new System.NotImplementedException();
        }
    }
}
