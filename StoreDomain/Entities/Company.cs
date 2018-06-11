using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public enum CompanyType
    {
        limited_company = 0,
        limited_partnership = 1,
        unlimited_partnership = 2,
        chartered_company = 3,
        statutory_company = 4,
        holding_company = 5,
        subsidiary_company = 6
    }

    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual CompanyType Type { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+[ ][a-zA-Z]+$", ErrorMessage = "Wrong name")]        
        public string FullName { get; set; }

        public byte[] LogoImageData { get; set; }
        public string LogoImageMimeType { get; set; }
    }
}
