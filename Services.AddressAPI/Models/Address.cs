using System.ComponentModel.DataAnnotations;

namespace Services.AddressAPI.Models
{
    public class Address
    {
        [Key]
        public int Address_ID { get; set; }

        [Required]
        public int Customer_ID { get; set; }

        [Required]
        public string AddressLine { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Ward { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}
