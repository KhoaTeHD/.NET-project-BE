using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.GoodsReceiptAPI.Models
{
    public class GoodsReceipt
    {
        [Key]
        public int Goo_ID { get; set; }

        [Required]
        public int Supplier_ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Datetime { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        // Navigation property
        public ICollection<DetailGoodsReceipt> DetailGoodsReceipts { get; set; }
    }
}
