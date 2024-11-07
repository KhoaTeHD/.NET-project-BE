using System.ComponentModel.DataAnnotations;

namespace Services.BrandAPI.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
