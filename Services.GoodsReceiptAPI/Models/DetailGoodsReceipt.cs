using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Services.GoodsReceiptAPI.Models.Dto;

namespace Services.GoodsReceiptAPI.Models
{
    public class DetailGoodsReceipt
    {

        [Required]
        public int Goo_ID { get; set; }

        [Required]
        public int Product_ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Unit_Price { get; set; }

        // Navigation property
        [ForeignKey("Goo_ID")]
        public GoodsReceipt GoodsReceipt { get; set; }

        [NotMapped]
        public ProductVariationDto? ProductVariation { get; set; }
    }
}
