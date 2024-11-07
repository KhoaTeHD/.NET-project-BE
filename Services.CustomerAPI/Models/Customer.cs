using System.ComponentModel.DataAnnotations;
namespace Services.CustomerAPI.Models
{
    public class Customer
    {
        [Key]
        public int Cus_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Cus_Name { get; set; }

        public string Cus_Avatar { get; set; }

        [Required]
        [EmailAddress]
        public string Cus_Email { get; set; }

        [Required]
        [StringLength(11)]
        public string Cus_Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Cus_Password { get; set; }

        [Required]
        [StringLength(4)]
        public string Cus_Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Cus_Birthday { get; set; }

        [Required]
        public Boolean Cus_Status { get; set; }
    }
}
