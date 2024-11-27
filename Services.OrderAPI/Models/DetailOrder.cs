using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Services.OrderAPI.Models.Dto;

namespace Services.OrderAPI.Models
{
    public class DetailOrder
    {
        [Required]
        public long Order_ID { get; set; }

        [Required]
        public int Product_ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Unit_Price { get; set; }

        // Navigation property
        [ForeignKey("Order_ID")]
        public Order Order { get; set; }

        [NotMapped]
        public ProductVariationDto ProductVariation { get; set; }
    }
}
