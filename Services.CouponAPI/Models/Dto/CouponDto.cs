namespace Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public string Coupon_Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string CouponName { get; set; }

        public float Discount { get; set; }

        public string Unit { get; set; }

        public Boolean Status { get; set; }
    }
}
