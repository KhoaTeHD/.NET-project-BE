using Services.ProductAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models
{
    public class ProductVariation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Pro_Id { get; set; }
        [Required]
        public int Col_Id { get; set; }
        [Required]
        public int Siz_Id { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal ImportPrice { get; set; }
        [Required]
        public string Pic { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? Desc { get; set; }
        public int? Discount { get; set; }
        [Required]
        public Boolean Status { get; set; }

        [JsonIgnore]
        [ForeignKey("Pro_Id")]
        public Product Product { get; set; }
        [NotMapped]
        public ColorDto Color { get; set; }
        [NotMapped]
        public SizeDto Size { get; set; }
    }
}
