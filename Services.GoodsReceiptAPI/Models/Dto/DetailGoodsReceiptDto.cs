namespace Services.GoodsReceiptAPI.Models.Dto
{
    public class DetailGoodsReceiptDto
    {
        public int Goo_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_Price { get; set; }
    }
}
