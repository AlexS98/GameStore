using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebUI")]
namespace StoreDomain.Entities
{
    class UserCabinet
    {
        public int UserCabinetId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+[ ][a-zA-Z]+$", ErrorMessage = "Wrong name")]
        public string UserName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImageData { get; set; }
        public string AvatarImageMimeType { get; set; }
    }
}
