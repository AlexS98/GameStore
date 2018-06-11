using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public class GameAddition
    {
        public int GameAdditionId { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }

        public byte[] Screen1ImageData { get; set; }
        public string Screen1ImageMimeType { get; set; }

        public byte[] Screen2ImageData { get; set; }
        public string Screen2ImageMimeType { get; set; }

        public byte[] Screen3ImageData { get; set; }
        public string Screen3ImageMimeType { get; set; }
    }
}
