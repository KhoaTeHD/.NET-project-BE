using System;
using System.ComponentModel.DataAnnotations;

namespace Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int Coupon_Code { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponName { get; set; }

        [Required]
        public float Discount { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; }

        [Required]
        public Boolean Status { get; set; }
    }
}
