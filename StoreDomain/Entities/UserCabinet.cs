﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public class UserCabinet
    {
        public int UserCabinetId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImageData { get; set; }
        public string AvatarImageMimeType { get; set; }
    }
}
