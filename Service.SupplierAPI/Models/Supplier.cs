using System.ComponentModel.DataAnnotations;

namespace Services.SupplierAPI.Models
{
    public class Supplier
    {
        [Key]
        public int Supplier_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        public Boolean Status { get; set; }
    }
}

