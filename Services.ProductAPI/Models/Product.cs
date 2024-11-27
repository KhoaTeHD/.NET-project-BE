using Services.ProductAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cat_Id { get; set; }
        [Required]
        public int Nat_Id { get; set; }
        [Required]
        public int Bra_Id { get; set; }
        [Required]
        public int Sup_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
        [JsonIgnore]
        public ICollection<ProductVariation>? ProductVariations { get; set; }
        [NotMapped]
        public BrandDto Brand { get; set; }
        [NotMapped]
        public CategoryDto Category { get; set; }
        [NotMapped]
        public NationDto Nation { get; set; }
        [NotMapped]
        public SupplierDto Supplier { get; set; }
    }
}
