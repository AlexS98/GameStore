using GameStore.StoreDomain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.ViewModels
{
    public class UserCabinetViewModel
    {
        public User User { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+[ ][a-zA-Z]+$", ErrorMessage = "Wrong name")]
        public string UserName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImageData { get; set; }
        public string AvatarImageMimeType { get; set; }

        public UserCabinet ToUserCabinet()
        {
            return new UserCabinet
            {
                User = User,
                UserName = UserName,
                Address = Address,
                PhoneNumber = PhoneNumber,
                AvatarImageData = AvatarImageData,
                AvatarImageMimeType = AvatarImageMimeType
            };
        }
    }
}