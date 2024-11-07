namespace Services.OrderAPI.Models.Dto
{
    public class DetailOrderDto
    {
        public long Order_ID { get; set; }

        public int Product_ID { get; set; }

        public int Quantity { get; set; }

        public decimal Unit_Price { get; set; }
    }
}
