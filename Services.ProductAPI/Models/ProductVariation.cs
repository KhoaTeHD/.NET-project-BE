using System.ComponentModel.DataAnnotations;

namespace Services.ProductAPI.Models
{
    public class ProductVariation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Col_Id { get; set; }
        [Required]
        public int Siz_Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal ImportPrice { get; set; }
        [Required]
        public string Pic { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Desc { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
