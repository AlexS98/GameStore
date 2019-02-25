using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.ViewModels
{
    public class CabinetViewModel
    {
        public int UserCabinetId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+[ ][a-zA-Z]+$", ErrorMessage = "Wrong name")]
        public string UserName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImageData { get; set; }
        public string AvatarImageMimeType { get; set; }
    }
}