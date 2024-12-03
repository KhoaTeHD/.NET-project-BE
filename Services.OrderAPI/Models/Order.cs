using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.OrderAPI.Models
{
    public class Order
    {
        [Key]
        public long Order_ID { get; set; }

        [Required]
        public string Customer_ID { get; set; }

        public int? Coupon_Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime Datetime { get; set; }

        [Column(TypeName = "money")]
        public decimal Discount_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Shipping_Charge { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public string OrderStatus { get; set; }
        public string? FormOfPayment { get; set; }

        // Navigation property
        public ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
