namespace Services.OrderAPI.Models.Dto
{
    public class OrderDto
    {
        public long Order_ID { get; set; }

        public string Customer_ID { get; set; }

        public int? Coupon_Code { get; set; }

        public string Address { get; set; }

        public DateTime Datetime { get; set; }

        public decimal Discount_amount { get; set; }

        public decimal Total { get; set; }

        public string OrderStatus { get; set; }

        public string? FormOfPayment { get; set; }

        public decimal? Shipping_Charge { get; set; }

        // List of detail orders
        public List<DetailOrderDto> DetailOrders { get; set; }
    }
}
